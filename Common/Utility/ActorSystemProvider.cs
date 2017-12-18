namespace Common.Utility {
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;
    using Akka.Actor;
    using Akka.Configuration;

    public static class ActorSystemProvider {

        public const string ACTOR_SYSTEM_NAME = "FuppSystem";

        private static readonly Lazy<ActorSystem> ACTOR_SYSTEM_LAZY = new Lazy<ActorSystem>(CreateActorSystem);

        public static ActorSystem ActorSystem => ACTOR_SYSTEM_LAZY.Value;

        private static ActorSystem CreateActorSystem() {
            if(Environment.GetEnvironmentVariable("ENV") != "NETCORE") {
                return ActorSystem.Create(ACTOR_SYSTEM_NAME);
            }
            else {
                string hocon = File.ReadAllText("netcore-akka-base.conf");
                hocon = hocon.Replace("SEED_NODE_1", Environment.GetEnvironmentVariable("SEED_NODE_1"));
                hocon = hocon.Replace("SEED_NODE_2", Environment.GetEnvironmentVariable("SEED_NODE_2"));
                hocon = hocon.Replace("ENV_ROLE", Environment.GetEnvironmentVariable("ENV_ROLE"));
                hocon = hocon.Replace("ENV_PORT", Environment.GetEnvironmentVariable("ENV_PORT"));
                hocon = hocon.Replace("ENV_HOSTNAME", GetLocalIpAddr());
                Config config = ConfigurationFactory.ParseString(hocon); 
                return ActorSystem.Create(ACTOR_SYSTEM_NAME, config);
            }
        }

        private static string GetLocalIpAddr() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList) {
                if(ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            return "0.0.0.0";
        }
    }
}
