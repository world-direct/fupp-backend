using System;
using Akka.Actor;
using Akka.Routing;
using Common.Actors;
using Common.Commands;

public class StartTestRequestActor : ReceiveActor {

    public StartTestRequestActor() {

        TestrunCoordinatorRouter = Context.ActorOf(Props.Create<TestRunCoordinator>().WithRouter(FromConfig.Instance), "TestRunCoordinators");

        Receive<StartTestRequest>(x => HandleStartTestRequest(x));
    }

    private IActorRef TestrunCoordinatorRouter { get; }

    private void HandleStartTestRequest(StartTestRequest x) {
        //this is pretty much "fire and forget"
        TestrunCoordinatorRouter.Tell(new StartNewLoadTest(x.Url, x.NumberOfAgents, x.ReQuestsPerAgent));
        //therefore there is actually nothing to tell back....
        Sender.Tell(new StartTestResponse());
    }
}
