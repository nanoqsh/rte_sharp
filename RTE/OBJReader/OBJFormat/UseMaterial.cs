using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class UseMaterial : OBJType
    {
        public readonly string Name;
        
        public UseMaterial(string[] args) : base(args)
        {
            if (args.Length != 1)
                throw new ArgsAmountException(1, args.Length);

            Name = args[0];
        }

        public override string ToString()
        {
            return "usemtl " + Name;
        }
    }
}
