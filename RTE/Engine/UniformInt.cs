
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class UniformInt : Uniform
    {
        public readonly int Value;

        public UniformInt(string name, int value)
            : base(name)
        {
            Value = value;
        }

        public override void Bind(int index)
        {
            GL.Uniform1(index, Value);
        }
    }
}
