using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Routing;
using Akka.Routing;
using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Actors
{
    public sealed class TestRunActor : ReceiveActor
    {

        private readonly Guid id;

        public TestRunActor(StartNewLoadTest message)
        {
            Console.WriteLine($"TestRun created");

            ClusterRouterPool config = new ClusterRouterPool(new BroadcastPool(message.NumberOfAgents),
                                   new ClusterRouterPoolSettings(message.NumberOfAgents, message.NumberOfAgents, true, Constants.Roles.AGENT));
            Context.ActorOf(Props.Create(() => new AgentActor(id, message.RequestsPerAgentCount))
                .WithRouter(config));

        }


    }
}
