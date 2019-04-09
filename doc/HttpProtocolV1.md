# HTTP Protocol (version 1) <br/> Beacons Microservice

Beacons microservice implements a HTTP compatible API, that can be accessed on configured port.
All input and output data is serialized in JSON format. Errors are returned in [standard format]().

* [BeaconV1 class](#class)
* [POST /v1/beacons/get_beacons](#operation1)
* [POST /v1/beacons/get_beacon_by_id](#operation2)
* [POST /v1/beacons/get_beacon_by_udi](#operation3)
* [POST /v1/beacons/calculate_position](#operation4)
* [POST /v1/beacons/create_beacon](#operation5)
* [POST /v1/beacons/update_beacon](#operation6)
* [POST /v1/beacons/delete_beacon_by_id](#operation7)

## Data types

### <a name="class"></a> BeaconV1 class

Represents a beacon

**Properties:**
- id: string - unique beacon id
- site_id: string - unique site id
- type: string - the type of the beacon
- udi: string - the UDI of the beacon
- label: string - the label of the beacon
- center: CenterObjectV1 - the position of the beacon
- radius: double - the radius of the beacon

## Operations

### <a name="operation1"></a> Method: 'POST', route '/v1/beacons/get_beacons'

Retrieves a collection of beacons according to specified criteria

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- filter: Object
  - id: string - (optional) beacon id
  - site_id: string - (optional) the site id of the beacon
  - label: string - (optional) the label of the beacon
  - udi: string - (optional) the UDI of the beacon
  - udis: string - (optional) a comma-separated list of UDI
- paging: Object
  - skip: int - (optional) start of page (default: 0). Operation returns paged result
  - take: int - (optional) page length (max: 100). Operation returns paged result

**Response body:**
The array of BeaconV1 objects, DataPage<BeaconV1> object is paging was requested or error

### <a name="operation2"></a> Method: 'POST', route '/v1/beacons/get_beacon_by_id'

Retrieves a single beacon specified by its unique id

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- beacon_id: string - unique beacon id

**Response body:**
The BeaconV1 object, null if object wasn't found or error 

### <a name="operation3"></a> Method: 'POST', route '/v1/beacons/get_beacon_by_udi'

Retrieves a single beacon specified by its UDI

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- udi: string - the UDI

**Response body:**
The BeaconV1 object, null if object wasn't found or error 

### <a name="operation4"></a> Method: 'POST', route '/v1/beacons/calculate_position'

Calculates the center position of the beacons

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- site_id: string - unique site id
- udis: string[] - an array of UDI

**Response body:**
The middle position of the beacons as CenterObjectV1 object, null if beacons weren't found or error 

### <a name="operation5"></a> Method: 'POST', route '/v1/beacons/create_beacon'

Creates a new beacon

**Request body:**
- correlation_id: string - (optional) unique id that identifies distributed transaction
- beacon: BeaconV1 - beacon object to be created. If object id is not defined it is assigned automatically.

**Response body:**
The created BeaconV1 object or error

### <a name="operation6"></a> Method: 'POST', route '/v1/beacons/update_beacon'

Updates beacon specified by its unique id

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- beacon: BeaconV1 - beacon object with new values. Partial updates are supported

**Response body:**
The updated BeaconV1 object or error 
 
### <a name="operation6"></a> Method: 'POST', route '/v1/beacons/delete_beacon_by_id'

Deletes beacon specified by its unique id

**Request body:** 
- correlation_id: string - (optional) unique id that identifies distributed transaction
- beacon_id: string - unique beacon id

**Response body:**
The deleted BeaconV1 object or error
 