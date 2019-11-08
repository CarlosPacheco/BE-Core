import { throwError, Observable, Subject } from 'rxjs';
import { OnDestroy, EventEmitter } from '@angular/core';

import {
  HubConnection, HubConnectionBuilder,
  JsonHubProtocol, LogLevel, HubConnectionState
} from "@aspnet/signalr";

export class SignalRHub implements OnDestroy {
  private connection: HubConnection;
  private subjects: { [name: string]: Subject<any | any[]> };
  private promise: Promise<void>;

  private readonly state: Subject<HubConnectionState>;
  private readonly error: Subject<Error>;
  public onStateChanged: Observable<HubConnectionState>;
  public onError: Observable<Error>;

  public connectionEstablished = new EventEmitter<boolean>();

  constructor(private url: string) {
    this.createConnection();
    this.subjects = {};
    this.state = new Subject<HubConnectionState>();
    this.onStateChanged = this.state.asObservable();
    this.error = new Subject<Error>();
    this.onError = this.error.asObservable();
  }

  ngOnDestroy(): void {
      if (this.error) {
      this.error.unsubscribe();
    }

    if (this.state) {
      this.state.unsubscribe();
    }

    if (this.connection) {
      this.connection.stop();
    }
  }

  /*
  * Start the connection
  */
  public start() {
    if (!this.hasSubscriptions()) {
      console.warn('No listeners have been setup. You need to setup a listener before starting the connection or you will not receive data.');
    }

    try {

      if (this.state) {
        this.state.next(this.connection.state);
      }

      this.promise = this.connection.start().then(() => {

        if (this.state) {
          this.state.next(this.connection.state);
        }
       
        this.connectionEstablished.emit(true);
      });
      this.promise.catch(this.handleOnRejectedError);;
    } catch (err) {
      console.log(err);
      if (this.state) {
        this.state.next(this.connection.state);
      }
      setTimeout(this.start, 5000);
    }
  }

  public on<T>(event: string): Observable<T | any[]> {
    const subject = this.getOrCreateSubject<T | any[]>(event);
    this.connection.on(event, (...args: any[]) =>
      subject.next(<T | any[]>args)
    );
    return subject.asObservable();
  }

  public async invoke(method: string, ...args: any[]): Promise<any> {
    if (!this.promise)
      return Promise.reject('The connection has not been started yet. Please start the connection by invoking the start method before attempting to send a message to the server.');
    await this.promise;
    return this.connection.invoke(method, ...args);
  }

  private hasSubscriptions(): boolean {
    for (let key in this.subjects) {
      if (this.subjects.hasOwnProperty(key)) {
        return true;
      }
    }

    return false;
  }

  private getOrCreateSubject<T>(event: string): Subject<T> {
    return this.subjects[event] || (this.subjects[event] = new Subject<T | any[]>());
  }

  private createConnection() {

    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Information)
      .withUrl(this.url).withHubProtocol(new JsonHubProtocol()).build();

    if (this.state) {
      this.state.next(this.connection.state);
    }
    this.connection.onclose(this.onConnectionClosed);
  }

  private handleOnRejectedError(reason: any) {
    console.log(reason);
    if (this.state) {
      this.state.next(this.connection.state);
    }
    return throwError(reason || 'Server error');
  }

  private onConnectionClosed(error?: Error): void {
    if (this.state) {
      this.state.next(this.connection.state);
    }

    if (this.error) {
      this.error.next(error);
    }
  }

}
