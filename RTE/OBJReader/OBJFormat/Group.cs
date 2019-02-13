using System.Collections.Generic;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class Group : OBJType
    {
        public const string DefaultName = "default";
        
        public readonly IEnumerable<string> Names;
        
        public Group(string[] args) : base(args)
        {
            if (args.Length < 1)
                throw new ArgsMinAmountException(1, args.Length);
            
            Names = args;
        }

        public override string ToString()
        {
            return "g " + string.Join(" ", Names);
        }
    }
}
