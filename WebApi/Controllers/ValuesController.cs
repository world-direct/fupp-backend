namespace WebApi.Controllers {
    using System.Threading.Tasks;
    using System.Web.Http;
    using Akka.Actor;
    using Common.Utility;

    public class ValuesController : ApiController {

        public ValuesController() {

            //var testRunCoordinator = system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");
            //var testResultCache = system.ActorOf(Props.Create<TestResultCache>(), "testResultCache");
            //ActorRef = ActorSystemProvider.ActorSystem.ActorSelection("user/TestActor");
            //  TestRunRef = Conten // ActorSystemProvider.ActorSystem.ActorSelection("testRunCoordinator");

            TestRunRequestActor = ActorSystemProvider.ActorSystem.ActorSelection("user/StartTestRequest");
        }

        private ActorSelection TestRunRequestActor { get; }

        // GET api/values
        public async Task<int> Get() {
            var response = await TestRunRequestActor.Ask<StartTestResponse>(new StartTestRequest());
             
            return 4;
        }
    }
}
