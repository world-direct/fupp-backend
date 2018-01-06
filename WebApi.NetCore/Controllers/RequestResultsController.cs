using System.Threading.Tasks;
using Akka.Actor;
using Common.Utility;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.NetCore.Controllers {
    [Route("api/[controller]")]
    public class RequestResultsController : ControllerBase {

        public RequestResultsController() {
            RequestResultsRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/RequestResultsRequest");
        }

        private ActorSelection RequestResultsRequestActor { get; }

        // GET api/requestResults?testId=1234&agentId=5678
        [HttpGet]
        public async Task<Common.Dtos.RequestResultDto[]> Get(string testId, string agentId) {
            try {
                var response = await RequestResultsRequestActor.Ask<RequestsResultsResponse>(new RequestsResultRequest(testId, agentId));
                this.HttpContext.Response.StatusCode = 200;
                return response.RequestResults;
            }
            catch {
                this.HttpContext.Response.StatusCode = 500;
                return null;
            }
        }
    }
}
