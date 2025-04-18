using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private string _connectionString;

        public SQLDataAccess(string connectionString)
        {
            _connectionString = connectionString;
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
                //log
                result = new List<T>();
            }

            return result;
        }

        public Result SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default")
        {
            var result = new Result();

            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            
            return result;
        }
    }

}
