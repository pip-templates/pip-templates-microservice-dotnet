# <img src="https://github.com/pip-services/pip-services/raw/master/design/Logo.png" alt="Pip.Services Logo" style="max-width:30%"> <br/> Beacons microservice

This is the Beacons microservice from the Pip.Templates library. 

The microservice currently supports the following deployment options:
* Deployment platforms: Standalone Process
* External APIs: HTTP/REST
* Persistence: Memory, Flat Files, MongoDB

This microservice does not depend on other microservices.

<a name="links"></a> Quick Links:

* [Download Links](doc/Downloads.md)
* [Development Guide](doc/Development.md)
* [Configuration Guide](doc/Configuration.md)
* [Deployment Guide](doc/Deployment.md)
* Communication Protocols
  - [HTTP Version 1](doc/HttpProtocolV1.md)

## Contract

The logical contract of the microservice is presented below. 

```cs
public class BeaconV1 : IStringIdentifiable
{
	public string Id { get; set; }
	public string SiteId { get; set; }
	public string Type { get; set; }
	public string Udi { get; set; }
	public string Label { get; set; }
	public CenterObjectV1 Center { get; set; }
	public double Radius { get; set; }
}

public class CenterObjectV1
{
	public string Type { get; set; }
	public double[] Coordinates { get; set; }
}

public interface IBeaconsClientV1
{
	Task<DataPage<BeaconV1>> GetBeaconsAsync(string correlationId, FilterParams filter, PagingParams paging);
	Task<BeaconV1> GetBeaconByIdAsync(string correlationId, string id);
	Task<BeaconV1> GetBeaconByUdiAsync(string correlationId, string udi);
	Task<CenterObjectV1> CalculatePositionAsync(string correlationId, string siteId, string[] udis);
	Task<BeaconV1> CreateBeaconAsync(string correlationId, BeaconV1 beacon);
	Task<BeaconV1> UpdateBeaconAsync(string correlationId, BeaconV1 beacon);
	Task<BeaconV1> DeleteBeaconByIdAsync(string correlationId, string id);
}

```

## Download

Right now, the only way to get the microservice is to check it out directly from the github repository
```bash
git clone https://github.com/pip-templates/pip-templates-microservice-dotnet.git
```

The Pip.Service team is working on implementing packaging, to make stable releases available as zip-downloadable archives.

## Run

Add the **config.yml** file to the config folder and set configuration parameters as needed.

Example of a microservice configuration
```yaml
---
# Console logger
- descriptor: "pip-services3-commons:logger:console:default:1.0"
  level: "trace"

# Performance counters that posts values to log
- descriptor: "pip-services3-commons:counters:log:default:1.0"
  level: "trace"

{{#if MONGO_ENABLED}}
# MongoDB Persistence
- descriptor: "beacons:persistence:mongodb:default:1.0"
  collection: {{MONGO_COLLECTION}}{{^MONGO_COLLECTION}}beacons{{/MONGO_COLLECTION}}
  connection:
    uri: {{MONGO_SERVICE_URI}}
    host: {{MONGO_SERVICE_HOST}}{{^MONGO_SERVICE_HOST}}localhost{{/MONGO_SERVICE_HOST}}
    port: {{MONGO_SERVICE_PORT}}{{^MONGO_SERVICE_PORT}}27017{{/MONGO_SERVICE_PORT}}
    database: {{MONGO_DB}}{{^MONGO_DB}}app{{/MONGO_DB}}
  credential:
    username: {{MONGO_USER}}
    password: {{MONGO_PASS}}
{{/if}}

{{^MOCK_ENABLED}}{{^MONGO_ENABLED}}
# Default in-memory persistence
- descriptor: "beacons:persistence:memory:default:1.0"
{{/MONGO_ENABLED}}{{/MOCK_ENABLED}}

# Default controller
- descriptor: "beacons:controller:default:default:1.0"

# Common HTTP endpoint
- descriptor: "pip-services3:endpoint:http:default:1.0"
  connection:
    protocol: "http"
    host: "0.0.0.0"
    port: {{{HTTP_PORT}}}{{^HTTP_PORT}}8080{{/HTTP_PORT}}

# HTTP service version 1.0
- descriptor: "beacons:service:http:default:1.0"

# Heartbeat service
- descriptor: "pip-services3:heartbeat-service:http:default:1.0"
  route: heartbeat

# Status service
- descriptor: "pip-services3:status-service:http:default:1.0"
  route: status
```
 
For more information on microservice configuration, see [The Configuration Guide](Configuration.md).

The microservice can be started using the command:
```bash
cd run
dotnet run
```

## Use

The easiest way to work with the microservice is through the client SDK. 

If you use .NET Core, then get references to the required libraries:
- Pip.Services3.Commons : https://github.com/pip-services3-dotnet/pip-services3-commons-dotnet
- Pip.Services3.Rpc: 
https://github.com/pip-services3-dotnet/pip-services3-rpc-dotnet

Add the **PipServices3.Commons** and **Beacons.Clients.Version1** namespaces
```cs
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using Beacons.Clients.Version1;
```

Define client configuration parameters that match the configuration of the microservice's external API
```cs
// Client configuration
var config = ConfigParams.FromTuples(
	"connection.type", "http",
	"connection.host", "localhost",
	"connection.port", 8080
);
```

Instantiate the client and open a connection to the microservice
```cs
// Create the client instance
var client = new BeaconsHttpClientV1();

// Configure the client
client.Configure(config);

// Connect to the microservice
client.OpenAsync(null);
    
// Work with the microservice
...
```

The client is now ready to perform operations
```cs
// Create a beacon
var beacon = new BeaconV1
{
	Udi = "00001",
	Type = BeaconTypeV1.AltBeacon,
	SiteId = "1",
	Label = "TestBeacon",
	Center = new CenterObjectV1 { Type = "Point", Coordinates = new double[] { 0, 0 } },
	Radius = 50
};

await client.CreateBeaconAsync(null, beacon);

// Get the list of beacons
var beaconPage = await client.GetBeaconsAsync(null,
    FilterParams.FromTuples(
        "label", "TestBeacon",
    ),
    new PagingParams(
        Skip =  0,
        Take = 10
    )
);
```

## Acknowledgements

This microservice was created and currently maintained by *Sergey Seroukhov*.
