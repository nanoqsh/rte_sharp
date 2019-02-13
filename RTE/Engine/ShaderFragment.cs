using OpenTK.Graphics.OpenGL4;
using RTE.Engine.Shaders;

namespace RTE.Engine
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
