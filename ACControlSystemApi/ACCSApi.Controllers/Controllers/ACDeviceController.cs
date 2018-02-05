using System;
using ACCSApi.Model.Dto;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACCSApi.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Basic")]
    [Route("api/acdevice")]
    public class ACDeviceController : Controller
    {
        private readonly IACDeviceService _acDeviceService;
        private readonly ILogger<ACDeviceController> _logger;

        public ACDeviceController(IACDeviceService acDeviceService, ILogger<ACDeviceController> logger)
        {
            _acDeviceService = acDeviceService;
            _logger = logger;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var retVal = _acDeviceService.GetAllDevicesDtos();

                return Ok(retVal);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                AcDeviceDto retVal;
                try
                {
                    retVal = _acDeviceService.GetDeviceDto(id);
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                return Ok(retVal);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("current")]
        public IActionResult GetCurrentDevice()
        {
            try
            {
                AcDeviceDto retVal;
                try
                {
                    retVal = _acDeviceService.GetCurrentDeviceDto();
                    if (retVal == null)
                        throw new ItemNotFoundException("Current device not set!");
                }
                catch (ItemNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                return Ok(retVal);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]AcDeviceDto device)
        {
            try
            {
                int retId;
                try
                {
                    retId = _acDeviceService.AddDevice(device);
                }
                catch (ItemAlreadyExistsException e)
                {
                    return BadRequest(e.Message);
                }
                return Ok(retId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]AcDeviceDto device)
        {
            try
            {
                try
                {
                    device = _acDeviceService.UpdateDevice(device);
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e.Message);
                }
                return Ok(device);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("current")]
        public IActionResult PutCurrentDevice([FromBody]string id)
        {
            try
            {
                AcDeviceDto currentDevice;
                try
                {
                    currentDevice = _acDeviceService.SetCurrentDevice(int.Parse(id));
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e.Message);
                }
                return Ok(currentDevice);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                try
                {
                    _acDeviceService.DeleteDevice(id);
                }
                catch (ItemNotFoundException e)
                {
                    return BadRequest(e.Message);
                }
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
