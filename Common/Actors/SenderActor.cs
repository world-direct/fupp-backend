namespace Common.Actors {
    using System;
    using System.Net;
    using System.Net.Http;
    using Akka.Actor;
    using Events;

    public sealed class SenderActor : ReceiveActor {

        private const int TIMEOUT = 10;
        private readonly string id;

        private readonly IActorRef receiver;
        private readonly string url;
        private DateTime startTime;

        public SenderActor(IActorRef receiver, string url) {
            Console.WriteLine($"{nameof(SenderActor)} {Self.Path} created.");
            this.receiver = receiver;
            this.url = url;
            id = Guid.NewGuid().ToString();

            Receive<HttpResponseReceived>(m => HandleHttpResponseReceived(m));
            Receive<object>(m => Console.WriteLine("BITTE NICHT"));

            MakeRequest();
        }

        private void MakeRequest() {
            Console.WriteLine("uuuuuund");

            startTime = DateTime.Now;


            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0,0,0,TIMEOUT);
            client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)).ContinueWith(result => {
                Console.WriteLine("NAAAA");
                if (result.IsCanceled || result.IsFaulted) {
                    return new HttpResponseReceived(default(HttpStatusCode?));
                }
                return new HttpResponseReceived(result.Result.StatusCode);
            }).PipeTo(Self);

            Console.WriteLine("YEAHHHH");
        }

        private void HandleHttpResponseReceived(HttpResponseReceived m) {
            Console.WriteLine($"{Self.Path} received HTTP result: {m.StatusCode}.");
            receiver.Tell(new RequestFinished(id, m.StatusCode, DateTime.Now - startTime));
        }
    }
}
