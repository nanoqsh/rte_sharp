namespace RTE.OBJReader.OBJFormat
{
    public class LineUnitV : ILineUnit
    {
        public readonly int V;
        
        public LineUnitV(int v)
        {
            V = v;
        }
        
        public bool ContainsZero()
        {
            return V == 0;
        }

        public override string ToString()
        {
            return V.ToString();
        }

        public override bool Equals(object obj)
        {
            LineUnitV ob = (LineUnitV) obj;

            return V == ob.V;
        }

        protected bool Equals(LineUnitV other)
        {
            return V == other.V;
        }

        public override int GetHashCode()
        {
            return V;
        }
    }
}
