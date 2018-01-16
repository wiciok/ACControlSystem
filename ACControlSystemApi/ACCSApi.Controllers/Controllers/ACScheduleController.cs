using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model;
using ACCSApi.Model.Enums;
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
        private readonly IAuthService _authService;
        private readonly ILogger<ACScheduleController> _logger;

        public ACScheduleController(IACScheduleService acScheduleService, IAuthService authService, ILogger<ACScheduleController> logger)
        {
            _scheduleService = acScheduleService;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("{token}")]
        public IActionResult GetAll(string token)
        {
            try
            {
                var retVal = _scheduleService.GetAllSchedules();

                return Ok(retVal);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "500: Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{token}/{id}")]
        public IActionResult Get(string token, int id)
        {
            try
            {
                IACSchedule retVal;
                try
                {
                    retVal = _scheduleService.GetSchedule(id);
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

        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]ACSchedule schedule)
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

        [HttpDelete("{token}/{id}")]
        public IActionResult Delete(string token, int id)
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
