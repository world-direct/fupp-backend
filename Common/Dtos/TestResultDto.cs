using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos {
    public sealed class TestResultDto {
        public TestResultDto(string url, string iD) {
            Url = url;
            ID = iD;
        }

        public string Url { get; }
        public string ID { get; }
    }
}
