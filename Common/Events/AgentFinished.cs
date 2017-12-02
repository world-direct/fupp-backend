using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dtos;

namespace Common.Events
{
    public class AgentFinished
    {
        public AgentFinished(IEnumerable<RequestResultDto> requests) {
            this.Requests = requests;
        }

        public IEnumerable<RequestResultDto> Requests { get; }
    }
}
