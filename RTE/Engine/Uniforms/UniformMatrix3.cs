using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace RTE.Engine.Uniforms
{
    class UniformMatrix3 : Uniform
    {
        public Matrix3 Matrix;

        public UniformMatrix3(int index)
            : this(index, Matrix3.Identity)
        {
        }

        public UniformMatrix3(int index, Matrix3 matrix)
            : base(index)
        {
            Matrix = matrix;
        }

        public override void Bind()
        {
            GL.UniformMatrix3(Index, false, ref Matrix);
        }
    }
}
