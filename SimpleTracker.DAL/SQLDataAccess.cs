using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace SimpleTracker.DAL;

public class SqlDataAccess : ISqlDataAccess
{
    private string _connectionString { get; set; }

    public SqlDataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure,
                                                     U parameters,
                                                     string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure,
                                 T parameters,
                                 string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}