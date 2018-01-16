using System;
using System.Collections.Generic;
using ACCSApi.Model;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACCSApi.Controllers.Controllers
{
    [Authorize(AuthenticationSchemes = "Basic")]
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{token}/all")]
        public IActionResult GetAll(string token)
        {
            try
            {
                var usersDtos = _userService.GetAllUsers();
                return Ok(usersDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]AuthData userData)
        {
            try
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
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(userId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //edit authorization data (email, password)
        [HttpPut("{token}")]
        public IActionResult Put(string token, [FromBody]AuthData userData)
        {
            try
            {
                try
                {
                    _userService.UpdateUserAuthData(userData);
                }
                catch (ArgumentNullException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{token}/{id}")]
        public IActionResult Delete(string token, int id)
        {
            try
            {
                try
                {
                    _userService.RemoveUser(id);
                }

                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
