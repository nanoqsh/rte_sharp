using System.Collections.Generic;
using System.Linq;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class Face : Element
    {
        public readonly List<IFaceUnit> Units = new List<IFaceUnit>();

        public Face(string[] args) : base(args)
        {
            if (args.Length < 3)
                throw new ArgsMinAmountException(3, args.Length);

            foreach (string arg in args)
            {
                IFaceUnit face = ParseUnit(arg);
                
                if (face.ContainsZero())
                    throw new ParseException("Face must not contain zero!");
                
                Units.Add(face);
            }

            if (!AllFormatsAreSame())
                throw new ParseException("All arguments must be same format!");
        }
        
        private static IFaceUnit ParseUnit(string text)
        {
            string[] data = text.Split('/');
            
            switch (data.Length)
            {
                case 1:
                    return new FaceUnitV(
                        int.Parse(text)
                    );
                
                case 2:
                    return new FaceUnitVT(
                        int.Parse(data[0]),
                        int.Parse(data[1])
                        );
                
                case 3:
                    if (string.IsNullOrEmpty(data[1]))
                        return new FaceUnitVN(
                            int.Parse(data[0]),
                            int.Parse(data[2])
                            );
                        
                    else
                        return new FaceUnitVTN(
                            int.Parse(data[0]),
                            int.Parse(data[1]),
                            int.Parse(data[2])
                            );
                
                default:
                    throw new ParseException("Unknown face format!");
            }
        }

        private bool AllFormatsAreSame()
        {
            IFaceUnit unit = Units[0];

            return Units
                .Skip(1)
                .All(u => u.GetType() == unit.GetType());
        }

        public override string ToString()
        {
            return "f " + string.Join(" ", Units);
        }
    }
}
