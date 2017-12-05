namespace Common.Actors {
    using System;
    using System.Net;
    using Akka.Actor;
    using Events;

    public sealed class SenderActor : ReceiveActor {

        private const int TIMEOUT = 10000;
        private readonly Guid id;

        private readonly IActorRef receiver;
        private readonly string url;
        private DateTime startTime;

        public SenderActor(IActorRef receiver, string url) {
            Console.WriteLine($"{nameof(SenderActor)} {Self.Path} created.");
            this.receiver = receiver;
            this.url = url;
            id = Guid.NewGuid();

            Receive<HttpResponseReceived>(m => HandleHttpResponseReceived(m));
            Receive<object>(m => Console.WriteLine("BITTE NICHT"));

            MakeRequest();
        }

        private void MakeRequest() {
            Console.WriteLine("uuuuuund");
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Timeout = TIMEOUT;

            startTime = DateTime.Now;
            request.GetResponseAsync().ContinueWith(result => {
                Console.WriteLine("NAAAA");
                return new HttpResponseReceived(((HttpWebResponse) result.Result).StatusCode);
            }).PipeTo(Self);
            Console.WriteLine("YEAHHHH");
        }

        private void HandleHttpResponseReceived(HttpResponseReceived m) {
            Console.WriteLine($"{Self.Path} received HTTP result: {m.StatusCode}.");
            receiver.Tell(new RequestFinished(id, m.StatusCode, DateTime.Now - startTime));
        }
    }
}
