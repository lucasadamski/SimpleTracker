using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IUserDal
    {
        bool CreateNewUser(User user);
        User ReadUser(int id);
        User ReadUser(string login);
        User ReadUser(string login, string password);
        User ReadUserByToken(string token);
        void Update(User user);
    }
}