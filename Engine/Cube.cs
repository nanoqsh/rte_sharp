

namespace OpenGLEngine.Engine
{
    static class Cube
    {
        public static ArrayObject<Vertex5D> Make()
        {
            return new ArrayObject<Vertex5D>(
                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 0.0f),
                new Vertex5D(0.5f, -0.5f, -0.5f, 1.0f, 0.0f),
                new Vertex5D(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 0.0f),

                new Vertex5D(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vertex5D(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 1.0f),
                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 1.0f),
                new Vertex5D(-0.5f, 0.5f, 0.5f, 0.0f, 1.0f),
                new Vertex5D(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),

                new Vertex5D(-0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(-0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vertex5D(-0.5f, 0.5f, 0.5f, 1.0f, 0.0f),

                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),

                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(0.5f, -0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vertex5D(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),

                new Vertex5D(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex5D(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vertex5D(-0.5f, 0.5f, 0.5f, 0.0f, 0.0f),
                new Vertex5D(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f)
                );
        }
    }
}
