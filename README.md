# infrastructure
Simple .net core library which allows to use MongoDB and DDD approach


## Prerequisites 
* MongoDb: 4.2.3 2008R2Plus SSL (with replicaSet)

* .NET Core 3.0

sample mongod.conf

```
    # mongod.conf

    storage:
      dbPath: C:\Program Files\MongoDB\Server\4.2\data
      journal:
        enabled: true

    systemLog:
      destination: file
      logAppend: true
      path:  C:\Program Files\MongoDB\Server\4.2\log\mongod.log

    net:
      port: 27017
      bindIp: 127.0.0.1

    replication:
      replSetName: rs0
      oplogSizeMB: 100
```
