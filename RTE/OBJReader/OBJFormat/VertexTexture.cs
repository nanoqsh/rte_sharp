using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class VertexTexture : VertexData
    {
        public readonly float[] Data;

        public VertexTexture(string[] args) : base(args)
        {
            if (args.Length < 1 || args.Length > 3)
                throw new ArgsRangeException(1, 3, args.Length);

            Data = Parser.ToFloatArray(args);
        }

        public override string ToString()
        {
            return "vt " + Writer.FloatArrayToString(Data);
        }
    }
}
