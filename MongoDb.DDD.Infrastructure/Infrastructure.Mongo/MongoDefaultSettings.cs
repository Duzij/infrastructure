using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public static class MongoDefaultSettings
    {
        public static string ConnectionString= "mongodb://localhost:27017?connect=replicaSet";
        public static string EventsDocumentName = "__events";
        public static string TransientTransactionError = "TransientTransactionError";
        public static string UnknownTransactionCommitResult = "UnknownTransactionCommitResult";
    }
}
