using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MWTest.Db;
using MWTest.Model;

namespace MWTest.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly MWTestDb _db;

        public UsersController(MWTestDb db)
        {
            _db = db;
        }

        // GET api/users
        [HttpGet]
        [Authorize]
        public IEnumerable<User> Get()
        {
            return _db.Users.ToArray();
        }

        // GET api/users/:id
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "user1";
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/:id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/:ud
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
