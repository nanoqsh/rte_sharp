namespace RTE.OBJReader.OBJFormat
{
    public class FaceUnitV : IFaceUnit
    {
        public readonly int V;
        
        public FaceUnitV(int v)
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
            return V == ((FaceUnitV)obj).V;
        }
        
        protected bool Equals(FaceUnitV other)
        {
            return V == other.V;
        }

        public override int GetHashCode()
        {
            return V;
        }
    }
}
