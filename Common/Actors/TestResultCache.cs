using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Common.Dtos;
using Common.Events;
using Common.Queries;

namespace Common.Actors {
    public class TestResultCache : ReceiveActor {

        Dictionary<string, List<AgentResultDto>> AgentStatistics { get; set; }
        new Dictionary<string, List<RequestResultDto>> RequestDetails { get; set; }
        List<TestResultDto> TestRuns { get; set; }
        public TestResultCache() {

            AgentStatistics = new Dictionary<string, List<AgentResultDto>>();
            RequestDetails = new Dictionary<string, List<RequestResultDto>>();
            TestRuns = new List<TestResultDto>();

            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe(Constants.Topics.AGENT_TOPIC, Self));
            mediator.Tell(new Subscribe(Constants.Topics.TEST_TOPIC, Self));

            //instead of handling this messages here, each could be sent to a child that reads/writes DB
            
            Receive<AgentFinished>(x => HandleAgentFinished(x));
            Receive<TestStarted>(x => HandleTestStarted(x));

            Receive<GetTestStatistics>(x => HandleGetTestStatistics(x));

            Receive<GetAgentDataForTest>(x => HandleGetAgentDataForTest(x));

            Receive<GetIndividualStandardInfoForTestAgent>(x => HandleGetIndividualStandardInfoForTestAgent(x));
        }

        private void HandleTestStarted(TestStarted x) {
            // in real life: write to DB
            TestRuns.Add(new TestResultDto(x.Url, x.TestName));
        }

        private void HandleAgentFinished(AgentFinished x) {

            if (!AgentStatistics.ContainsKey(x.TestId)) {
                AgentStatistics.Add(x.TestId, new List<AgentResultDto>());
            }

            double rate = 0.0;

            if (!RequestDetails.ContainsKey(string.Concat(x.TestId, x.AgentId))) {
                RequestDetails.Add(string.Concat(x.TestId, x.AgentId), new List<RequestResultDto>());
            }

            if (x.Requests.Any()) {

                rate = (double)x.Requests.Count(m => m.ResultCode == System.Net.HttpStatusCode.OK) / (double)x.Requests.Count();

                foreach (var item in x.Requests) {
                    // in real life: write to DB
                    RequestDetails[string.Concat(x.TestId, x.AgentId)].Add(new RequestResultDto(item.ResultCode, item.RequestDuration, item.Requestid));
                }
            }
            // in real life: write to DB
            AgentStatistics[x.TestId].Add(new AgentResultDto(rate, x.AgentId));
        }


        private void HandleGetTestStatistics(GetTestStatistics x) {
            try {
                // in real life: read from DB
                Sender.Tell(TestRuns.ToArray(), Self);
            }
            catch (Exception e) {
                Sender.Tell(new Failure { Exception = e }, Self);
            }
        }

        private void HandleGetAgentDataForTest(GetAgentDataForTest x) {
            try {

                if (AgentStatistics.ContainsKey(x.TestId)) {
                    // in real life: read from DB
                    Sender.Tell(AgentStatistics[x.TestId].ToArray());
                   
                }

                Sender.Tell(new AgentResultDto[] { }, Self);
            }
            catch (Exception e) {
                Sender.Tell(new Failure { Exception = e }, Self);
            }
        }


        private void HandleGetIndividualStandardInfoForTestAgent(GetIndividualStandardInfoForTestAgent x) {
            try {

                string id = string.Concat(x.TestId, x.AgentId);

                if (RequestDetails.ContainsKey(id)) {
                    // in real life: read from DB
                    Sender.Tell(RequestDetails[id].ToArray());

                }

                Sender.Tell(new RequestResultDto[] { }, Self);
            }
            catch (Exception e) {
                Sender.Tell(new Failure { Exception = e }, Self);
            }
        }
    }
}
