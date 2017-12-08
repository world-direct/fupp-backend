using System.Threading.Tasks;
using Akka.Actor;
using Common.Utility;
using WebApi.NetCore.TestActors.Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.NetCore.Controllers {
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase {

        public ValuesController() {
            ActorRef = ActorSystemProvider.ActorSystem.ActorSelection("user/TestActor");
        }

        private ActorSelection ActorRef { get; }

        // GET api/values
        [HttpGet]
        public async Task<int> Get() {
            var result = await ActorRef.Ask<ResultCalculated>(new AddCommand(8, 2));
            return result.Result;
        }
    }
}
