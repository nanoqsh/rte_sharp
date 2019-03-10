using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class UniformColor : Uniform
    {
        public Color4 Color;

        public UniformColor(int index)
            : this(index, Color4.Black)
        {
        }

        public UniformColor(int index, Color4 color)
            : base(index)
        {
            Color = color;
        }

        public override void Bind()
        {
            GL.Uniform4(Index, Color);
        }
    }
}
