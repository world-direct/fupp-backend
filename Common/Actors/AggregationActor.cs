using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Commands;
using Common.Dtos;
using Common.Events;
using Akka.Cluster.Tools.PublishSubscribe;

namespace Common.Actors
{
    public class AggregationActor : ReceiveActor
    {
        private IActorRef _publishMediator { get; }
        private IList<RequestResultDto> _finishedRequests { get; }

        public AggregationActor() {

            Receive<RequestFinished>(x => HandleRequestFinished(x));
            Receive<ProcessAgentResults>(x => HandleProcessAgentResults(x));
            _publishMediator = DistributedPubSub.Get(Context.System).Mediator;

            _finishedRequests = new List<RequestResultDto>();
        }

        private void HandleRequestFinished(RequestFinished requestFinished) {
            var result = new RequestResultDto(requestFinished.ResultCode, requestFinished.RequestDuration);
            _finishedRequests.Add(result);
        }

        private void HandleProcessAgentResults(ProcessAgentResults processAgentResults) {
            _publishMediator.Tell(new Publish("agentFinished", new AgentFinished(_finishedRequests.ToList())));
        }
    }
}
