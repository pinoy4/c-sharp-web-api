using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MWTest.Managers;
using MWTest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWTest.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET api/users
        [HttpGet]
        [Authorize(Policy = "RoleAdmin")]
        public IEnumerable<User> Get()
        {
            return _userManager.AllUsers();
        }

        // GET api/users/:id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<User> Get(int id)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role
            return await _userManager.UserWithIdAsync(id);
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
            // TODO: add user
        }

        // PUT api/users/:id
        [HttpPut("{id}")]
        [Authorize(Policy = "RoleUser")]
        public void Put(int id, [FromBody]string value)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role

            // TODO: update user
        }

        // DELETE api/users/:id
        [HttpDelete("{id}")]
        [Authorize(Policy = "RoleUser")]
        public void Delete(int id)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role

            // TODO: remove user
        }
    }
}