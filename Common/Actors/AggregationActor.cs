namespace Common.Actors {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Cluster.Tools.PublishSubscribe;
    using Commands;
    using Dtos;
    using Events;

    public class AggregationActor : ReceiveActor {

        private readonly Guid testId;
        private readonly Guid agentId;
        private readonly IActorRef publishMediator;

        public AggregationActor(Guid testId, Guid agentId) {
            this.testId = testId;
            this.agentId = agentId;
            publishMediator = DistributedPubSub.Get(Context.System).Mediator;
            FinishedRequests = new List<RequestResultDto>();

            Receive<RequestFinished>(x => HandleRequestFinished(x));
            Receive<ProcessAgentResults>(x => HandleProcessAgentResults(x));
        }

        private IList<RequestResultDto> FinishedRequests { get; }

        private void HandleRequestFinished(RequestFinished requestFinished) {
            FinishedRequests.Add(new RequestResultDto(requestFinished.ResultCode, requestFinished.RequestDuration));
        }

        private void HandleProcessAgentResults(ProcessAgentResults processAgentResults) {
            publishMediator.Tell(new Publish(Constants.Topics.AGENT_TOPIC, new AgentFinished(testId, agentId, FinishedRequests.ToList())));
        }
    }
}
