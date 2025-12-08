using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class UserDal : DalBase
    {
        public UserDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {

        }
        public bool CreateNewUser(User user)
        {
            var result = false;
            try
            {
                result = _db.SaveData(storedProcedure: "[dbo].[spUser_Insert]", new { 
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
                result = _db.LoadData<User, dynamic>(storedProcedure: "[dbo].[spUser_Read]", new { id }).FirstOrDefault();
                _logger.LogDebug("[dbo].[spUser_Read] success");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return result;
        }
    
    }
}
