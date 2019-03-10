
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class UniformInt : Uniform
    {
        public readonly int Value;

        public UniformInt(int index)
            : this(index, 0)
        {
        }

        public UniformInt(int index, int value)
            : base(index)
        {
            Value = value;
        }

        public override void Bind()
        {
            GL.Uniform1(Index, Value);
        }
    }
}
