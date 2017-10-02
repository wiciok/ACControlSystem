using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ACControlSystemApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ACSchedule")]
    public class ACScheduleController : Controller
    {
        // GET: api/ACSchedule
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ACSchedule/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ACSchedule
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ACSchedule/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{token/id}")]
        public IActionResult Delete(string token, int id)
        {
            throw new NotImplementedException();
        }
    }
}
