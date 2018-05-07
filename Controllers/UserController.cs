using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MWTest.Filters;
using MWTest.Managers;
using MWTest.Model;
using MWTest.Payloads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWTest.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET users
        [HttpGet]
        [Authorize(Policy = "RoleAdmin")]
        public IEnumerable<User> Get()
        {
            return _userManager.AllUsers();
        }

        // GET users/:id
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<User> Get(int id)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role
            return await _userManager.UserWithIdAsync(id);
        }

        // POST users
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody]UserPostPayload payload)
        {
            if (_userManager.UserWithEmail(payload.Email) != null)
            {
                ModelState.AddModelError("Email", "The given email is not available");
                return new BadRequestObjectResult(ModelState);
            }

            if (_userManager.UserWithUsername(payload.Username) != null)
            {
                ModelState.AddModelError("Username", "The given username is not available");
                return new BadRequestObjectResult(ModelState);
            }

            var user = new User()
            {
                Email = payload.Email,
                Password = payload.Password,
                Username = payload.Username,
                Role = payload.Role
            };

            await _userManager.AddUserAsync(user);

            return NoContent();
        }

        // PUT users/:id
        [HttpPut("{id:int}")]
        [Authorize(Policy = "RoleUser")]
        public void Put(int id, [FromBody]string value)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role

            // TODO: update user
        }

        // DELETE users/:id
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "RoleUser")]
        public void Delete(int id)
        {
            // TODO: check if authenticated user is the same as the given id
            // or the user has admin (or superior) role

            // TODO: remove user
        }
    }
}