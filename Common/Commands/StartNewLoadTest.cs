namespace Common.Commands {

    public sealed class StartNewLoadTest {

        public StartNewLoadTest(string url, int numberOfAgents, int numberOfRequestsPerAgent) {
            NumberOfAgents = numberOfAgents;
            NumberOfRequestsPerAgent = numberOfRequestsPerAgent;
        }

        public string Url { get; set; }
        
        public int NumberOfAgents { get; }

        public int NumberOfRequestsPerAgent { get; }
    }
}
