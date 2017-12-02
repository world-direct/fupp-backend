using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events
{
    public sealed class RequestFinished
    {
        public RequestFinished(HttpStatusCode resultCode, TimeSpan requestDuration) {
            ResultCode = resultCode;
            RequestDuration = requestDuration;
        }

        public HttpStatusCode ResultCode { get;  }
        public TimeSpan RequestDuration { get; }

    }
}
