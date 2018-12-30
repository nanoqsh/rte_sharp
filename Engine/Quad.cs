

namespace OpenGLEngine.Engine
{
    static class Quad
    {
        public static ArrayObject<Vertex4D> Make()
        {
            return new ArrayObject<Vertex4D>(
                new Vertex4D(-1.0f, -1.0f, 0.0f, 0.0f),
                new Vertex4D(-1.0f, 1.0f, 0.0f, 1.0f),
                new Vertex4D(1.0f, 1.0f, 1.0f, 1.0f),
                new Vertex4D(1.0f, -1.0f, 1.0f, 0.0f)
                )
                .SetDrawMode(DrawMode.Quads);
        }
    }
}
