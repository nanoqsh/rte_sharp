using System.Collections.Generic;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class MaterialLibrary : OBJType
    {
        public readonly IEnumerable<string> FileNames;
    
        public MaterialLibrary(string[] args) : base(args)
        {
            if (args.Length < 1)
                throw new ArgsMinAmountException(1, args.Length);
            
            FileNames = args;
        }

        public override string ToString()
        {
            return "mtllib " + string.Join(" ", FileNames);
        }
    }
}
