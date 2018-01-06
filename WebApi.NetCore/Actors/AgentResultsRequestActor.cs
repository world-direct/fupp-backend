using System;
using Akka.Actor;
using Akka.Routing;
using Common.Actors;
using Common.Commands;
using Common.Dtos;
using Common.Queries;

public class AgentResultsRequestActor : ReceiveActor {

    public AgentResultsRequestActor() {

        TestResultsRouter = Context.ActorOf(Props.Create<TestRunCoordinator>().WithRouter(FromConfig.Instance), "TestResultCaches");

        Receive<AgentsResultRequest>(x => HandleAgentsResultRequest(x));
    }

    private IActorRef TestResultsRouter { get; }

    private void HandleAgentsResultRequest(AgentsResultRequest x) {
        //the request is empty so we read all test statistics
        try {
            Console.WriteLine($"request agents for testId {x.testId}");
            var agentResults = TestResultsRouter.Ask<AgentResultDto[]>(new GetAgentDataForTest(x.testId));
            agentResults.Wait();
            Sender.Tell(new AgentsResultResponse { AgentResults = agentResults.Result });
        }
        catch (Exception e) {
            //propagate exception, maybe someone wants to handle it
            Sender.Tell(new Failure { Exception = e }, Self);
        }
    }
}
