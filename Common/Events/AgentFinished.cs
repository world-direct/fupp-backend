namespace Common.Events {
    using System;
    using System.Collections.Generic;
    using Dtos;

    public sealed class AgentFinished {
        
        public AgentFinished(Guid testId, Guid agentId, IEnumerable<RequestResultDto> requests) {
            TestId = testId;
            AgentId = agentId;
            Requests = requests;
        }

        public Guid TestId { get; }

        public Guid AgentId { get; }

        public IEnumerable<RequestResultDto> Requests { get; }
    }
}
