using System;
using ACCSApi.Model;
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
    [Route("api/acschedule")]
    public class ACScheduleController : Controller
    {
        private readonly IACScheduleService _scheduleService;
        private readonly ILogger<ACScheduleController> _logger;

        public ACScheduleController(IACScheduleService acScheduleService, ILogger<ACScheduleController> logger)
        {
            _scheduleService = acScheduleService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var retVal = _scheduleService.GetAllSchedules();

                return Ok(retVal);
            }

            catch (CurrentACDeviceNotSetException ex)
            {
                _logger.LogError(ex, "400: Bad request");
                return BadRequest(ex);
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
                IACSchedule retVal;
                try
                {
                    retVal = _scheduleService.GetSchedule(id);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest(ex);
                }

                catch (ItemNotFoundException e)
                {
                    return NotFound();
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
        public IActionResult Post([FromBody]ACSchedule schedule)
        {
            try
            {
                int createdScheduleId;
                try
                {
                    createdScheduleId = _scheduleService.AddNewSchedule(schedule);
                }

                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest(ex);
                }

                catch (ACScheduleNotAddedException ex)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ex.Message);
                }

                return Ok(createdScheduleId);
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
                    _scheduleService.DeleteSchedule(id);
                }
                catch (ItemNotFoundException ex)
                {
                    return BadRequest(ex.Message);
                }

                catch (CurrentACDeviceNotSetException ex)
                {
                    _logger.LogError(ex, "400: Bad request");
                    return BadRequest(ex);
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
