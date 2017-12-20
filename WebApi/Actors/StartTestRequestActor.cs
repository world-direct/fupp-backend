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
        TestrunCoordinatorRouter.Tell(new StartNewLoadTest("http://orf.at", 3, 5));
        
        Sender.Tell(new StartTestResponse());
    }
}
