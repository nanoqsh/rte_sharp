using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformFloat : Uniform<float>
    {
        public UniformFloat(int index, float value)
            : base(index, value)
        {
        }

        protected override void SetUniform()
        {
            GL.Uniform1(Index, value);
        }
    }
}
