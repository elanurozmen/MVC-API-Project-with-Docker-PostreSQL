// using Npgsql;
// using System.Threading.Tasks;

// public class PostgresDbService
// {
//     private readonly string _connectionString;

//     public PostgresDbService(string connectionString)
//     {
//         _connectionString = connectionString;
//     }

//     public async Task SaveWeatherDataAsync(string date, int temperatureC, string summary)
//     {
//         using (var connection = new NpgsqlConnection(_connectionString))
//         {
//             await connection.OpenAsync();
//             var query = "INSERT INTO Weather (Date, TemperatureC, Summary) VALUES (@Date, @TemperatureC, @Summary)";
//             using (var command = new NpgsqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@Date", date);
//                 command.Parameters.AddWithValue("@TemperatureC", temperatureC);
//                 command.Parameters.AddWithValue("@Summary", summary);
//                 await command.ExecuteNonQueryAsync();
//             }
//         }
//     }
// }