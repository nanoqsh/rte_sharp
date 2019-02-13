using System.Linq;
using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class Point : Element
    {
        public readonly int[] VertexIndexes;
        
        public Point(string[] args) : base(args)
        {
            if (args.Length < 1)
                throw new ArgsMinAmountException(1, args.Length);

            VertexIndexes = args
                .Select(int.Parse)
                .Select(p =>
                {
                    if (p == 0)
                        throw new ParseException("Point must not contain zero!");

                    return p;
                })
                .ToArray();
        }

        public override string ToString()
        {
            return "p " + string.Join(" ", VertexIndexes);
        }
    }
}
