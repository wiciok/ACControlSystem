using System;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/acsetting")]
    public class ACSettingController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IACSettingsService _acSettingsService;
        private readonly ILogger<ACSettingController> _logger;

        public ACSettingController(IAuthService authService, IACSettingsService acSettingsService, ILogger<ACSettingController> logger)
        {
            _authService = authService;
            _acSettingsService = acSettingsService;
            _logger = logger;
        }


        [HttpGet("{token}")]
        public IActionResult GetAll(string token)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var result = _acSettingsService.GetAll();

                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{token}/allon")]
        public IActionResult GetAllTurnOn(string token)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var result = _acSettingsService.GetAllOn();

                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{token}/{guid}")]
        public IActionResult Delete(string token, Guid guid)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    try
                    {
                        _acSettingsService.Delete(guid);
                    }
                    catch (ItemNotFoundException e)
                    {
                        return NotFound(e.Message);
                    }
                    return Ok(); //todo: nocontent?
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //registering new codes:

        [HttpPost("{token}/nec")]
        public IActionResult AddNec(string token, [FromBody] AcSettingAdd acSettingAdd)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var retVal = _acSettingsService.AddNec(acSettingAdd);
                    return Ok(retVal);
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/raw")]
        public IActionResult AddRaw(string token, [FromBody] AcSettingAdd acSettingAdd)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var retVal = _acSettingsService.AddRaw(acSettingAdd);
                    return Ok(retVal);
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //default turn on/off settings related:

        [HttpGet("{token}/defaultOn")]
        public IActionResult GetDefaultOn(string token)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var result = _acSettingsService.GetDefaultOn();
                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (ItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/defaultOff")]
        public IActionResult GetDefaultOff(string token)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var result = _acSettingsService.GetDefaultOff();
                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (ItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/defaultOn/{guid}")]
        public IActionResult SetDefaultOn(string token, Guid guid)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    IACSetting result;
                    try
                    {
                        result = _acSettingsService.SetDefaultOn(guid);
                    }
                    catch (ItemNotFoundException e)
                    {
                        return NotFound(e.Message);
                    }

                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/defaultOff/{guid}")]
        public IActionResult SetDefaultOff(string token, Guid guid)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
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
                    catch (ArgumentException e)
                    {
                        return BadRequest(e.Message);
                    }

                    return Ok(result);
                }
                else
                    return Unauthorized();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}