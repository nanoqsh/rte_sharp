using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
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
