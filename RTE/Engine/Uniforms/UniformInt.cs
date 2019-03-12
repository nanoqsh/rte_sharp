using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformInt : Uniform<int>
    {
        public UniformInt(int index, int value)
            : base(index, value)
        {
        }

        public override void SetUniform()
        {
            GL.Uniform1(Index, value);
        }
    }
}
