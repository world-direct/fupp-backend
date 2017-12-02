using Akka.Actor;
using Common.Actors;
using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAgent.Actors
{
    public sealed class TestRunCoordinator : ReceiveActor
    {

        public TestRunCoordinator()
        {
            Console.WriteLine($"TestRunCoordinator created");
            Receive<StartNewLoadTest>(message => StartNewLoadTest(message));
        }

        private void StartNewLoadTest(StartNewLoadTest message)
        {
           var testRunActor = Context.ActorOf(Props.Create(() => new TestRunActor(message)));
            testRunActor.Forward(message);
        }

    }
}
