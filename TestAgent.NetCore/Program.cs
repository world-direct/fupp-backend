namespace TestAgent {
    using System;
    using Akka.Actor;
    using Common.Actors;
    using Common.Commands;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");
            using (var system = ActorSystemProvider.ActorSystem) {
                //TODO remove (only for testing)
                var testRunCoordinator = system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");
                var testResultCache = system.ActorOf(Props.Create<TestResultCache>(), "testResultCache");

                Console.ReadLine();
                testRunCoordinator.Tell(new StartNewLoadTest("http://my.paylife.at", 3, 5));

                Console.ReadLine();
                system.Terminate();
                system.WhenTerminated.Wait();
            }
        }
    }
}
