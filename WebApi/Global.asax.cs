using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Akka.Actor;
using WebApi.TestActors;

namespace WebApi {
    public class WebApiApplication : System.Web.HttpApplication {

        public static ActorSystem ActorSystem { get; private set; }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            ActorSystem = ActorSystem.Create("fupp");
            var d = ActorSystem.ActorOf(Props.Create<MyTestActor>(), "TestActor");

        }

        protected void Application_End()
        {
            CoordinatedShutdown.Get(ActorSystem).Run().Wait(TimeSpan.FromSeconds(5));
        }
    }
}
