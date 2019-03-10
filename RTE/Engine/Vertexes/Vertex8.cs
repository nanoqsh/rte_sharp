namespace RTE.Engine.Vertexes
{
    struct Vertex8
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float I;
        public readonly float J;
        public readonly float K;
        public readonly float U;
        public readonly float V;

        public Vertex8(
            float x,
            float y,
            float z,
            float i,
            float j,
            float k,
            float u,
            float v
            )
        {
            X = x;
            Y = y;
            Z = z;
            I = i;
            J = j;
            K = k;
            U = u;
            V = v;
        }
    }
}
