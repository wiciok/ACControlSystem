using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/HostDevice")]
    public class HostDeviceController : Controller
    {
        private readonly IHostDeviceService _hostDeviceService;
        private readonly IAuthService _authService;

        public HostDeviceController(IHostDeviceService hostDeviceService, IAuthService authService)
        {
            _hostDeviceService = hostDeviceService;
            _authService = authService;
        }


        [HttpGet("{token}/all")]
        public IActionResult GetAll(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                var retVal = _hostDeviceService.GetAllDevices();
                return Ok(retVal);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/{id}")]
        public IActionResult Get(string token, int id)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                IRaspberryPiDevice retVal;
                try
                {
                    retVal = _hostDeviceService.GetDevice(id);
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                return Ok(retVal);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/current")]
        public IActionResult GetCurrentDevice(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                IRaspberryPiDevice retVal;
                try
                {
                    retVal = _hostDeviceService.GetCurrentDevice();
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                return Ok(retVal);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/{id}")]
        public IActionResult Post(string token, [FromBody]IRaspberryPiDevice device)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();
                int retId;
                try
                {
                    retId = _hostDeviceService.AddDevice(device);
                }
                catch (ItemAlreadyExistsException e)
                {
                    return BadRequest(e);
                }
                return Ok(retId);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}")]
        public IActionResult Put(string token, [FromBody]IRaspberryPiDevice device)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                try
                {
                    device = _hostDeviceService.UpdateDevice(device);
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e);
                }
                return Ok(device);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{token}/currentDevice")]
        public IActionResult PutCurrentDevice(string token, [FromBody]int id)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                IRaspberryPiDevice currentDevice;
                try
                {
                    currentDevice = _hostDeviceService.SetCurrentDevice(id);
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e);
                }
                return Ok(currentDevice);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}/{id}")]
        public IActionResult Delete(string token, int id)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                try
                {
                    _hostDeviceService.DeleteDevice(id);
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e);
                }
                return Ok(); //todo: nocontent may be better
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}