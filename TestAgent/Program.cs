namespace TestAgent {
    using System;
    using Akka.Actor;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");

            using (ActorSystem system = ActorSystemProvider.ActorSystem) {
                // system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");

                Console.ReadLine();
                system.Terminate();
                system.WhenTerminated.Wait();
            }
        }
    }
}
