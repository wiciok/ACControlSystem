using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ACControlSystemApi.Services.Interfaces;
using ACControlSystemApi.Model;

namespace ACControlSystemApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private IAuthService _authService;

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