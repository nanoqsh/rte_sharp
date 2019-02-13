using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class UseMap : OBJType
    {
        public readonly string Name;
        public readonly bool Use = true;
        
        public UseMap(string[] args) : base(args)
        {
            if (args.Length != 1)
                throw new ArgsAmountException(1, args.Length);

            Name = args[0];

            if (args[0] == "off")
                Use = false;
        }

        public override string ToString()
        {
            return "usemap " + Name;
        }
    }
}
