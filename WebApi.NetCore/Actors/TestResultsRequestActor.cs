using System;
using Akka.Actor;
using Akka.Routing;
using Common.Actors;
using Common.Commands;
using Common.Dtos;
using Common.Queries;

public class TestResultsRequestActor : ReceiveActor {

    public TestResultsRequestActor() {

        TestResultsRouter = Context.ActorOf(Props.Create<TestRunCoordinator>().WithRouter(FromConfig.Instance), "TestResultCaches");

        Receive<TestsResultRequest>(x => HandleTestsResultRequest(x));
    }

    private IActorRef TestResultsRouter { get; }

    private void HandleTestsResultRequest(TestsResultRequest x) {
        //the request is empty so we read all test statistics
        try {
            var testResults = TestResultsRouter.Ask<TestResultDto[]>(new GetTestStatistics());
            testResults.Wait();
            Sender.Tell(new TestsResultResponse { TestResults = testResults.Result });
        }
        catch (Exception e) {
            //propagate exception, maybe someone wants to handle it
            Sender.Tell(new Failure { Exception = e }, Self);
        }
    }
}
