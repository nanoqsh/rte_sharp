using System;

namespace RTE.OBJReader.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(string reason)
            : base($"Parse error: {reason}")
        {
        }
    }
}
