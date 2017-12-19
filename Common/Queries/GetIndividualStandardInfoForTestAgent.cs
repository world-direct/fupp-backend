using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Queries
{
    public sealed class GetIndividualStandardInfoForTestAgent
    {
        public string TestId { get; }
        public string AgentId { get; }

        public GetIndividualStandardInfoForTestAgent(string testId, string agentIf) {
            TestId = testId;
            AgentId = agentIf;
        }
    }
}
