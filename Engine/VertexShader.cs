using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
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
