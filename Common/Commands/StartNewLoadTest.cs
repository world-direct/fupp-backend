namespace Common.Commands {

    public sealed class StartNewLoadTest {

        public StartNewLoadTest(string url, int numberOfAgents, int numberOfRequestsPerAgent) {
            Url = url;
            NumberOfAgents = numberOfAgents;
            NumberOfRequestsPerAgent = numberOfRequestsPerAgent;
        }

        public string Url { get; }
        
        public int NumberOfAgents { get; }

        public int NumberOfRequestsPerAgent { get; }
    }
}
