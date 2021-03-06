﻿#reference "~\ReferencedAssemblies\CrossCutting.Typewriter.dll" 
${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;
     using CrossCutting.Typewriter;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.IncludeProject("DDDReflection");
        settings.OutputFilenameFactory = file => 
        {
            return $"..\\..\\..\\FE\\ALDA-UI\\src\\app\\core\\services\\" + file.Name.Replace("Controller.cs", ".service.ts").ToLower();   
        };
    }

     // Change BaseApiController to Service
    string ServiceName(Class c) => c.ServiceName();

    //get the service route
    string ServiceRoute(Class c) => c.ServiceRoute();

    // Turn IActionResult into void
    string ReturnType(Method m) => m.ReturnType();

    // Get the non primitive paramaters so we can create the Imports at the
    // top of the service
    string ImportsList(Class c) => c.ImportsList();

    // Format the method based on the return type
    string MethodFormat(Method m) => m.MethodFormat();

}//The do not modify block below is intended for the outputted typescript files... 
//*************************DO NOT MODIFY**************************
//
//THESE FILES ARE AUTOGENERATED WITH TYPEWRITER AND ANY MODIFICATIONS MADE HERE WILL BE LOST
//PLEASE VISIT http://frhagn.github.io/Typewriter/ TO LEARN MORE ABOUT THIS VISUAL STUDIO EXTENSION
//
//*************************DO NOT MODIFY**************************

import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '@env/environment';
$Classes(:BaseApiController)[$ImportsList
// ReSharper disable once InconsistentNaming
const API_BASE_URL: string = environment.apis.core + '$ServiceRoute';

@Injectable()
export class $ServiceName {
    constructor(private httpClient: HttpClient) { }        
    $Methods[
    // $HttpMethod: $Url      
    public $name($Parameters[$name: $Type][, ]): Observable<$ReturnType> {
        return this.httpClient.$HttpMethod$MethodFormat
            .pipe(catchError(e => this.handleError(e)));
    }]

    // Utility
    private handleError(error: HttpErrorResponse) {
        return throwError(error || 'Server error');
    }
}]
