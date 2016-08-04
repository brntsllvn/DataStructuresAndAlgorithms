namespace zChainHashing
{
    public class InputPair
    {
        public string Command { get; set; }
        public string Value { get; set; }

        public InputPair(string command, string value)
        {
            Command = command;
            Value = value;
        }
    }
}