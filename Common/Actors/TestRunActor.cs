namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Akka.Cluster.Routing;
    using Akka.Cluster.Tools.PublishSubscribe;
    using Akka.Routing;
    using Commands;
    using Common.Events;

    public sealed class TestRunActor : ReceiveActor {

        private const string TO_ACTOR_ROUTER_NAME = "actorRouter";

        private readonly IActorRef publishMediator;

        public TestRunActor(StartNewLoadTest message) {
            Console.WriteLine($"TestRun created");

            ClusterRouterPool config = new ClusterRouterPool(new BroadcastPool(message.NumberOfAgents),
                new ClusterRouterPoolSettings(message.NumberOfAgents, message.NumberOfAgents, true, Constants.Roles.AGENT));
            var local = new BroadcastPool(message.NumberOfAgents);
            var testId = Guid.NewGuid().ToString();
            Context.ActorOf(Props.Create(() => new AgentActor(message, testId)).WithRouter(config), TO_ACTOR_ROUTER_NAME);

            publishMediator = DistributedPubSub.Get(Context.System).Mediator;
            publishMediator.Tell(new Publish(Constants.Topics.TEST_TOPIC, new TestStarted(testId, message.Url)));

        }
    }
}
