

namespace RTE.OBJReader.Exceptions
{
    public class ArgsRangeException : ParseException
    {
        public readonly int From;
        public readonly int To;
        public readonly int Received;

        public ArgsRangeException(int from, int to, int received)
            : base($"Expected from {@from} to {to} arguments, {received} received!")
        {
            From = from;
            To = to;
            Received = received;
        }
    }
}
