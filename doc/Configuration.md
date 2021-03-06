# Configuration Guide <br/> Beacons Microservice

Configuration structure used by this module follows the 
[standard configuration] https://github.com/pip-services/pip-services/blob/master/usage/Configuration.md 
structure.

Example **config.yml** file:

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
