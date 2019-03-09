using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Shaders
{
    class ShaderFragment : Shader
    {
        public ShaderFragment(string shaderName)
            : base(shaderName)
        {
        }

        protected override ShaderType ShaderType => ShaderType.FragmentShader;
    }
}
