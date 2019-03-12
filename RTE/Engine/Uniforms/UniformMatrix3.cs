using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace RTE.Engine.Uniforms
{
    class UniformMatrix3 : Uniform<Matrix3>
    {
        public UniformMatrix3(int index)
            : this(index, Matrix3.Identity)
        {
        }

        public UniformMatrix3(int index, Matrix3 value)
            : base(index, value)
        {
        }

        public override void SetUniform()
        {
            GL.UniformMatrix3(Index, false, ref value);
        }
    }
}
