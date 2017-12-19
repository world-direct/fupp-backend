namespace Common.Actors {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Cluster.Tools.PublishSubscribe;
    using Commands;
    using Dtos;
    using Events;
    using Newtonsoft.Json;

    public class AggregationActor : ReceiveActor {

        private readonly string testId;
        private readonly string agentId;
        private readonly IActorRef publishMediator;

        public AggregationActor(string testId, string agentId) {
            this.testId = testId;
            this.agentId = agentId;
            publishMediator = DistributedPubSub.Get(Context.System).Mediator;
            FinishedRequests = new List<RequestResultDto>();

            Receive<RequestFinished>(x => HandleRequestFinished(x));
            Receive<ProcessAgentResults>(x => HandleProcessAgentResults(x));
        }

        private IList<RequestResultDto> FinishedRequests { get; }

        private void HandleRequestFinished(RequestFinished requestFinished) {
            FinishedRequests.Add(new RequestResultDto(requestFinished.ResultCode, requestFinished.RequestDuration, requestFinished.SenderId));
        }

        private void HandleProcessAgentResults(ProcessAgentResults m) {
            var x = new AgentFinished(testId, agentId, FinishedRequests.ToList());
            Console.WriteLine($"Result is {JsonConvert.SerializeObject(x)}");
            publishMediator.Tell(new Publish(Constants.Topics.AGENT_TOPIC, new AgentFinished(testId, agentId, FinishedRequests.ToList())));
        }
    }
}
