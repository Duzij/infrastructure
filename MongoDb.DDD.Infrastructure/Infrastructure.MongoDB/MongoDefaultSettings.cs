namespace Infrastructure.MongoDB
{
    public static class MongoDefaultSettings
    {
        public static string ConnectionString = "mongodb://localhost:27017?connect=replicaSet&maxPoolSize=500";
        public static string EventsDocumentName = "__events";
        public static string TransientTransactionError = "TransientTransactionError";
        public static string UnknownTransactionCommitResult = "UnknownTransactionCommitResult";
        public static string IdName = "_id.Value";
        public static string EtagName = "Etag";
    }
}
