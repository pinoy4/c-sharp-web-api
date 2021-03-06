﻿using Microsoft.AspNetCore.Mvc;
using MWTest.Auth;
using MWTest.Filters;
using MWTest.Managers;
using MWTest.Payloads;
using System.Threading.Tasks;

namespace MWTest.Controllers
{
    [Route("jwt")]
    public class JwtController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IJwtFactory _jwtFactory;

        public JwtController(
            IUserManager userManager,
            IAuthenticationManager authenticationManager,
            IJwtFactory jwtFactory
        )
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _jwtFactory = jwtFactory;
        }

        // POST jwt
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody]JwtPostPayload payload)
        {
            // Get user from db
            var user = _userManager.UserWithUsername(payload.Username);
            if (user == null)
            {
                return NotFound(); // user not found
            }

            // Verify password
            if (!_authenticationManager.IsPasswordCorrect(user, payload.Password))
            {
                return Unauthorized(); // password is not valid
            }

            // Generate jwt
            var jwt = await _jwtFactory.GenerateEncodedToken(user);

            // Set jwt header
            Request.HttpContext.Response.Headers.Add("jwt", jwt);

            // Return correct result
            return NoContent();
        }
    }
}
