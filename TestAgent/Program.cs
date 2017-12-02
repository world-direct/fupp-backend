using Akka.Actor;
using Common.Commands;
using Common.Utility;
using System;
using TestAgent.Actors;

namespace TestAgent {
    class Program {
        static void Main(string[] args) {
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

                system.WhenTerminated.Wait();
            }
        }
    }
}
