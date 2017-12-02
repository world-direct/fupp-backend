namespace WebApi.Controllers {
    using System.Threading.Tasks;
    using System.Web.Http;
    using Akka.Actor;
    using Common.Utility;
    using TestActors.Messages;

    public class ValuesController : ApiController {

        public ValuesController() {
            ActorRef = ActorSystemProvider.ActorSystem.ActorSelection("user/TestActor");
        }

        private ActorSelection ActorRef { get; }

        // GET api/values
        public async Task<int> Get() {
            var result = await ActorRef.Ask<ResultCalculated>(new AddCommand(8, 2));
            return result.Result;
        }
    }
}
