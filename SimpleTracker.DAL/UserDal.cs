using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class UserDal : DalBase, IUserDal
    {
        public UserDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {

        }
        public bool CreateNewUser(User user)
        {
            var result = false;
            try
            {
                result = _db.SaveData(storedProcedure: "[dbo].[spUser_Insert]", new
                {
                    login = user.Login,
                    password = user.Password,
                    token = user.Token,
                    refreshToken = user.RefreshToken,
                    refreshTokenExpiryDate = user.RefreshTokenExpiryDate
                });
                _logger.LogDebug("[dbo].[spUser_Insert] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return result;
        }
        public User ReadUser(int id)
        {
            var result = new User();
            try
            {
                result = _db.LoadData<User, dynamic>(storedProcedure: "[dbo].[spUser_ReadById]", new { id }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_ReadById] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public User ReadUser(string login)
        {
            var result = new User();
            try
            {
                result = _db.LoadData<User, dynamic>(storedProcedure: "[dbo].[spUser_ReadByLogin]", new { login }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_ReadByLogin] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public User ReadUser(string login, string password)
        {
            var result = new User();
            try
            {
                result = _db.LoadData<User, dynamic>(storedProcedure: "[dbo].[spUser_ReadByCredentials]", new { login, password }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_ReadByCredentials] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public User ReadUserByToken(string token)
        {
            var result = new User();
            try
            {
                result = _db.LoadData<User, dynamic>(storedProcedure: "[dbo].[spUser_ReadByToken]", new { token }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_ReadByToken] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }

        public void Update(User user)
        {            
            try
            {
                _db.LoadData<bool, dynamic>(storedProcedure: "[dbo].[spUser_Update]", new { 
                    user.Id, 
                    user.Login, 
                    user.Password, 
                    user.Token,
                    user.RefreshToken,
                    user.RefreshTokenExpiryDate
                    }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_ReadByCredentials] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

    }
}
