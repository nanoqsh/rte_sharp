using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class FragmentShader : Shader
    {
        public FragmentShader(string shaderName)
            : base(shaderName)
        {
        }

        protected override ShaderType ShaderType => ShaderType.FragmentShader;
    }
}
