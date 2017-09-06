using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ACControlSystemApi.Model;
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

       

        [HttpGet]
        public ACState Get()   //gets current state of ACDevice
        {
            _authService.CheckAuthentication();
            return _acStateControlService.GetCurrentState();
        }
        
        // POST: api/ACControl
        [HttpPost]
        public void Post([FromBody]ACState state) //manually on/off ACDevice
        {
            _authService.CheckAuthentication();
            _acStateControlService.SetCurrentState(state);
        }
    }
}
