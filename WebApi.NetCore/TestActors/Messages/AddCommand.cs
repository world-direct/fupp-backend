namespace WebApi.NetCore.TestActors.Messages {
    public class AddCommand {
        public int Summand1 { get; }
        public int Summand2 { get; }

        public AddCommand(int summand1, int summand2) {
            Summand1 = summand1;
            Summand2 = summand2;
        }
    }

}