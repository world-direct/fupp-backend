namespace Common.Events {
    using System;
    using System.Net;

    public sealed class RequestFinished {
        
        public RequestFinished(Guid senderId, HttpStatusCode resultCode, TimeSpan requestDuration) {
            SenderId = senderId;
            ResultCode = resultCode;
            RequestDuration = requestDuration;
        }

        public Guid SenderId { get; }

        public HttpStatusCode ResultCode { get; }

        public TimeSpan RequestDuration { get; }
    }
}
