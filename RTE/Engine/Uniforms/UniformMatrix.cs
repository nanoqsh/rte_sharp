using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace RTE.Engine.Uniforms
{
    class UniformMatrix4 : Uniform
    {
        public Matrix4 Matrix;

        public UniformMatrix4(int index)
            : this(index, Matrix4.Identity)
        {
        }

        public UniformMatrix4(int index, Matrix4 matrix)
            : base(index)
        {
            Matrix = matrix;
        }

        public override void Bind()
        {
            GL.UniformMatrix4(Index, false, ref Matrix);
        }
    }
}
