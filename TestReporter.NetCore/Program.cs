namespace TestCoordinator {
    using System;
    using Common.Utility;

    public class Program {

        private static void Main(string[] args) {
            using (var system = ActorSystemProvider.ActorSystem) {
                Console.WriteLine("I'm a TestReporter");
                Console.ReadLine();
            }
        }
    }
}
