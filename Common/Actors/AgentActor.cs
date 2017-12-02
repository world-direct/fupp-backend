namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Cluster.Routing;
    using Akka.Configuration;
    using Akka.Routing;
    using Events;

    public sealed class AgentActor : ReceiveActor {

        private const string SENDER_ROUTER_NAME = "senderRouter";

        private readonly Guid testRunId;
        private readonly int requestsPerAgentCount;
        private readonly Guid id;

        public AgentActor(Guid testRunId, int requestsPerAgentCount) {
            Console.WriteLine("AgentActor created");
            this.testRunId = testRunId;
            this.requestsPerAgentCount = requestsPerAgentCount;
            id = Guid.NewGuid();

            Pool pool = new BroadcastPool(requestsPerAgentCount);
            ClusterRouterPoolSettings settings = new ClusterRouterPoolSettings(requestsPerAgentCount, requestsPerAgentCount, false,"Agent");
            RouterConfig clusterRouterConfig = new ClusterRouterPool(pool, settings);
            Context.ActorOf(Props.Create<SenderActor>().WithRouter(clusterRouterConfig), SENDER_ROUTER_NAME);

            Receive<RequestFinished>(m => HandleRequestFinished(m));
        }

        private void HandleRequestFinished(RequestFinished m) {
            
        }
    }
}
