using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ACControlSystemApi.Services.Interfaces;
using ACControlSystemApi.Model.Interfaces;
using System.Net.Http;
using ACControlSystemApi.Services.Exceptions;

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
            try
            {
                if (_authService.CheckAuthentication())
                {
                    IUser user;
                    try
                    {
                        user = _userService.GetUser(id);
                    }
                    
                    catch(ItemNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }

                    return Ok(user);
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/User
        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]IUser user)
        {
            try
            {
                if (_authService.CheckAuthentication())
                {
                    IUser retUser;
                    try
                    {
                        retUser = _userService.AddUser(user);
                    }

                    catch (ItemAlreadyExistsException ex)
                    {
                        return StatusCode(StatusCodes.Status409Conflict, ex.Message);
                    }
                    catch(ArgumentException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return Ok(retUser);
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/User/5
        [HttpPut("{token}")]
        public IActionResult Put(string token, [FromBody]IUser user)
        {
            try
            {
                if (_authService.CheckAuthentication())
                {
                    try
                    {
                        _userService.UpdateUser(user);
                    }
                    catch(ArgumentNullException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    catch(ItemNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }

                    return Ok(user); //todo: check this
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{token}/{id}")]
        public IActionResult Delete(string token, int id)
        {
            try
            {
                if (_authService.CheckAuthentication())
                {
                    try
                    {
                        _userService.RemoveUser(id);
                    }
                    
                    catch(ItemNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }
                    return Ok();
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
