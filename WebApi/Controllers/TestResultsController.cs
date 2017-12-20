namespace WebApi.Controllers {
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Akka.Actor;
    using Common.Dtos;
    using Common.Utility;

    public class TestResultsController : ApiController {

        public TestResultsController() {

            TestResultsRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/TestResultsRequest");
        }

        private ActorSelection TestResultsRequestActor { get; }

        // GET api/testresults
        public async Task<TestResultDto[]> Get() {

            try {
                var response = await TestResultsRequestActor.Ask<TestsResultResponse>(new TestsResultRequest());
                return response.TestResults;
            }
            catch {
                return null;
            }


        }

    }
}
