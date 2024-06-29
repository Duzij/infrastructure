# Infrastructure
Simple .NET library with DDD and event sourcing in mind.

Part of the project is POC library application with RazorPages client. 

### Drivers 
* [MongoDB](https://github.com/Duzij/infrastructure/tree/master/MongoDb.DDD.Infrastructure/Infrastructure.MongoDB)

## Prerequisites 
* MongoDb: 7.0.9 (with replicaSet)
* .NET Core 8.0

sample mongod.conf

```
# mongod.conf

storage:
  dbPath: C:\Program Files\MongoDB\Server\7.0\data

systemLog:
  destination: file
  logAppend: true
  path:  C:\Program Files\MongoDB\Server\7.0\log\mongod.log

net:
  port: 27017
  bindIp: 127.0.0.1

replication:
  replSetName: rs0

```
