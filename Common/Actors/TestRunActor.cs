namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Cluster.Routing;
    using Akka.Routing;
    using Commands;

    public sealed class TestRunActor : ReceiveActor {

        private readonly Guid id;

        public TestRunActor(StartNewLoadTest message) {
            Console.WriteLine($"TestRun created");

            ClusterRouterPool config = new ClusterRouterPool(new BroadcastPool(message.NumberOfAgents),
                new ClusterRouterPoolSettings(message.NumberOfAgents, message.NumberOfAgents, true, Constants.Roles.AGENT));
            Context.ActorOf(Props.Create(() => new AgentActor(message))
                    .WithRouter(config));
        }
    }
}
