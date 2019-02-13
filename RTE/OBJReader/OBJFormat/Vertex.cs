using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class Vertex : VertexData
    {
        public readonly float[] Data;

        public Vertex(string[] args) : base(args)
        {
            if (args.Length < 3 || args.Length > 4)
                throw new ArgsRangeException(3, 4, args.Length);

            Data = Parser.ToFloatArray(args);
        }

        public override string ToString()
        {
            return "v " + Writer.FloatArrayToString(Data);
        }
    }
}
