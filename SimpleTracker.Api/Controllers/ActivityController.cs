using Microsoft.AspNetCore.Mvc;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController(IActivityDal activityDal) : ControllerBase
    {
        [HttpGet("{id}/{userId}")]
        public Activity Get(int id, string userId)
        {
            return activityDal.ReadActivity(id, userId);
        }

        [HttpGet("{userId}")]
        public IEnumerable<Activity> Get(string userId)
        {
            return activityDal.GetAllActivities(userId);
        }


        [HttpPost("{name}/{unitId}/{userId}")]
        public void Post(string name, int unitId, string userId)
        {
            var activity = new Activity() { Name = name, UnitId = unitId, UserId = userId };
            activityDal.CreateNewActivity(activity);
        }

        [HttpPut("{id}/{name}/{unitId}/{userId}")]
        public void Put(int id, string name, int unitId, string userId)
        {
            var activity = new Activity() { Id = id, Name = name, UnitId = unitId, UserId = userId };
            activityDal.UpdateActivity(activity);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            activityDal.DeleteActivity(id);
        }
    }
}
