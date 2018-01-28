using System;
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
    [Route("api/acsetting")]
    public class ACSettingController : Controller
    {
        private readonly IACSettingsService _acSettingsService;
        private readonly ILogger<ACSettingController> _logger;

        public ACSettingController(IACSettingsService acSettingsService, ILogger<ACSettingController> logger)
        {
            _acSettingsService = acSettingsService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _acSettingsService.GetAll();

                return Ok(result);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("allon")]
        public IActionResult GetAllTurnOn()
        {
            try
            {
                var result = _acSettingsService.GetAllOn();

                return Ok(result);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                try
                {
                    _acSettingsService.Delete(guid);
                }
                catch (ItemNotFoundException e)
                {
                    return NotFound(e.Message);
                }
                return NoContent();
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //registering new codes:

        [HttpPost("nec")]
        public IActionResult AddNec([FromBody] AcSettingAdd acSettingAdd)
        {
            try
            {
                var retVal = _acSettingsService.AddNec(acSettingAdd);
                return Ok(retVal);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("raw")]
        public IActionResult AddRaw([FromBody] AcSettingAdd acSettingAdd)
        {
            try
            {
                var retVal = _acSettingsService.AddRaw(acSettingAdd);
                return Ok(retVal);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //default turn on/off settings related:

        [HttpGet("defaultOn")]
        public IActionResult GetDefaultOn()
        {
            try
            {
                var result = _acSettingsService.GetDefaultOn();
                return Ok(result);
            }

            catch (ItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set!");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("defaultOff")]
        public IActionResult GetDefaultOff()
        {
            try
            {
                var result = _acSettingsService.GetDefaultOff();
                return Ok(result);
            }

            catch (ItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest("Current ACDevice not set");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("defaultOn/{guid}")]
        public IActionResult SetDefaultOn(Guid guid)
        {
            try
            {

                IACSetting result;
                try
                {
                    result = _acSettingsService.SetDefaultOn(guid);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest("Current ACDevice not set");
                }

                catch (ItemNotFoundException e)
                {
                    return NotFound(e.Message);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("defaultOff/{guid}")]
        public IActionResult SetDefaultOff(Guid guid)
        {
            try
            {
                IACSetting result;
                try
                {
                    result = _acSettingsService.SetDefaultOff(guid);
                }
                catch (ItemNotFoundException e)
                {
                    return NotFound(e.Message);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest("Current ACDevice not set");
                }

                catch (ArgumentException e)
                {
                    return BadRequest(e.Message);
                }

                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}