using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class VertexShader : Shader
    {
        public VertexShader(string shaderName)
            : base(shaderName)
        {
        }

        protected override ShaderType ShaderType => ShaderType.VertexShader;
    }
}
