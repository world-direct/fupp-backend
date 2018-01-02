using System.Threading.Tasks;
using Akka.Actor;
using Common.Utility;
using WebApi.NetCore.TestActors.Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.NetCore.Controllers {
    [Route("api/[controller]")]
    public class TestResultsController : ControllerBase {

        public TestResultsController() {
            TestResultsRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/TestResultsRequest");
        }

        private ActorSelection TestResultsRequestActor { get; }

        // GET api/testresults
        [HttpGet]
        public async Task<Common.Dtos.TestResultDto[]> Get() {
            try {
                var response = await TestResultsRequestActor.Ask<TestsResultResponse>(new TestsResultRequest());
                this.HttpContext.Response.StatusCode = 200;
                return response.TestResults;
            }
            catch {
                this.HttpContext.Response.StatusCode = 500;
                return null;
            }
        }
    }
}
