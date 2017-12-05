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

        private readonly StartNewLoadTest startNewLoadTest;

        private int numberOfReturnedRequests;

        public AgentActor(StartNewLoadTest startNewLoadTest, Guid testId) {
            Console.WriteLine($"{nameof(AgentActor)} (path {Self.Path}) created.");
            this.startNewLoadTest = startNewLoadTest;
            Guid id = Guid.NewGuid();

            Context.ActorOf(Props.Create(() => new SenderActor(Self, startNewLoadTest.Url)).WithRouter(new BroadcastPool(startNewLoadTest.NumberOfRequestsPerAgent)), TO_SENDER_ROUTER_NAME);
            Context.ActorOf(Props.Create(() => new AggregationActor(testId, id)), AGGREGATION_ACTOR_NAME);

            Receive<RequestFinished>(m => HandleRequestFinished(m));
        }

        private void HandleRequestFinished(RequestFinished m) {
            Console.WriteLine($"AgentActor: Request finished with status code {m.ResultCode}.");
            numberOfReturnedRequests += 1;
            Context.Child(AGGREGATION_ACTOR_NAME).Forward(m);

            if (numberOfReturnedRequests == startNewLoadTest.NumberOfRequestsPerAgent) {
                Context.Child(AGGREGATION_ACTOR_NAME).Tell(new ProcessAgentResults());
            }
        }
    }
}
