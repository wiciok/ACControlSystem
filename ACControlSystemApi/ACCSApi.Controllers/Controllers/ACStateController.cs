using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Model.Transferable;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/ACState")]
    public class ACStateController : Controller
    {
        private readonly IACStateControlService _acStateControlService;
        private readonly IAuthService _authService;

        public ACStateController(IACStateControlService acStateControlService, IAuthService authService)
        {
            _acStateControlService = acStateControlService;
            _authService = authService;
        }
    
        //no authorization required, device state can be accesed by anyone
        [HttpGet]
        public IActionResult Get()   //gets current state of ACDevice
        {
            try
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/ACControl
        [HttpGet("{token}")]
        public IActionResult Get(string token) //manually on/off ACDevice
        {
            try
            {
                var state = new ACState() {IsTurnOff = true};
                _acStateControlService.SetCurrentState(state);

                return Ok();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/ACControl
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
