namespace Common.Events {
    using System;
    using System.Collections.Generic;
    using Dtos;

    public sealed class AgentFinished {
        
        public AgentFinished(string testId, string agentId, IEnumerable<RequestResultDto> requests) {
            TestId = testId;
            AgentId = agentId;
            Requests = requests;
        }

        public string TestId { get; }

        public string AgentId { get; }

        public IEnumerable<RequestResultDto> Requests { get; }
    }
}
