using System.Collections.Generic;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class MapLibrary : OBJType
    {
        public readonly IEnumerable<string> FileNames;
    
        public MapLibrary(string[] args) : base(args)
        {
            if (args.Length < 1)
                throw new ArgsMinAmountException(1, args.Length);
            
            FileNames = args;
        }

        public override string ToString()
        {
            return "maplib " + string.Join(" ", FileNames);
        }
    }
}
