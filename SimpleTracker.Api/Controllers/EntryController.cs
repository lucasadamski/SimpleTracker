using Microsoft.AspNetCore.Mvc;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.Utility;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController(IEntryDal entryDal) : ControllerBase
    {
        // GET api/<EntryController>/5
        //https://localhost:7168/api/entry/1
        [HttpGet("{id}/{userId}")]
        public Entry Get(int id, string userId)
        {
            var result = entryDal.ReadEntry(id, userId);
            return result;
        }

        [HttpGet("{userId}/{from}/{to}")]
        // https://localhost:7168/api/Entry/testUser/09-05-2025/10-05-2025
        public IEnumerable<Entry> Get(string userId, string from, string to)
        {
            var result = new Entries();
            var parsedFrom = Temporal.ParseToDateTime(from);
            var parsedTo = Temporal.ParseToDateTime(to);

            result = entryDal.ReadEntries(userId, parsedFrom, parsedTo);
            return result.Data;
        }

        [HttpGet("dto/{userId}/{from}/{to}")]
        // https://localhost:7168/api/Entry/dto/testUser/01-01-2024/01-01-2034
        public IEnumerable<EntryDto> GetDto(string userId, string from, string to)
        {
            IEnumerable<EntryDto> result;
            var parsedFrom = Temporal.ParseToDateTime(from);
            var parsedTo = Temporal.ParseToDateTime(to);

            result = entryDal.ReadEntriesDto(userId, parsedFrom, parsedTo);
            return result;
        }

        // POST api/<EntryController>
        [HttpPost("{value}/{activityId}")]
        public void Post(int value, int activityId)
        {
            var entry = new Entry()
            {
                Value = value,
                ActivityId = activityId,
                DateAdded = DateTime.Now,
            };
            entryDal.CreateNewEntry(entry);
        }

        // PUT api/<EntryController>/5
        [HttpPut("{id}/{value}")]
        public void Put(int id, int value)
        {
            entryDal.UpdateEntry(id, value);
        }

        // DELETE api/<EntryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            entryDal.DeleteEntry(id);
        }
    }
}
