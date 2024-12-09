
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Api_Project.Models;

public class CheckCollection{ 
            public ObjectId _id { get; set; }
            public int checkId { get; set; }
            public string? customerName { get; set; }
            public float checkAmount { get; set; }
            public bool checkStatus{get;set;}
            public string drawer{get;set;}
}

public class CheckDatabaseSettings
{
    public string ConnectionString { get; set; } = "mongodb://mongodb:27017";

    public string DatabaseName { get; set; } = "CheckDb";

    public string CheckCollectionName { get; set; } = "CheckCollection";
}
 