namespace TestAgent {
    using System;
    using System.Threading;
    using Akka.Actor;
    using Common.Actors;
    using Common.Commands;
    using Common.Dtos;
    using Common.Queries;
    using Common.Utility;
    using Newtonsoft.Json;

    public class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Test AgentActor");
            using (var system = ActorSystemProvider.ActorSystem) {
                //TODO remove (only for testing)
                var testRunCoordinator = system.ActorOf(Props.Create<TestRunCoordinator>(), "testRunCoordinator");
                var testResultCache = system.ActorOf(Props.Create<TestResultCache>(), "testResultCache");
                Console.WriteLine("press <enter> to start tests");
                Console.ReadLine();
                testRunCoordinator.Tell(new StartNewLoadTest("http://wdat25097.world-direct.at", 3, 5));
                testRunCoordinator.Tell(new StartNewLoadTest("http://my.paylife.at", 3, 5));

                Thread.Sleep(2000); //doing really important work here ...
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" press <enter> to continue");
                Console.ReadLine();


                Console.WriteLine("reading test dictionary");
                //GUi.... simulated

                var testResults = testResultCache.Ask<TestResultDto[]>(new GetTestStatistics());

                testResults.Wait();

                foreach (var item in testResults.Result) {
                    Console.WriteLine($"test:\r\n{JsonConvert.SerializeObject(item)}");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("enter testrunid to get agent details -> ");
                string id = Console.ReadLine();

                var agentResults = testResultCache.Ask<AgentResultDto[]>(new GetAgentDataForTest(id));

                agentResults.Wait();

                Console.WriteLine(JsonConvert.SerializeObject(agentResults.Result));

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("get further data for specific agent from this test");
                Console.Write("agent id -> ");
                string agent = Console.ReadLine();

                var requests = testResultCache.Ask<RequestResultDto[]>(new GetIndividualStandardInfoForTestAgent(id, agent));

                requests.Wait();

                Console.WriteLine(JsonConvert.SerializeObject(requests.Result));

                Console.ResetColor();
                Console.WriteLine("press <enter> to terminate actor system");
                Console.ReadLine();
                system.Terminate();
                system.WhenTerminated.Wait();

                Console.WriteLine("System Terminated -- press <enter> to exit");
                Console.ReadLine();
            }
        }
    }
}
