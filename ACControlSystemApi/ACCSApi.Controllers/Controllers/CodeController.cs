using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/Code")]
    public class CodeController : Controller
    {
        private readonly ICodeRecordingService _codeRecordingService;
        private readonly IACDeviceService _acDeviceService;
        private readonly IAuthService _authService;

        public CodeController(IAuthService authService, IACDeviceService acDeviceService, ICodeRecordingService codeRecordingService)
        {
            _authService = authService;
            _acDeviceService = acDeviceService;
            _codeRecordingService = codeRecordingService;
        }

        [HttpPost("{token}/nec")]
        public IActionResult RecordNecCode(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                _codeRecordingService.RecordNecCode();

                return Ok();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/raw")]
        public IActionResult RecordRawCode(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                _codeRecordingService.RecordRawCode();

                return Ok();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{token}/nec")]
        public IActionResult ResetCurrentAcDeviceNecCodeSettings(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                _codeRecordingService.ResetCurrentAcDeviceNecCodeSettings();

                return Ok();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}