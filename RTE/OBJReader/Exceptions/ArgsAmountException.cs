

namespace RTE.OBJReader.Exceptions
{
    public class ArgsAmountException : ParseException
    {
        public readonly int Expected;
        public readonly int Received;

        public ArgsAmountException(int expected, int received)
            : base($"Expected {expected} arguments, {received} received!")
        {
            Expected = expected;
            Received = received;
        }
    }
}
