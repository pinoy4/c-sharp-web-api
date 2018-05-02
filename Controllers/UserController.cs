using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MWTest.Db;
using MWTest.Model;

namespace MWTest.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly MWTestDb _db;

        public UserController(MWTestDb db)
        {
            _db = db;
        }

        // GET api/user
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _db.Users.ToArray();
        }

        // GET api/values/:id
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "user1";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/:id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/:ud
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
