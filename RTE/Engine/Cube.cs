using RTE.Engine.Vectors;

namespace RTE.Engine
{
    static class Cube
    {
        public static ElementArrayObject<Vector5> MakeIndexed()
        {
            return new ElementArrayObject<Vector5>(
                    new Vector5(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                    new Vector5(0.5f, -0.5f, 0.5f, 0.0f, 1.0f),
                    new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 1.0f),
                    new Vector5(-0.5f, 0.5f, 0.5f, 1.0f, 0.0f),

                    new Vector5(-0.5f, -0.5f, -0.5f, 1.0f, 1.0f),
                    new Vector5(0.5f, -0.5f, -0.5f, 1.0f, 0.0f),
                    new Vector5(0.5f, 0.5f, -0.5f, 0.0f, 0.0f),
                    new Vector5(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f)
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

        public static ArrayObject<Vector5> Make()
        {
            return new ArrayObject<Vector5>(
                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 0.0f),
                new Vector5(0.5f, -0.5f, -0.5f, 1.0f, 0.0f),
                new Vector5(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 0.0f),

                new Vector5(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vector5(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 1.0f),
                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 1.0f),
                new Vector5(-0.5f, 0.5f, 0.5f, 0.0f, 1.0f),
                new Vector5(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),

                new Vector5(-0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(-0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vector5(-0.5f, 0.5f, 0.5f, 1.0f, 0.0f),

                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),

                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(0.5f, -0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(0.5f, -0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(-0.5f, -0.5f, 0.5f, 0.0f, 0.0f),
                new Vector5(-0.5f, -0.5f, -0.5f, 0.0f, 1.0f),

                new Vector5(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f),
                new Vector5(0.5f, 0.5f, -0.5f, 1.0f, 1.0f),
                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(0.5f, 0.5f, 0.5f, 1.0f, 0.0f),
                new Vector5(-0.5f, 0.5f, 0.5f, 0.0f, 0.0f),
                new Vector5(-0.5f, 0.5f, -0.5f, 0.0f, 1.0f)
                );
        }
    }
}
