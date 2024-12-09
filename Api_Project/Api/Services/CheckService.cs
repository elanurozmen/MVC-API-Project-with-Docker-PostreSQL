using Api_Project.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;


namespace Api_Project.Services;

public class CheckService
{
    private readonly IMongoCollection<CheckCollection> _checksCollection;
    
    public CheckService( )
    {
        var checkDatabaseSettings =new CheckDatabaseSettings();
        Console.WriteLine(checkDatabaseSettings);
        var mongoClient = new MongoClient(checkDatabaseSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(checkDatabaseSettings.DatabaseName);
        _checksCollection = mongoDatabase.GetCollection<CheckCollection>(checkDatabaseSettings.CheckCollectionName);

    }
//      public void InsertSampleData()
//     {
// var sampleData = new CheckCollection
//         {
//             checkId = 2,
//             customerName = "Elanur Özmen",
//             checkAmount = 1000,
//             checkStatus = true
//         };        _checksCollection.InsertOne(sampleData); // Koleksiyona belge ekleme
//     }

    public async Task<List<CheckCollection>> GetAsync() =>
        await _checksCollection.Find(_ => true).ToListAsync();

    public async Task<CheckCollection?> GetAsync(int id) =>
        await _checksCollection.Find(x => x.checkId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(CheckCollection newCheck) =>
        await _checksCollection.InsertOneAsync(newCheck);

    public async Task UpdateAsync(int id, CheckCollection updatedCheck) =>
        await _checksCollection.ReplaceOneAsync(x => x.checkId == id, updatedCheck);

    public async Task RemoveAsync(int id) =>
        await _checksCollection.DeleteOneAsync(x => x.checkId == id);
}