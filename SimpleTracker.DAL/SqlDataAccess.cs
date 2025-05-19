using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using Microsoft.Extensions.Logging;

namespace SimpleTracker.DAL
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private string _connectionString;
        private ILogger _logger;

        public SQLDataAccess(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            IEnumerable<T> result;
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                result = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.LogError("SQLDataAccess.LoadData failed for stored procedure {StoredProcedure} connection string {ConnectionString} error message {Exception}", storedProcedure, _connectionString, e.Message);
                result = new List<T>();
            }

            return result;
        }

        public bool SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default")
        {
            var result = true;

            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.LogError("SQLDataAccess.SaveData failed for stored procedure {StoredProcedure} connection string {ConnectionString} error message {Exception}", storedProcedure, _connectionString, e.Message);
                result = false;
            }
            
            return result;
        }
    }

}
