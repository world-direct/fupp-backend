namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Configuration;
    using Akka.Routing;
    using Events;

    public sealed class AgentActor : ReceiveActor {

        private const string SENDER_ROUTER_NAME = "senderRouter";

        private readonly Guid testRunId;
        private readonly int requestsPerAgentCount;
        private readonly Guid id;

        public AgentActor(Guid testRunId, int requestsPerAgentCount) {
            this.testRunId = testRunId;
            this.requestsPerAgentCount = requestsPerAgentCount;
            id = Guid.NewGuid();

            Config config = new Cluster
            Context.ActorOf(Props.Create<SenderActor>().WithRouter(new BroadcastPool(requestsPerAgentCount,)), SENDER_ROUTER_NAME);

            Receive<RequestFinished>(m => HandleRequestFinished(m));
        }

        private void HandleRequestFinished(RequestFinished m) {
            
        }

    }
}
