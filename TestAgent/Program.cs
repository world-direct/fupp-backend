using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestAgent.Actors;

namespace TestAgent {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Test Agent");
            using (var system = ActorSystem.Create("TestAgent"))
            {
                //TODO remove (only for testing)
                system.ActorOf(Props.Create<TestRunCoordinator>(), "test run coordinator");

                system.WhenTerminated.Wait();
            }
        }
    }
}
