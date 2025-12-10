using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IUserDal
    {
        bool CreateNewUser(User user);
        User ReadUser(int id);
        User ReadUser(string login);
        User ReadUser(string login, string password);
        void Update(User user);
    }
}