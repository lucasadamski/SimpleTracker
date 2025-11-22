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
        // GET: api/<EntryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

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
        public Entries Get(string userId, string from, string to)
        {
            var fromDt = Temporal.ParseToDateTime(from);
            var toDt = Temporal.ParseToDateTime(from);

            var result = entryDal.ReadEntries(userId, fromDt, toDt);
            return result;
        }

        // POST api/<EntryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EntryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EntryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
