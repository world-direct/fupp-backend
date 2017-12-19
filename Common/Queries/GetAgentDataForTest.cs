using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Queries
{
    public sealed class GetAgentDataForTest
    {
        public GetAgentDataForTest(string testId) {
            TestId = testId;
        }

        public string TestId { get; }
    }
}
