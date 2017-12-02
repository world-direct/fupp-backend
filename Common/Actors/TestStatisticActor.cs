using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Common.Commands;
using Common.Events;

namespace Common.Actors
{
    public class TestStatisticActor : ReceiveActor
    {
        private IActorRef _publishMediator;

        public TestStatisticActor() {
            Receive<TestStarted>(x => HandleTestStarted(x));
            Receive<AnalyseTestResult>(x => HandleAnalyseTestResult(x));
            Receive<AgentFinished>(x => HandleAgentFinished(x));

            _publishMediator = DistributedPubSub.Get(Context.System).Mediator;

        }

        private void HandleTestStarted(TestStarted testStarted) {
        }

        private void HandleAnalyseTestResult(AnalyseTestResult analyseTestResult) {
        }

        private void HandleAgentFinished(AgentFinished agentFinished) {
            _publishMediator.Tell(new Publish(Constants.Topics.TEST_TOPIC, new TestResultAnalysed()));

        }
    }
}
