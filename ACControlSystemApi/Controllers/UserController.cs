using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ACControlSystemApi.Services.Interfaces;
using ACControlSystemApi.Model.Interfaces;
using System.Net.Http;

namespace ACControlSystemApi.Controllers
{
    //todo: check routing if its working!

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // GET: api/User/5
        [HttpGet("{token}/{id}", Name = "Get")]
        public IActionResult Get(string token, int id)
        {
            if (_authService.CheckAuthentication())
            {
                var user = _userService.GetUser(id);

                if (user != null)
                    return Ok(user);
                else
                    return NoContent();
            }
            else
                return Unauthorized();
        }

        // POST: api/User
        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]IUser user)
        {
            if (_authService.CheckAuthentication())
            {
                bool result = _userService.AddUser(user);

                if (result)
                    return Ok();
                else
                    throw new NotImplementedException(); //todo: finish this
            }
            else
                return Unauthorized();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
