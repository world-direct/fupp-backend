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
                Config config = GetNetConfigWithEnvVars(); 
                return ActorSystem.Create(ACTOR_SYSTEM_NAME, config);
            }
        }

        private static Config GetNetConfigWithEnvVars() {
            Config baseConfig = ConfigurationFactory.ParseString(File.ReadAllText("netcore-akka-base.conf")); 
            Config akkaConfig = ConfigurationFactory.ParseString(File.ReadAllText("akka.conf")); 
            string injectedConfig = "";
            if(!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ENV_PUBLIC_HOSTNAME"))) {
                injectedConfig += string.Format("\nakka.remote.dot-netty.tcp.public-hostname = {0}", Environment.GetEnvironmentVariable("ENV_PUBLIC_HOSTNAME"));
            }
            injectedConfig += string.Format("\nakka.remote.dot-netty.tcp.hostname = {0}", GetLocalIpAddr());
            Console.WriteLine(injectedConfig);

            return ConfigurationFactory.ParseString(injectedConfig)
                                    .WithFallback(akkaConfig)
                                    .WithFallback(baseConfig);
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
