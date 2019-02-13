namespace RTE.OBJReader.OBJFormat
{
    public class FaceUnitVTN : IFaceUnit
    {
        public readonly int V;
        public readonly int T;
        public readonly int N;
        
        public FaceUnitVTN(int v, int t, int n)
        {
            V = v;
            T = t;
            N = n;
        }

        public override string ToString()
        {
            return $"{V}/{T}/{N}";
        }

        public bool ContainsZero()
        {
            return V == 0 || T == 0 || N == 0;
        }

        public override bool Equals(object obj)
        {
            FaceUnitVTN ob = (FaceUnitVTN) obj;
            
            return V == ob.V && T == ob.T && N == ob.N;
        }

        protected bool Equals(FaceUnitVTN other)
        {
            return V == other.V && T == other.T && N == other.N;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = V;
                hashCode = (hashCode * 397) ^ T;
                hashCode = (hashCode * 397) ^ N;
                return hashCode;
            }
        }
    }
}
