using OpenTK.Graphics.OpenGL4;
using System;
namespace RTE.Engine
{
    enum DrawMode
    {
        Triangles,
        Quads
    }

    static class DrawModeExtension
    {
        public static PrimitiveType GetPrimitiveType(this DrawMode mode)
        {
            switch (mode)
            {
                case DrawMode.Quads:
                    return PrimitiveType.Quads;

                case DrawMode.Triangles:
                    return PrimitiveType.Triangles;

                default:
                    throw new ArgumentException("Undefined DrawMode!");
            }
        }

        public static BeginMode GetBeginMode(this DrawMode mode)
        {
            switch (mode)
            {
                case DrawMode.Quads:
                    return BeginMode.Quads;

                case DrawMode.Triangles:
                    return BeginMode.Triangles;

                default:
                    throw new ArgumentException("Undefined DrawMode!");
            }
        }
    }
}
