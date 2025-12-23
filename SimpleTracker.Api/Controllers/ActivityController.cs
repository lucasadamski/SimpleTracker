using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActivityController(IActivityDal activityDal, IUserDal userDal) : ControllerBase
    {
        [Authorize]
        [HttpGet("{id}/{userId}")]
        public Activity Get(int id, string userId)
        {
            return activityDal.ReadActivity(id, userId);
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IEnumerable<Activity> GetAll([FromBody]string token)
        {
            var userId = userDal.ReadUserIdFromToken(token);
            return activityDal.GetAllActivities(userId);
        }

        [Authorize]
        [HttpPost("{name}/{unitId}/{userId}")]
        public void Post(string name, int unitId, string userId)
        {
            var activity = new Activity() { Name = name, UnitId = unitId, UserId = userId };
            activityDal.CreateNewActivity(activity);
        }

        [Authorize]
        [HttpPut("{id}/{name}/{unitId}/{userId}")]
        public void Put(int id, string name, int unitId, string userId)
        {
            var activity = new Activity() { Id = id, Name = name, UnitId = unitId, UserId = userId };
            activityDal.UpdateActivity(activity);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            activityDal.DeleteActivity(id);
        }
    }
}
