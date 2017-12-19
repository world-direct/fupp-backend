using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos {
    public sealed class AgentResultDto {
        public double SuccessRate { get; }

        public string ID { get; }

        public AgentResultDto(double successRate, string iD) {
            SuccessRate = successRate;
            ID = iD;
        }
    }
}
