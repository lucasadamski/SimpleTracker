using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.Utility;
using Serilog;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EntryController(IEntryDal entryDal, Serilog.ILogger logger) : ControllerBase
    {
        [Authorize]
        [HttpGet("{id}/{userId}")]
        public Entry Get(int id, int userId)
        {
            var result = entryDal.ReadEntry(id, userId);
            return result;
        }

        [Authorize]
        [HttpGet("{userId}/{from}/{to}")]
        public IEnumerable<Entry> Get(int userId, string from, string to)
        {
            var result = new Entries();
            var parsedFrom = Temporal.ParseToDateTime(from);
            var parsedTo = Temporal.ParseToDateTime(to);

            result = entryDal.ReadEntries(userId, parsedFrom, parsedTo);
            return result.Data;
        }

        [Authorize]
        [HttpGet("dto/{userId}/{from}/{to}")]
        public IEnumerable<EntryDto> GetDto(int userId, string from, string to)
        {
            IEnumerable<EntryDto> result;
            var parsedFrom = Temporal.ParseToDateTime(from);
            var parsedTo = Temporal.ParseToDateTime(to);

            result = entryDal.ReadEntriesDto(userId, parsedFrom, parsedTo);
            return result;
        }

        [Authorize]
        [HttpPost("Create")]
        public void Create([FromBody]Entry entry)
        {
            logger.Verbose("EntryController.Create: entry.Value {entryValue} entry.ActivityId {entryActivityId}", entry.Value, entry.ActivityId);
            entryDal.CreateNewEntry(entry);
        }

        [Authorize]
        [HttpPut("{id}/{value}")]
        public void Put(int id, int value)
        {
            entryDal.UpdateEntry(id, value);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            entryDal.DeleteEntry(id);
        }
    }
}
