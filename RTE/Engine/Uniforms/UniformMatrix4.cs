using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace RTE.Engine.Uniforms
{
    class UniformMatrix4 : Uniform<Matrix4>
    {
        public UniformMatrix4(int index)
            : this(index, Matrix4.Identity)
        {
        }

        public UniformMatrix4(int index, Matrix4 value)
            : base(index, value)
        {
        }

        protected override void SetUniform()
        {
            GL.UniformMatrix4(Index, false, ref value);
        }
    }
}
