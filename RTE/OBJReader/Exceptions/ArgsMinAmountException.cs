

namespace RTE.OBJReader.Exceptions
{
    public class ArgsMinAmountException : ParseException
    {
        public readonly int Min;
        public readonly int Received;

        public ArgsMinAmountException(int min, int received)
            : base($"Expected at least {min} arguments, {received} received!")
        {
            Min = min;
            Received = received;
        }
    }
}
