// using Npgsql;
// using System.Threading.Tasks;

// public class PostgresDbService
// {
//     private readonly string _connectionString;

//    public PostgresDbService(string connectionString)
//     {
//          if (string.IsNullOrEmpty(connectionString))
//         {
//             throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
//         }

//         _connectionString = connectionString;
//     }    public async Task SaveWeatherDataAsync(string date, int temperatureC, int temperatureF, string summary)
//     {
//         using var connection = new NpgsqlConnection(_connectionString);
//         await connection.OpenAsync();

//         var createTableQuery = @"
//             CREATE TABLE IF NOT EXISTS WeatherForecasts (
//                 Id SERIAL PRIMARY KEY,
//                 Date DATE NOT NULL,
//                 TemperatureC INT NOT NULL,
//                 TemperatureF INT NOT NULL,
//                 Summary TEXT NOT NULL
//             );";
//         using (var command = new NpgsqlCommand(createTableQuery, connection))
//         {
//             await command.ExecuteNonQueryAsync();
//         }

//         var insertQuery = @"
//             INSERT INTO WeatherForecasts (Date, TemperatureC, TemperatureF, Summary)
//             VALUES (@Date, @TemperatureC, @TemperatureF, @Summary);";

//         using var cmd = new NpgsqlCommand(insertQuery, connection);
//         cmd.Parameters.AddWithValue("Date", date);
//         cmd.Parameters.AddWithValue("TemperatureC", temperatureC);
//         cmd.Parameters.AddWithValue("TemperatureF", temperatureF);
//         cmd.Parameters.AddWithValue("Summary", summary);
//         await cmd.ExecuteNonQueryAsync();
//     }
// }
