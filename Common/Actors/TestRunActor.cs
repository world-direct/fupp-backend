namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Cluster.Routing;
    using Akka.Routing;
    using Commands;

    public sealed class TestRunActor : ReceiveActor {

        private const string TO_ACTOR_ROUTER_NAME = "actorRouter";

        public TestRunActor(StartNewLoadTest message) {
            Console.WriteLine($"TestRun created");

            ClusterRouterPool config = new ClusterRouterPool(new BroadcastPool(message.NumberOfAgents),
                new ClusterRouterPoolSettings(message.NumberOfAgents, message.NumberOfAgents, true, Constants.Roles.AGENT));
            var local = new BroadcastPool(message.NumberOfAgents);
            var guid = Guid.NewGuid();
            Context.ActorOf(Props.Create(() => new AgentActor(message, Guid.NewGuid())).WithRouter(config), TO_ACTOR_ROUTER_NAME);
        }
    }
}
