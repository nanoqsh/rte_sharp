using System;

namespace RTE.OBJReader.Exceptions
{
    public class PackException : Exception
    {
        public PackException(string reason)
            : base($"Pack error: {reason}")
        {
        }
    }
}
