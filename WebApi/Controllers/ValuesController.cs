using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using WebApi.TestActors.Messages;

namespace WebApi.Controllers {
    using Common.Utility;

    public class ValuesController : ApiController {

        private ActorSelection ActorRef { get; }

        public ValuesController() {
             ActorRef = ActorSystemProvider.ActorSystem.ActorSelection("user/TestActor");
        }

        // GET api/values
        public async Task<int> Get() {
            var result = await ActorRef.Ask<ResultCalculated>(new AddCommand(8, 2));
            return  result.Result;
        }

       
    }
}
