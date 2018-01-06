using System;
using Akka.Actor;
using Akka.Routing;
using Common.Actors;
using Common.Commands;
using Common.Dtos;
using Common.Queries;

public class RequestResultsRequestActor : ReceiveActor {

    public RequestResultsRequestActor() {

        TestResultsRouter = Context.ActorOf(Props.Create<TestRunCoordinator>().WithRouter(FromConfig.Instance), "TestResultCaches");

        Receive<RequestsResultRequest>(x => HandleAgentsResultRequest(x));
    }

    private IActorRef TestResultsRouter { get; }

    private void HandleAgentsResultRequest(RequestsResultRequest x) {
        //the request is empty so we read all test statistics
        try {
            Console.WriteLine($"request request for testID {x.testId} and agentId {x.agentId}");
            var requestResults = TestResultsRouter.Ask<RequestResultDto[]>(new GetIndividualStandardInfoForTestAgent(x.testId, x.agentId));
            requestResults.Wait();
            Sender.Tell(new RequestsResultsResponse { RequestResults = requestResults.Result });
        }
        catch (Exception e) {
            //propagate exception, maybe someone wants to handle it
            Sender.Tell(new Failure { Exception = e }, Self);
        }
    }
}
