# Development and Testing Guide <br/> Beacons Microservice

This document provides high-level instructions on how to build and test the microservice.

* [Environment Setup](#setup)
* [Installing](#install)
* [Building](#build)
* [Testing](#test)

## <a name="setup"></a> Environment Setup

This is a .NET Core project and you have to install the .NET Core SDK tools. 
You can download them from the official website: https://dotnet.microsoft.com/download 

You can check that .NET has been successfully installed by running the following command:
```bash
dotnet --version
```

To work with the GitHub code repository you need to install Git from: https://git-scm.com/downloads

If you plan on developing and testing with persistent storages other than in-memory/flat file persistences,
you may need to install a database server:
- Download and install MongoDB from: https://www.mongodb.org/downloads

## <a name="install"></a> Installing

Once your environment is ready, you can check out the microservice's source code from the GitHub repository:
```bash
git clone https://github.com/pip-templates/pip-templates-microservice-dotnet.git
```

Then go to the project folder and install dependent modules:

```bash
# Install dependencies
dotnet restore
```

If you've worked with the microservice before, you can check out the latest changes and update the dependencies:
```bash
# Update source code updates from GitHub
git pull
```

## <a name="build"></a> Building

This microservice is written in the language C#, so, if you make changes to the source code, you need to compile 
it before running or committing to GitHub. The process will output compiled library files into an /obj folder.

```bash
dotnet build
```

## <a name="test"></a> Testing

The command to run unit tests is as follows:
```bash
dotnet test
```