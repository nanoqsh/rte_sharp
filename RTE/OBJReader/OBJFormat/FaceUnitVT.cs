namespace RTE.OBJReader.OBJFormat
{
    public class FaceUnitVT : IFaceUnit
    {
        public readonly int V;
        public readonly int T;

        public FaceUnitVT(int v, int t)
        {
            V = v;
            T = t;
        }

        public bool ContainsZero()
        {
            return V == 0 || T == 0;
        }

        public override string ToString()
        {
            return $"{V}/{T}";
        }

        public override bool Equals(object obj)
        {
            FaceUnitVT ob = (FaceUnitVT) obj;
            
            return V == ob.V && T == ob.T;
        }

        protected bool Equals(FaceUnitVT other)
        {
            return V == other.V && T == other.T;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (V * 397) ^ T;
            }
        }
    }
}
