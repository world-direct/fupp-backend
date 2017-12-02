namespace Common.Utility {
    using System;
    using Akka.Actor;

    public static class ActorSystemProvider {

        public const string ACTOR_SYSTEM_NAME = "FuppSystem";

        private static readonly Lazy<ActorSystem> ACTOR_SYSTEM_LAZY = new Lazy<ActorSystem>(CreateActorSystem);

        public static ActorSystem ActorSystem => ACTOR_SYSTEM_LAZY.Value;

        private static ActorSystem CreateActorSystem() {
            return ActorSystem.Create(ACTOR_SYSTEM_NAME);
        }
    }
}
