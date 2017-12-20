using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Akka.Actor;

namespace WebApi {
    using Common.Utility;

    public class WebApiApplication : System.Web.HttpApplication {



        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
           var actorSystem = ActorSystemProvider.ActorSystem;
            actorSystem.ActorOf(Props.Create<StartTestRequestActor>(), "StartTestRequest");
            actorSystem.ActorOf(Props.Create<TestResultsRequestActor>(), "TestResultsRequest");
            

        }

        protected void Application_End()
        {
            CoordinatedShutdown.Get(ActorSystemProvider.ActorSystem).Run().Wait(TimeSpan.FromSeconds(5));

        }
    }
}
