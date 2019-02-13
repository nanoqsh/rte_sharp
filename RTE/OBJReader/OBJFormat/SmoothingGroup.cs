using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class SmoothingGroup : OBJType
    {
        public readonly int Number;
        
        public SmoothingGroup(string[] args) : base(args)
        {
            if (args.Length != 1)
                throw new ArgsAmountException(1, args.Length);

            Number = args[0] == "off"
                ? 0
                : int.Parse(args[0]);
        }

        public override string ToString()
        {
            return "s " + Number;
        }
    }
}
