namespace RTE.OBJReader.OBJFormat
{
    public class FaceUnitVN : IFaceUnit
    {
        public readonly int V;
        public readonly int N;

        public FaceUnitVN(int v, int n)
        {
            V = v;
            N = n;
        }

        public bool ContainsZero()
        {
            return V == 0 || N == 0;
        }

        public override string ToString()
        {
            return $"{V}//{N}";
        }

        public override bool Equals(object obj)
        {
            FaceUnitVN ob = (FaceUnitVN) obj;
            
            return V == ob.V && N == ob.N;
        }

        protected bool Equals(FaceUnitVN other)
        {
            return V == other.V && N == other.N;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (V * 397) ^ N;
            }
        }
    }
}
