using System;
using ACCSApi.Model;
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
    [Route("api/acstate")]
    public class ACStateController : Controller
    {
        private readonly IACStateControlService _acStateControlService;
        private readonly ILogger<ACStateController> _logger;

        public ACStateController(IACStateControlService acStateControlService, ILogger<ACStateController> logger)
        {
            _acStateControlService = acStateControlService;
            _logger = logger;
        }

        //[AllowAnonymous]
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
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST: api/ACControl
        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]ACState state) //manually on/off ACDevice
        {
            try
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

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
