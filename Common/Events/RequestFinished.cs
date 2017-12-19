namespace Common.Events {
    using System;
    using System.Net;

    public sealed class RequestFinished {
        
        public RequestFinished(string senderId, HttpStatusCode? resultCode, TimeSpan requestDuration) {
            SenderId = senderId;
            ResultCode = resultCode;
            RequestDuration = requestDuration;
        }

        public string SenderId { get; }

        public HttpStatusCode? ResultCode { get; }

        public TimeSpan RequestDuration { get; }
    }
}
