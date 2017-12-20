namespace Common.Actors {
    using System;
    using Akka.Actor;
    using Commands;

    public sealed class TestRunCoordinator : ReceiveActor {

        public TestRunCoordinator() {
            Console.WriteLine($"TestRunCoordinator created");
            Receive<StartNewLoadTest>(message => StartNewLoadTest(message));
        }

        private void StartNewLoadTest(StartNewLoadTest message) {
            Console.WriteLine("a.lsdkfvnadklsfvnmaxfnvm,dfnvdnfbdfnmbdfnmvdm,fvnmasdfnvm,afnvm,asdfbm,a<dbnm,adfnbm,adfnb,madfnbm,adfbnm,adfbn");
            var testRunActor = Context.ActorOf(Props.Create(() => new TestRunActor(message)));
            testRunActor.Forward(message);
        }
    }
}
