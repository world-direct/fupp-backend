namespace Common.Actors {
    using System;
    using System.Net;
    using Akka.Actor;
    using Events;

    public sealed class SenderActor : ReceiveActor {

        private const int TIMEOUT = 10000;
        private readonly Guid id;

        private readonly string url;
        private DateTime startTime;

        public SenderActor(string url) {
            this.url = url;
            id = Guid.NewGuid();

            Receive<HttpResponseReceived>(m => HandleHttpResponseReceived(m));

            MakeRequest();
        }

        private void MakeRequest() {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Timeout = TIMEOUT;

            startTime = DateTime.Now;
            request.GetResponseAsync().ContinueWith(result => new HttpResponseReceived(((HttpWebResponse) result.Result).StatusCode)).PipeTo(Self);
        }

        private void HandleHttpResponseReceived(HttpResponseReceived m) {
            Context.Parent.Tell(new RequestFinished(id, m.StatusCode, DateTime.Now - startTime));
        }
    }
}
