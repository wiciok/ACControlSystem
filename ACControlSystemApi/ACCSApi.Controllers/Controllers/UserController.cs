using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    //todo: check routing if its working!

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // GET: api/User/5
        [HttpGet("{token}/{id}")]
        public IActionResult Get(string token, int id)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
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

                    return Ok(user.PublicData);
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
        public IActionResult Post(string token, [FromBody]IUserRegister userData)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    int userId;
                    try
                    {
                        userId = _userService.AddUser(userData);
                    }

                    catch (ItemAlreadyExistsException ex)
                    {
                        return StatusCode(StatusCodes.Status409Conflict, ex.Message);
                    }
                    catch(ArgumentException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return Ok(userId);
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
        //edit authorization data (email, password)
        [HttpPut("{token}")]
        public IActionResult Put(string token, [FromBody]IUserRegister userData)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    try
                    {
                        _userService.UpdateUserAuthData(userData);
                    }
                    catch(ArgumentNullException ex)
                    {
                        return BadRequest(ex.Message);
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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{token}/{id}")]
        public IActionResult Delete(string token, int id)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
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
