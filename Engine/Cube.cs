

namespace OpenGLEngine.Engine
{
    static class Cube
    {
        public static ElementArrayObject<Vertex3D> MakeIndexed()
        {
            return new ElementArrayObject<Vertex3D>(
                    new Vertex3D(-0.5f, -0.5f, 0.5f),
                    new Vertex3D(0.5f, -0.5f, 0.5f),
                    new Vertex3D(0.5f, 0.5f, 0.5f),
                    new Vertex3D(-0.5f, 0.5f, 0.5f),

                    new Vertex3D(-0.5f, -0.5f, -0.5f),
                    new Vertex3D(0.5f, -0.5f, -0.5f),
                    new Vertex3D(0.5f, 0.5f, -0.5f),
                    new Vertex3D(-0.5f, 0.5f, -0.5f)
                ).CreateElementBuffer(
                    0, 1, 2,
                    2, 3, 0,

                    1, 5, 6,
                    6, 2, 1,

                    7, 6, 5,
                    5, 4, 7,

                    4, 0, 3,
                    3, 7, 4,

                    4, 5, 1,
                    1, 0, 4,

                    3, 2, 6,
                    6, 7, 3
                );
        }

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
