using OpenTK.Graphics.OpenGL4;
using OpenTK;


namespace OpenGLEngine.Engine
{
    class UniformMatrix : Uniform
    {
        public Matrix4 Matrix;

        public UniformMatrix(string name, Matrix4 matrix)
            : base(name)
        {
            Matrix = matrix;
        }

        public override void Bind(int index)
        {
            GL.UniformMatrix4(index, false, ref Matrix);
        }
    }
}
