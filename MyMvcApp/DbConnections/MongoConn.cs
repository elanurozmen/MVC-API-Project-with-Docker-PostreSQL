// using MongoDB.Bson;
// using MongoDB.Driver;
// using System.Text.Json;
// using System.Threading.Tasks;

// public class MongoDbService
// {
//     private readonly IMongoCollection<BsonDocument> _weatherCollection;

//     public MongoDbService(string connectionString, string databaseName, string collectionName)
//     {
//         var client = new MongoClient(connectionString);
//         var database = client.GetDatabase(databaseName);
//         _weatherCollection = database.GetCollection<BsonDocument>(collectionName);
//     }

//     public async Task SaveWeatherDataAsync(string date, int temperatureC, string summary)
//     {
//         var document = new BsonDocument
//         {
//             { "Date", date },
//             { "TemperatureC", temperatureC },
//             { "Summary", summary }
//         };

//         await _weatherCollection.InsertOneAsync(document);
//     }
// }
