using Akka.Actor;
using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAgent.Actors
{
    public class TestRunCoordinator : ReceiveActor
    {
        

        public TestRunCoordinator()
        {
            Console.WriteLine($"TestRunCoordinator created");
            Receive<StartNewLoadTest>(message => StartNewLoadTest(message));
        }

        private void StartNewLoadTest(StartNewLoadTest message)
        {
            
        }

    }
}
