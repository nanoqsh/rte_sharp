using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.OBJFormat
{
    public class VertexNormal : VertexData
    {
        public readonly float[] Data;
        
        public VertexNormal(string[] args) : base(args)
        {
            if (args.Length != 3)
                throw new ArgsAmountException(3, args.Length);
            
            Data = Parser.ToFloatArray(args);
        }
        
        public override string ToString()
        {
            return "vn " + Writer.FloatArrayToString(Data);
        }
    }
}
