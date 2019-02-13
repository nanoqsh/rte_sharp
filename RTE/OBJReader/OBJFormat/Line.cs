using System.Collections.Generic;
using System.Linq;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class Line : Element
    {
        public readonly List<ILineUnit> Units = new List<ILineUnit>();

        public Line(string[] args) : base(args)
        {
            if (args.Length < 2)
                throw new ArgsMinAmountException(2, args.Length);

            foreach (string arg in args)
            {
                ILineUnit line = ParseUnit(arg);
                
                if (line.ContainsZero())
                    throw new ParseException("Line must not contain zero!");
                
                Units.Add(line);
            }

            if (!AllFormatsAreSame())
                throw new ParseException("All arguments must be same format!");
        }

        private static ILineUnit ParseUnit(string text)
        {
            string[] data = text.Split('/');

            switch (data.Length)
            {
                case 1:
                    return new LineUnitV(
                        int.Parse(data[0])
                        );
                
                case 2:
                    return new LineUnitVT(
                        int.Parse(data[0]),
                        int.Parse(data[1])
                        );
                
                default:
                    throw new ParseException("Unknown line format!");
            }
        }
        
        private bool AllFormatsAreSame()
        {
            ILineUnit unit = Units[0];

            return Units
                .Skip(1)
                .All(u => u.GetType() == unit.GetType());
        }

        public override string ToString()
        {
            return "l " + string.Join(" ", Units);
        }
    }
}
