using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class UniformColor : Uniform
    {
        public Color4 Color;

        public UniformColor(string name, Color4 color)
            : base(name)
        {
            Color = color;
        }

        public override void Bind(int index)
        {
            GL.Uniform4(index, Color);
        }
    }
}
