# emonitor

![Build .Net Core](https://github.com/Eugenio161288/emonitor/workflows/Build%20.Net%20Core/badge.svg)
![Build SPA](https://github.com/Eugenio161288/emonitor/workflows/Build%20SPA/badge.svg?branch=master)

## Table of contents
 - [Architecture overview](#architecture-overview)
 - [Client application](#client-application)
 - [Server](#server)
 - [Local development](#local-development)
 - [Continuous Integration](#continuous-integration)
 - [Swagger](#swagger)
 - [Deployment architecture](#deployment-architecture)
 - [Notes and assumptions](#notes-and-assumptions)

## Architecture overview
[back to the contents](#table-of-contents)
![Architecture Overview](./docs/books-online-architecture.svg)

Main components:

**Identity Server**: protects resources, authenticates clients, validates tokens, registers new users. OpenID Connect and OAuth 2.0 protocols are used

**Identity DB**: DB that stores user identities and operation data (codes, tokens etc.). The relational DB (SQL Server) is used here.

**API Server**: API resources (endpoints) that the client wants to invoke

**API DB**: DB that stores data for API functionality. The Document DB (MongoDB) is used here.

**Client**: Single Page Application that may be used to register/login and invoke API functionality

Users can login/register via client application in a browser. 3rd party resellers should be registered in the system before using API endpoints

## Client application
[back to the contents](#table-of-contents)

The client application was implemented using [Angular](https://angular.io/). 

Please find detailed documentation about client application [here](./src/Client/Readme.md)

## Server
[back to the contents]

Backend applications were implemented using [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1). For unit and integration testing [xUnit](https://github.com/xunit/xunit) is used.

## Local development
[back to the contents](#table-of-contents)

For local development the `euro_monitor_db_dev` MongoDb DB was provisioned on [mLab](https://mlab.com/), please use it for local development and integration tests.

Please follow [Client](#client-application) instructions to configure client for local development.

1. ```src/Server> dotnet build```
2. ```src/Server/Euromonitor.Server.IdentityServer.Infrastructure> dotnet ef database update --context AppIdentityDbContext```
3. ```src/Server/Euromonitor.Server.IndetityServer.Infrastucture> dotnet ef database update --context PersistedGrantDbContext```
4. ```src/Server> dotnet build```
5. ```src/Server> dotnet run Euromonitor.Server.IdentityServer```
6. ```src/Server> dotnet run Euromonitor.Server.Api```
7. ```src/Client> npm install```
8. ```src/Client> ng serve```

**IdentityServer** runs under 5000 port, **API Server** runs under 5050 port, **SPA Client** runs under 4200 port

## Continuous Integration
[back to the contents](#table-of-contents)

The **Github Actions** are used for CI in this repository. There are 2 jobs: [Build SPA](https://github.com/Eugenio161288/emonitor/actions?query=workflow%3A%22Build+SPA%22) job builds client application and [Build .Net Core](https://github.com/Eugenio161288/emonitor/actions?query=workflow%3A%22Build+.Net+Core%22) job builds server code and runs unit and integration tests.

## Swagger
[back to the contents](#table-of-contents)

[NSwag](https://github.com/RicoSuter/NSwag) is used for Swagger/OpenAPI specification in the API Server project (Euromonitor.Server.Api). Please use ```/swagger``` link for Swagger.

## Deployment architecture
[back to the contents](#table-of-contents)
![Deployment Architecture](./docs/deployment-architecture.svg)

This picture shows an example of the deployed solution. Please use [link](https://erspaproxy.azurewebsites.net) to try the solution. 
[API Swagger link](https://erbooksonlineapi.azurewebsites.net/swagger) link for Swagger/OpenAPI specification of the hosted API.

## Notes and assumptions
[back to the contents](#table-of-contents)

- Registration and authorization functionality are implemented for demo purposes and contain the main idea only. **Multi factor authentication (MFA), password confirmation, duplicated password, Facebook/Google accounts etc. aren't implemented in this solution**
- Solution structure. To make it easier to take a look at all components I put all projects in one repository. For real project client, identity server and API server may be stored in different repositories, because they may be supported by different teams and they are deployed separately
- Connection strings. To make it easier for you to run the solution with real DBs (including test MongoDB instance in cloud) I put connection string for test/develop instances in source code. Hosted [example](https://erspaproxy.azurewebsites.net/) I provided above uses other DB instances. For real projects I use [Key Vault](https://azure.microsoft.com/en-us/services/key-vault/) or [Vault](https://aws.amazon.com/quickstart/architecture/vault/)/[AWS Secrets Manager](https://aws.amazon.com/secrets-manager/) depending on cloud provider
- Continuous Integration. **Github actions** is configured for CI in the repository for Demo purpose (and to try it out, because I haven't had a chance to use it on real projects yet)
- MongoDB for API DB. Document DB is used here for demo purposes only. It's possible to implement this technical assessment in different ways, I've chosen NoSQL DB because this task has quite simple data structure, so this example shows that Identity Server and API Server may have own DBs and even different types of the DBS
- In-memory clients and API and identity resources. To make it easier quickly adding/removing clients or resources for Identity Server, they are stored in memory. Because the application should be restarted on production after each editing, in real project I'd use DB for this data too.
- Docker containers. I didn't use docker containers for this task
- SQL Server DB. The SQL Server is used here as an example. For real project with familiar data structure I'd look to **MySQL** db too before implementing
