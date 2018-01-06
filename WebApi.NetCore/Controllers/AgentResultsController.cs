using System.Threading.Tasks;
using Akka.Actor;
using Common.Utility;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.NetCore.Controllers {
    [Route("api/[controller]")]
    public class AgentResultsController : ControllerBase {

        public AgentResultsController() {
            AgentResultsRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/AgentResultsRequest");
        }

        private ActorSelection AgentResultsRequestActor { get; }

        // GET api/agentResults&testId=1234
        [HttpGet]
        public async Task<Common.Dtos.AgentResultDto[]> Get(string testId) {
            try {
                var response = await AgentResultsRequestActor.Ask<AgentsResultResponse>(new AgentsResultRequest(testId));
                this.HttpContext.Response.StatusCode = 200;
                return response.AgentResults;
            }
            catch {
                this.HttpContext.Response.StatusCode = 500;
                return null;
            }
        }
    }
}
