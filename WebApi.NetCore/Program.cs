using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Common.Utility;
using Akka.Actor;

namespace WebApi.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
    {
            using (var actorSystem = ActorSystemProvider.ActorSystem) {
                actorSystem.ActorOf(Props.Create<StartTestRequestActor>(), "StartTestRequest");
                actorSystem.ActorOf(Props.Create<TestResultsRequestActor>(), "TestResultsRequest");
                BuildWebHost(args).Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
				.UseUrls("http://[::]:5000")
				.Build();
    }
}
