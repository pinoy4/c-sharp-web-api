# C# Web API

This project is meant to test and combine the most common features of a web API.
It is made up of a web app in `C#` and a database running `postgresql`.

# SETUP

#### Start
To start the database run `docker-compose up` in the terminal from the root folder of the project. This will start two containers:

- postgres: runs the database;  
    username=`mwtest`, password=`mwtest`, port=`5432`, database=`mwtest`
- pgadmin: eases access to the database. It is reachable at `localhost:5050`;  
    username=`pgadmin4@pgadmin.org`, password=`admin`
    
#### Migrations
To run migrations execute `dotnet ef database update` in the terminal from the root folder of the project.

#### Package dependencies
To enable swagger documentation install the following Nuget package from your package manager console:

```sh
Install-Package Swashbuckle.AspNetCore
```

Navigate your browser to https://localhost:{yourport}/swagger  to view your new API documentation.

# FEATURES:

- [x] database connection *(EntityFramework + Postgresql)*
- [x] database migrations *(EntityFramework)*
- [x] authentication *(Jwt)*
- [x] authorization *(role based from Jwt)*
- [x] automatic swagger documentation
- [ ] testing
- [x] input validation and payloads
- [ ] cors
- [ ] https
- [ ] error responses

# HOW TO
Here is a list of things that have been included in the project. This is not meant as a tutorial but as a quick reference for understanding what has gone into creating the project. Hopefully this will be useful when trying to replicate these features.

#### Bootstrap
This application builds on top of Visual Studio's Web API template.

#### Dependency injection
For a deeper understanding of ASP.Net Core's dependency injection [this explanation](https://joonasw.net/view/aspnet-core-di-deep-dive) is very helpful.
Following Microsoft's pattern of extending the `IServiceCollection` with methods to register services the `RegisterServices` static class was created. In it we add all necessary dependencies and then we call it's `AddMWTestServices` method in the `ConfigureServices` method of `Startup`.

#### Configuration
To add the configuration to any service (or to `Startup`) just add it as a dependency in the constructor and the dependency injection engine will handle the rest. It is a good idea where appropriate to use the [Options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.1) in configuration.
This enables the configuration to be mapped as objects and be injected in services that require it.

#### Postgresql connection
- Add dependencies `Npgsql.EntityFrameworkCore.PostgreSQL` and `Npgsql.EntityFrameworkCore.PostgreSQL.Design`
- Create the database `DbContext` class (`MWTestDb.cs`) with only the constructor
- Add the `DBConnectionOptions` key in the `appsettings.json` file
- Add the `AddMWTestDbService` method in the `RegisterServices` extension class
- In the `ConfigureServices` method of `Startup` add the database service
- [npgsql documetation](http://www.npgsql.org/efcore/index.html)

#### Database model
- Create a class to represent a model (`User`). Use annotations to specify constraints on the properties
- Add a `DbSet` of the model type to the `DbContext` (`MWTestDb.cs`)
- [Microsoft's documentation](https://docs.microsoft.com/en-gb/ef/core/get-started/aspnetcore/new-db?view=aspnetcore-2.1#create-the-model)

#### Database migrations
These migrations are generated by a tool starting from the database model in the code. When generating a migration a snapshot is created along with the actual migration and it is used to build the next migration.

- **Manually** add the `Microsoft.EntityFrameworkCore.Tools.DotNet` dependency to the `*.csproj` file.
- From the CLI run `dotnet ef migrations add migration_name` to generate the first migration (change `migration_name` to whatever you want to name the migration)
- [Microsoft's documentation](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations?view=aspnetcore-2.1)

If you encounter a `Build failed.` message then you can run the command with the `-v` flag to see what is causing the problem. If a file cannot be accessed you might need stop the project (web app) if it is running.

#### Routing
To enable routing there are two methods. Configuring it on startup or using attributes in the controller. You can find an example of how the attributes are used in the `UserController` class. More details on routing can be found in [Microsoft's documentation](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1).

#### JWT Authentication
This part is heavily *inspired* (pronounced copy-pasted) from [this tutorial](https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login).

- Add dependency for `Microsoft.AspNetCore.Authentication.JwtBearer`
- Add `JWTIssuerOptions` key in `appsettings.json`
- Add class `JwtIssuerOptions`
- Add interface `IJwtFactory` and implementing class `JwtFctory`
- Add class `JwtController`
- Add the `app.UseAuthentication();` line in the `Configure` method of `Startup`
- Add the `[Authorize]` attribute on the controller or action that needs authorization

#### Role based Authorization

- Add the `Role` property to `User`
- Add the `"Role"` claim in `JwtFactory`'s `claims` variable
- Add the authorization options to the `IServiceCollection` in `RegisterServices`
- Add proper `[Authoriza(	Policy = "PolicyName")]` annotations in the controllers

#### Input validation
The simplest way to validate input is to create an object that represents the input payload (like `UserPostPayload`) and set it as the parameter of the action (like `UserController`'s `Post` action). The system will automatically try to populate the properties with values from the request.

If further validation is needed you can add attributes to the object's properties like `[Required]` or `[EmailAddress]`. Then you must check if the `ModelState.IsValid` property is true or false. You can do this in the controller but it is better to create a reusable Filter to accomplish this task:

- Add the `ValidateModelAttribute` class
- Register it in the `AddMWTestServices` method of `RegisterServises`
- Add the `[ValidateModel]` attribute to the action (like in `UserController`'s `Post` method)
