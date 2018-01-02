using System.Threading.Tasks;
using Akka.Actor;
using Common.Utility;
using WebApi.NetCore.TestActors.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebApi.NetCore.Controllers {
    [Route("api/[controller]")]
    public class StartController : ControllerBase {

        public StartController() {
            TestRunRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/StartTestRequest");
        }

        private ActorSelection TestRunRequestActor { get; }

        // GET api/start
        [HttpGet]
        public async Task Get(string url, int nAgents, int nRequests) {

            try {
                var response = await TestRunRequestActor.Ask<StartTestResponse>(new StartTestRequest {Url=url, NumberOfAgents = nAgents, ReQuestsPerAgent = nRequests });
                this.HttpContext.Response.StatusCode = 200;
            }
            catch {
                this.HttpContext.Response.StatusCode = 500;
            }           
        }
    }
}
