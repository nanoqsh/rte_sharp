using OpenTK;

namespace RTE.Engine
{
    static class Quad
    {
        public static ArrayObject<Vector4> Make()
        {
            return new ArrayObject<Vector4>(
                new Vector4(1.0f, -1.0f, 1.0f, 0.0f),
                new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, 0.0f, 0.0f)
                )
                .SetDrawMode(DrawMode.Quads);
        }
    }
}
