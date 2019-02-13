using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class MergingGroup : OBJType
    {
        public readonly int Number = 0;
        public readonly float Resolution = 0;
        
        public MergingGroup(string[] args) : base(args)
        {
            if (args.Length < 1 || args.Length > 2)
                throw new ArgsRangeException(1, 2, args.Length);

            if (args[0] != "off")
                Number = int.Parse(args[0]);

            if (args.Length == 2)
                Resolution = Parser.ToFloat(args[1]);
        }

        public override string ToString()
        {
            return "mg "
                   + Number
                   + (Number == 0 ? "" : " " + Writer.FloatToString(Resolution));
        }
    }
}
