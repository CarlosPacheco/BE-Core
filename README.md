# BE-Core
Start project provides a layered architectural model based on Domain Driven Design, Clean and provides a SOLID model for your application.

### Introduction

The layering of an application's codebase is a widely accepted technique to
help reduce complexity and to improve code reusability. To achieve a layered
architecture, ASP.NET Boilerplate follows the principles of **Domain
Driven Design**.

### Domain Driven Design Layers

There are four fundamental layers in Domain Driven Design (DDD):

-   **Presentation Layer**: Provides an interface to the user. Uses the
    Application Layer to achieve user interactions.
-   **Application Layer**: Mediates between the Presentation and Domain
    Layers. Orchestrates business objects to perform specific
    application tasks.
-   **Domain Layer**: Includes business objects and their rules. This is the
    heart of the application.
-   **Infrastructure Layer**: Provides generic technical capabilities
    that support higher layers mostly using 3rd-party libraries.

### ASP.NET Boilerplate Application Architecture Model

In addition to DDD, there are also other logical and physical layers in
a modern architected application. The model below is suggested and
implemented for ASP.NET Boilerplate applications. ASP.NET Boilerplate
not only makes implementing this model easier by providing base classes
and services, but also provides [startup templates](/Templates) to
directly start with this model.

[<img src="images/abp-nlayer-architecture.png" alt="ASP.NET Boilerplate NLayer Architecture" class="img-thumbnail" width="1220" height="1236" />](https://raw.githubusercontent.com/aspnetboilerplate/aspnetboilerplate/master/doc/WebSite/images/abp-nlayer-architecture.png)

#### Client Applications

These are remote clients that use the application as a service via HTTP APIs
(API Controllers, [OData](OData-Integration.md) Controllers, maybe even a
GraphQL endpoint). A remote client can be a SPA (Single Page App), a mobile application, or
a 3rd-party consumer. [Localization](Localization.md) and
[Navigation](Navigation.md) can be done inside this applications.

#### Presentation Layer

ASP.NET \[Core\] MVC (Model-View-Controller) can be considered to be the
presentation layer. It can be a physical layer (uses application via
HTTP APIs) or a logical layer (directly injects and uses [application
services](Application-Services.md)). In either case it can include
[Localization](Localization.md), [Navigation](Navigation.md),
[Object Mapping](Object-To-Object-Mapping.md),
[Caching](Caching.md), [Configuration
Management](Setting-Management.md), [Audit
Logging](Audit-Logging.md) and so on. It also deals with
[Authorization](Authorization.md), [Session](Abp-Session.md),
[Features](Feature-Management.md) (for
[multi-tenant](Multi-Tenancy.md) applications) and [Exception
Handling](Handling-Exceptions.md).

#### Distributed Service Layer

This layer is used to serve application/domain functionality via remote
APIs like REST, OData, GraphQL... They don't contain business logic but
only translate HTTP requests to domain interactions, or can use
application services to delegate the operation. This layer generally
includes [Authorization](Authorization.md), [Caching](Caching.md),
[Audit Logging](Audit-Logging.md), [Object
Mapping](Object-To-Object-Mapping.md), [Exception
Handling](Handling-Exceptions.md), [Session](Abp-Session.md) and so
on...

#### Application Layer

The application layer mainly includes [Application
Services](Application-Services.md) that use domain layer and domain
objects ([Domain Services](Domain-Services.md),
[Entities](Entities.md)...) to perform requested application
functionalities. It uses [Data Transfer
Objects](Data-Transfer-Objects.md) to get data from and return data
to the presentation or distributed service layer. It can also deal with
[Authorization](Authorization.md), [Caching](Caching.md), [Audit
Logging](Audit-Logging.md), [Object
Mapping](Object-To-Object-Mapping.md), the [Session](Abp-Session.md) and
so on...

#### Domain Layer

This is the main layer that implements our domain logic. It includes
[Entities](Entities.md), [Value Objects](Value-Objects.md), and [Domain
Services](Domain-Services.md) to perform business/domain logic. It can
also include [Specifications](Specifications.md) and trigger [Domain
Events](EventBus-Domain-Events.md). It defines Repository Interfaces
to read and persist entities from the data source (generally a DBMS).

#### Infrastructure Layer

The infrastructure layer makes other layers work: It implements
the repository interfaces (using [Entity Framework
Core](Entity-Framework-Core.md) for example) to actually work with a
real database. It may also include an integration to a vendor to [send
emails](Email-Sending.md) and so on. This is not a strict layer below
all layers, but actually supports other layers by implementing the abstract
concepts of them.


### DDD & Architectures

DDD &  Clean architecture focus on separating the domain code from the application and infrastructure code. But that is where the similarities end.

In Clean/Hexagonal/Onion (or CHO in short) architecture, the goal of this decoupling is testability and modularity with intended effect being that the "core" of our software can be reasoned about in isolation from rest of the world.

In DDD, the main goal is to establish common language with the business experts. The separation of the domain from rest of the application code is just a side effect of this main goal. It also has some say about the design of classes as entities and aggregates, but that is only within the domain itself. It has nothing to say about design outside the domain code.

In practice, you might find that you use both at the same time. You use CHO architecture to design the whole structure of the system, with the "domain core" being isolated in it's separate modules. And then you use DDD to design this domain core in collaboration with domain experts and possibly by using DDD concepts like Entites and Aggregates.

DDD "layer architecture" 
https://github.com/EduardoPires/EquinoxProject
https://aspnetboilerplate.com/Pages/Documents/NLayer-Architecture

Clean/Hexagonal/Onion (or CHO in short) architecture
https://github.com/jasontaylordev/CleanArchitecture
https://github.com/ardalis/CleanArchitecture

Note: read about: Eric Evans, Steve Smith (ardalis), Jason Taylor, Jimmy Bogard, Sam Newman, Martin Fowler 
Remenber: DDD doesn't enforce any specific architecture, you are free to choose whatever works for your application. DDD guides us in right direction but we are the ones that need to make the decisions for each particular situation. There is no perfect model, all are wrong but some are useful you just need to make the right compromise.

"Be a student, not a follower. Don't just go do what someone says. Take interest in what someone says, then debate it, ponder it, and consider it from all angles." by Jim Rohn
