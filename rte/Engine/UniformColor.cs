using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace RTE.Engine
{
    class UniformColor : Uniform
    {
        public Color Color;

        public UniformColor(string name, Color color)
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
