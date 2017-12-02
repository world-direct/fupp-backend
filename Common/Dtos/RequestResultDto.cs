namespace Common.Dtos {
    using System;
    using System.Net;

    public sealed class RequestResultDto {

        public RequestResultDto(HttpStatusCode resultCode, TimeSpan requestDuration) {
            ResultCode = resultCode;
            RequestDuration = requestDuration;
        }

        public HttpStatusCode ResultCode { get; }

        public TimeSpan RequestDuration { get; }
    }
}
