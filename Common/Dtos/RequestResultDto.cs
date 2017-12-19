namespace Common.Dtos {
    using System;
    using System.Net;

    public sealed class RequestResultDto {

        public RequestResultDto(HttpStatusCode? resultCode, TimeSpan requestDuration, string requestid) {
            ResultCode = resultCode;
            RequestDuration = requestDuration;
            Requestid = requestid;
        }

        public HttpStatusCode? ResultCode { get; }

        public TimeSpan RequestDuration { get; }
        public string Requestid { get; }
    }
}
