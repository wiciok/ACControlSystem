using Microsoft.AspNetCore.Mvc;
using ACControlSystemApi.Services.Interfaces;

namespace ACStateSystemApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ACState")]
    public class ACStateController : Controller
    {
        private IACStateControlService _acStateControlService;
        private IAuthService _authService;

        public ACStateController(IACStateControlService acStateControlService, IAuthService authService)
        {
            _acStateControlService = acStateControlService;
            _authService = authService;
        }

       

        [HttpGet("{token}")]
        public ACState Get(string token)   //gets current state of ACDevice
        {
            _authService.CheckAuthentication(token);
            return _acStateControlService.GetCurrentState();
        }
        
        // POST: api/ACControl
        [HttpPost("{token}")]
        public void Post(string token, [FromBody]ACState state) //manually on/off ACDevice
        {
            _authService.CheckAuthentication(token);
            _acStateControlService.SetCurrentState(state);
        }
    }
}
