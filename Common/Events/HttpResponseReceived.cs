namespace Common.Events {
    using System.Net;

    public sealed class HttpResponseReceived {

        public HttpResponseReceived(HttpStatusCode? statusCode) {
            StatusCode = statusCode;
        }

        public HttpStatusCode? StatusCode { get; }
    }
}
