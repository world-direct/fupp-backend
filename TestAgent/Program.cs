using Akka.Actor;
using System;
using TestAgent.Actors;

namespace TestAgent {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");
            using (var system = ActorSystem.Create("TestAgent"))
            {
                //TODO remove (only for testing)
                system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");

                system.WhenTerminated.Wait();
            }
        }
    }
}
