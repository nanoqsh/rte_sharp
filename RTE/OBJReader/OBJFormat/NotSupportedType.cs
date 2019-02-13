using System.Collections.Generic;

namespace RTE.OBJReader.OBJFormat
{
    public class NotSupportedType : OBJType
    {
        public readonly IEnumerable<string> Data;
        
        public NotSupportedType(string[] args) : base(args)
        {
            Data = args;
        }

        public override string ToString()
        {
            return "NST: " + string.Join(" ", Data);
        }
    }
}
