using System;
using ACCSApi.Model;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/ACState")]
    public class ACStateController : Controller
    {
        private readonly IACStateControlService _acStateControlService;
        private readonly IAuthService _authService;
        private readonly ILogger<ACStateController> _logger;

        public ACStateController(IACStateControlService acStateControlService, IAuthService authService, ILogger<ACStateController> logger)
        {
            _acStateControlService = acStateControlService;
            _authService = authService;
            _logger = logger;
        }

        //no authorization required, device state can be accesed by anyone
        [HttpGet]
        public IActionResult Get()   //gets current state of ACDevice
        {
            /*try
            {
                var retState = _acStateControlService.GetCurrentState();
                return Ok(retState);
            }

            catch (ACStateUndefinedException ex)
            {
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }*/

            try
            {
                //var retState = _acStateControlService.GetCurrentState();
                var retState = new ACState() {IsTurnOff = false};
                return Ok(retState);
            }

            catch (ACStateUndefinedException ex)
            {
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /*// POST: api/ACControl
        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]IACState state) //manually on/off ACDevice
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    try
                    {
                        _acStateControlService.SetCurrentState(state);
                    }

                    catch (ArgumentException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return Ok();
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }*/
        [HttpPost]
        public IActionResult Post([FromBody]ACState state) //manually on/off ACDevice
        {
            try
            {
                //if (_authService.CheckAuthentication(token))
                if (true)
                {
                    try
                    {
                        _acStateControlService.SetCurrentState(state);
                    }

                    catch (ArgumentException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return Ok();
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                //return StatusCode(StatusCodes.Status500InternalServerError,"test error");
                return BadRequest(ex.Message);
            }
        }
    }
}
