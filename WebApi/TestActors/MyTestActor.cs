using Akka.Actor;
using WebApi.TestActors.Messages;

namespace WebApi.TestActors {
    public class MyTestActor : ReceiveActor {
        public MyTestActor() {
            
            Receive<AddCommand>(x => HandleAddCommand(x));
            
        }

        private void HandleAddCommand(AddCommand addCommand) {
            Context.Sender.Tell(new ResultCalculated(addCommand.Summand1+addCommand.Summand2));
        }
    }
}