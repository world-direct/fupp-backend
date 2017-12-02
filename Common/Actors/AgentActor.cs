namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Cluster.Routing;
    using Akka.Routing;
    using Commands;
    using Events;

    public sealed class AgentActor : ReceiveActor {

        private const string TO_SENDER_ROUTER_NAME = "senderRouter";
        private const string AGGREGATION_ACTOR_NAME = "aggregator";
        
        private readonly Guid id;
        private readonly StartNewLoadTest startNewLoadTest;

        private int numberOfReturnedRequests;

        public AgentActor(StartNewLoadTest startNewLoadTest) {
            this.startNewLoadTest = startNewLoadTest;
            id = Guid.NewGuid();

            //Pool pool = new BroadcastPool(requestsPerAgentCount);
            // ClusterRouterPoolSettings settings = new ClusterRouterPoolSettings(requestsPerAgentCount, requestsPerAgentCount, false,"Agent");
            //RouterConfig clusterRouterConfig = new ClusterRouterPool(pool, settings);
            //Context.ActorOf(Props.Create<SenderActor>().WithRouter(clusterRouterConfig), TO_SENDER_ROUTER_NAME);

            Context.ActorOf(Props.Create(() => new SenderActor(startNewLoadTest.Url)).WithRouter(new BroadcastPool(startNewLoadTest.NumberOfRequestsPerAgent)), TO_SENDER_ROUTER_NAME);
            Context.ActorOf(Props.Create<AggregationActor>(), AGGREGATION_ACTOR_NAME);

            Receive<RequestFinished>(m => HandleRequestFinished(m));
        }

        private void HandleRequestFinished(RequestFinished m) {
            numberOfReturnedRequests += 1;
            Context.Child(AGGREGATION_ACTOR_NAME).Forward(m);

            if (numberOfReturnedRequests == startNewLoadTest.NumberOfRequestsPerAgent) {
                Context.Child(AGGREGATION_ACTOR_NAME).Tell(new ProcessAgentResults());
            }
        }
    }
}
