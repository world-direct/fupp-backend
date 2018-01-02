namespace TestCoordinator {
    using System;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            using (var system = ActorSystemProvider.ActorSystem) {
                //TODO remove (only for testing)
                Console.ReadLine();
                Console.WriteLine("I'm a TestCoordinator");
                Console.ReadLine();
            }
        }
    }
}
