using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Shaders
{
    class ShaderVertex : Shader
    {
        public ShaderVertex(string shaderName)
            : base(shaderName)
        {
        }

        protected override ShaderType ShaderType => ShaderType.VertexShader;
    }
}
