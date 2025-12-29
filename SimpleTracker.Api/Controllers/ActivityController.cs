using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.DTO.Summary;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActivityController(IActivityDal activityDal, IUserDal userDal, IUnitDal unitDal) : ControllerBase
    {
        [Authorize]
        [HttpGet("{id}/{userId}")]
        public Activity Get(int id, int userId)
        {
            return activityDal.ReadActivity(id, userId);
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IEnumerable<Activity> GetAll()
        {
            var userId = GetUserId();
            return activityDal.GetAllActivities(userId);
        }

        [Authorize]
        [HttpGet("GetQuickStatsForAll")]
        public IEnumerable<ActivityQuickStats> GetQuickStatsForAll()
        {
            var userId = GetUserId();
            return activityDal.GetAllActivitiesQuickStats(userId);
        }

        [Authorize]
        [HttpPost("Create")]
        public void Create([FromBody] Activity activity)
        {
            activity.UserId = GetUserId();
            activityDal.CreateNewActivity(activity);
        }

        

        [Authorize]
        [HttpPut("{id}/{name}/{unitId}/{userId}")]
        public void Put(int id, string name, int unitId, int userId)
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

        [Authorize]
        [HttpGet("GetAllUnits")]
        public IEnumerable<Unit> GetAllUnits() => 
            unitDal.GetAll();

        private int GetUserId()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            return userDal.ReadUserIdFromToken(token);
        }
    }
}
