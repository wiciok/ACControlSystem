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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()   //gets current state of ACDevice
        {
            try
            {
                var retState = _acStateControlService.GetCurrentState();
                return Ok(retState);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
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
        [HttpPost]
        public IActionResult Post([FromBody]ACState state) //manually on/off ACDevice
        {
            try
            {
                try
                {
                    _acStateControlService.SetCurrentState(state);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest("Current ACDevice not set");
                }

                catch (ArgumentException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
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
