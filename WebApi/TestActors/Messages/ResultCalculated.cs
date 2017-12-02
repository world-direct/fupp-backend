namespace WebApi.TestActors.Messages {
    public class ResultCalculated {
        
        public int Result { get; }

        public ResultCalculated(int result) {
            Result = result;
        }
    }
}