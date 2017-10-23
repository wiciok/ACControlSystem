using System;
using ACCSApi.Model.Transferable;
using ACCSApi.Services.Interfaces;
using ACCSApi.Services.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACCSApi.Controllers.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]AuthPackage auth)
        {
            try
            {
                var result = _authService.TryAuthenticate(auth);

                if (result != null)
                    return Ok(result);
                return Unauthorized();
            }

            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                //todo: logging
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}