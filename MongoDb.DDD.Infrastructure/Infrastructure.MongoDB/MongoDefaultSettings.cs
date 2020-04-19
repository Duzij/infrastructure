﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDB
{
    public static class MongoDefaultSettings
    {
        public static string ServerUrl = "localhost";
        public static string EventsDocumentName = "__events";
        public static string TransientTransactionError = "TransientTransactionError";
        public static string UnknownTransactionCommitResult = "UnknownTransactionCommitResult";
        public static string IdName = "_id.Value";
        public static string EtagName = "Etag";
    }
}
