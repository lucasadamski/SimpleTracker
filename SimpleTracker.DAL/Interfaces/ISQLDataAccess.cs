using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface ISQLDataAccess
    {
        public IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        public bool SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}