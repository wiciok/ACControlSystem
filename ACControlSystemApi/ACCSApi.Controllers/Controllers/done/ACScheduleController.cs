using System;
using ACCSApi.Model.Interfaces;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers.done
{
    [Produces("application/json")]
    [Route("api/ACSchedule")]
    public class ACScheduleController : Controller
    {
        private IACScheduleService _scheduleService;
        private IAuthService _authService;

        public ACScheduleController(IACScheduleService acScheduleService, IAuthService authService)
        {
            _scheduleService = acScheduleService;
            _authService = authService;
        }

        [HttpGet("{token}")]
        public IActionResult Get(string token)
        {            
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var retVal = _scheduleService.GetAllSchedules();
                    if (retVal == null)
                        return NoContent();
                    else
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

        [HttpGet("{token}/{id}")]
        public IActionResult Get(string token, int id)
        {
            try
            {
                if (_authService.CheckAuthentication(token))
                {
                    var retVal = _scheduleService.GetSchedule(id);
                    if (retVal == null)
                        return NoContent();
                    else
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

        [HttpPost("{token}")]
        public IActionResult Post(string token, [FromBody]IACSchedule schedule)
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

                    return Ok();
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
