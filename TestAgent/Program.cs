namespace TestAgent {
    using System;
    using Actors;
    using Akka.Actor;
    using Common.Commands;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");
            using (var system = ActorSystemProvider.ActorSystem) {
                //TODO remove (only for testing)
                var testRunCoordinator = system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");
                testRunCoordinator.Tell(new StartNewLoadTest("https://www.google.at", 5, 2));

                Console.ReadLine();
                system.Terminate();
                system.WhenTerminated.Wait();
            }
        }
    }
}
