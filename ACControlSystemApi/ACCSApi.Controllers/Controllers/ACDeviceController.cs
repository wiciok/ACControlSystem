using System;
using System.Collections.Generic;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Route("api/ACDevice")]
    public class ACDeviceController : Controller
    {
        private readonly IACDeviceService _acDeviceService;
        private readonly IAuthService _authService;

        public ACDeviceController(IACDeviceService acDeviceService, IAuthService authService)
        {
            _acDeviceService = acDeviceService;
            _authService = authService;
        }

        [HttpGet("{token}/all")]
        public IActionResult GetAll(string token)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                var retVal = _acDeviceService.GetAllDevices();
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

                IACDevice retVal;
                try
                {
                    retVal = _acDeviceService.GetDevice(id);
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

                IACDevice retVal;
                try
                {
                    retVal = _acDeviceService.GetCurrentDevice();
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
        public IActionResult Post(string token, [FromBody]IACDevice device)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();
                int retId;
                try
                {
                    retId = _acDeviceService.AddDevice(device);
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

        [HttpPut("{token}")]
        public IActionResult Put(string token, [FromBody]IACDevice device)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                try
                {
                    device = _acDeviceService.UpdateDevice(device);
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

                IACDevice currentDevice;
                try
                {
                    currentDevice = _acDeviceService.SetCurrentDevice(id);
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

        [HttpDelete("{token}/{id}")]
        public IActionResult Delete(string token, int id)
        {
            try
            {
                if (!_authService.CheckAuthentication(token))
                    return Unauthorized();

                try
                {
                    _acDeviceService.DeleteDevice(id);
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


        #region code-related methods



        #endregion
    }
}
