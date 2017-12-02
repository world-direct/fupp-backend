using Akka.Actor;
using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Actors
{
    public class TestRun : ReceiveActor
    {

        public TestRun()
        {
            Console.WriteLine($"TestRun created");
            Receive<StartNewLoadTest>(message => StartNewLoadTest(message));
        }

        private void StartNewLoadTest(StartNewLoadTest message)
        {


        }
    }
}
