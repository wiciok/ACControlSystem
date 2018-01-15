using System;
using System.Collections.Generic;
using System.Linq;
using ACCSApi.Model;
using ACCSApi.Model.Enums;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/acschedule")]
    public class ACScheduleController : Controller
    {
        private readonly IACScheduleService _scheduleService;
        private readonly IAuthService _authService;

        public ACScheduleController(IACScheduleService acScheduleService, IAuthService authService)
        {
            _scheduleService = acScheduleService;
            _authService = authService;
        }

        [HttpGet("{token}")]
        public IActionResult GetAll(string token)
        {            
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var retVal = _scheduleService.GetAllSchedules();

                    return Ok(retVal);
                }
                return Unauthorized();
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
                if (_authService.CheckAuthentication(token))
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
                return Unauthorized();
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]ACSchedule schedule)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
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
                else
                    return Unauthorized();
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
                if (_authService.CheckAuthentication(token))
                {
                    try
                    {
                       _scheduleService.DeleteSchedule(id);
                    }
                    catch(ItemNotFoundException ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    return NoContent();
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
