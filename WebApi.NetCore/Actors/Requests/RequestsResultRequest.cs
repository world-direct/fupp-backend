public class RequestsResultRequest {
   public string testId { get; set; }

    public string agentId { get; set; }


    public RequestsResultRequest(string testId, string agentId) {
        this.testId = testId;
        this.agentId = agentId;
    }
}
