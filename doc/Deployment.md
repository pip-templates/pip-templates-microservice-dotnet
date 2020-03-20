# Deployment Guide <br/> Beacons Microservice

* [Standalone Process](#process)

## <a name="process"></a> Standalone Process

The simplest way to deploy the microservice is to run it as a standalone process. 
This microservice is implemented in .NET Core and requires installation of the .NET Core SDK. 
You can get it from the official website: https://www.microsoft.com/net/download/

**Step 1.** Download the microservice by following these [instructions](Download.md)

**Step 2.** Add the **config.yml** file to the root of the microservice and set configuration parameters as needed. 
See the [Configuration Guide](Configuration.md) for details on how this is done.

**Step 3.** Start the microservice using the command:

```bash
cd run
dotnet run
```