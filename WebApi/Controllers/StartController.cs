namespace WebApi.Controllers {
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Akka.Actor;
    using Common.Dtos;
    using Common.Utility;

    public class StartController : ApiController {

        public StartController() {

            TestRunRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/StartTestRequest");
        }

        private ActorSelection TestRunRequestActor { get; }

        // GET api/start
        public async Task<HttpStatusCode> Get(string url, int nAgents, int nRequests) {

            try {
                var response = await TestRunRequestActor.Ask<StartTestResponse>(new StartTestRequest {Url=url, NumberOfAgents = nAgents, ReQuestsPerAgent = nRequests });
                return HttpStatusCode.OK;
            }
            catch {
                return HttpStatusCode.InternalServerError;
            }
             

        }

    }
}
