using System;
using ACCSApi.Model.Dto;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/ACSetting")]
    public class ACSettingController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICodeRecordingService _codeRecordingService;
        private readonly IACSettingsService _acSettingsService;

        public ACSettingController(IAuthService authService, ICodeRecordingService codeRecordingService, IACSettingsService acSettingsService)
        {
            _authService = authService;
            _codeRecordingService = codeRecordingService;
            _acSettingsService = acSettingsService;
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //todo: check if its gonna deserialize properly
        [HttpGet("{token}/{guid}")]
        public IActionResult GetSpecific(string token, Guid guid)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    IACSetting result;
                    try
                    {
                        result = _acSettingsService.Get(guid);
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPut("{token}")]
        public IActionResult Put(string token, [FromBody] IACSetting setting)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    try
                    {
                        setting = _acSettingsService.Update(setting);
                    }
                    catch (ItemNotFoundException e)
                    {
                        return NotFound(e.Message);
                    }
                    return Ok(setting);
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

        [HttpDelete("{token}")]
        public IActionResult Delete(string token, [FromBody]Guid guid)
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
                //todo: logging
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
                //todo: logging
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
                //todo: logging
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

            catch (Exception ex)
            {
                //todo: logging
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

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/{guid}/defaultOn")]
        public IActionResult SetDefaultOn(string token, [FromBody]Guid guid)
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/{guid}/defaultOff")]
        public IActionResult SetDefaultOff(string token, [FromBody]Guid guid)
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
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}