

namespace OpenGLEngine.Engine
{
    struct Vertex5D
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float U;
        public readonly float V;

        public Vertex5D(float x, float y, float z, float u, float v)
        {
            X = x;
            Y = y;
            Z = z;
            U = u;
            V = v;
        }
    }
}
