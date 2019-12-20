using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IHubContext<EventHub, IEventHub> eventHub;

        public SampleController(IHubContext<EventHub, IEventHub> eventHub) {
            this.eventHub = eventHub;
        }
        // GET api/sample
        [HttpGet]
        public ActionResult<string> Get(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                this.eventHub.Clients.All.SendNoticeEventToClient(message);
                return "Message was sent!";
            }

            return string.Empty;
        }
    }
}
