using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformColor4 : Uniform
    {
        public Color4 Color;

        public UniformColor4(int index)
            : this(index, Color4.Black)
        {
        }

        public UniformColor4(int index, Color4 color)
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
