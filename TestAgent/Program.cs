namespace TestAgent {
    using System;
    using Akka.Actor;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");
            using (var system = ActorSystemProvider.ActorSystem)
            {
                //TODO remove (only for testing)
                var testRunCoordinator = system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");
                testRunCoordinator.Tell(new StartNewLoadTest()
                {
                    NumberOfAgents = 2,
                    Url = "https://www.google.at",
                    RequestsPerAgentCount = 2
                }
                );

                Console.ReadLine();
                system.Terminate();
                system.WhenTerminated.Wait();
            }
        }
    }
}
