.NET

    dotnet --info
        .NET SDK (reflecting any global.json)
        Runtime Environment:
        .NET SDKs installed:
        .NET runtimes installed:

    dotnet -h
        SDK commands:
        add Add a package or reference to a .NET project.
        build Build a .NET project.
        build-server Interact with servers started by a build.
        clean Clean build outputs of a .NET project.
        format Apply style preferences to a project or solution.
        help Show command line help.
        list List project references of a .NET project.
        msbuild Run Microsoft Build Engine (MSBuild) commands.
        new Create a new .NET project or file.
        nuget Provides additional NuGet commands.
        pack Create a NuGet package.
        publish Publish a .NET project for deployment.
        remove Remove a package or reference from a .NET project.
        restore Restore dependencies specified in a .NET project.
        run Build and run a .NET project output.
        sdk Manage .NET SDK installation.
        sln Modify Visual Studio solution files.
        store Store the specified assemblies in the runtime package store.
        test Run unit tests using the test runner specified in a .NET project.
        tool Install or manage tools that extend the .NET experience.
        vstest Run Microsoft Test Engine (VSTest) commands.
        workload Manage optional workloads.

**UMl diagrams**
https://crashedmind.github.io/PlantUMLHitchhikersGuide/index.html

## Dotnet CLI

**Visual studio code editor configuration**
* enable async completion
* enable import completion
* Organize imports on format
* Compact view off

**Visual studio code extensions :**
* C# for visual studio code.
* C# extensions.

**create a new dotnet solution**
* dotnet new sln

**create a new dotnet webapi**
* dotnet new webapi -o API

**Add the api cproj project into the solution file**
* dotnet sln add API

**Run the project**
* dotnet run --project .\API\API.csproj

**Run the project with different lunch profile(https)**
* dotnet run --project .\API\API.csproj -lp https

**Clean the https certificate**
* dotnet dev-certs https --clean

**Add dev certificate**
* dotnet dev-certs https --trust

**Swagger endpoint**
* https://localhost:7001/swagger/index.html

**restore nuget packages**
* dotnet restore

**Nuget Packages**
* Entity Framework Core Design
* Entity Framework SQlLite

**Controllers**
* Conventionally controllers are pluralized 

**global dotnet json file**
* dotnet new globaljson


## Entity Framework CLi

dotnet ef migrations add
dotnet ef migrations list
dotnet ef migrations script
dotnet ef dbcontext info
dotnet ef dbcontext scaffold
dotnet ef database drop
dotnet ef database update

**Instal entity framework globally**
* dotnet tool install --global dotnet-ef --version 7.0.0

**Create migrations**
* dotnet ef migrations add InitialCreate -o Data/Migrations --project .\API\API.csproj


**Up/Apply the migration**
* dotnet ef database update --project .\API\API.csproj

Update Migrations dotnet ef database update --project .\API\API.csproj

## Angular

**node version**
* node --version

**npm version**
* npm --version

**instal angualr cli**
* npm install -g @angular/cli

**angular cli version**
* ng version

**create angular app**
* ng new client

**serve angular project**
* ng serve open

**angular.json**
* angular config

**tsconfig.json**
* typescript config
