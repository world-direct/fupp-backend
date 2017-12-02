using System;
using System.Net;

namespace Common.Dtos {
    public class RequestResultDto {

        public RequestResultDto(HttpStatusCode resultCode, TimeSpan requestDuration)
        {
            ResultCode = resultCode;
            RequestDuration = requestDuration;
        }

        public HttpStatusCode ResultCode { get; }
        public TimeSpan RequestDuration { get; }

    }
}