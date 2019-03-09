using RTE.Engine.Shaders;

namespace RTE.Engine.Materials
{
    abstract class Material
    {
        public readonly string Name;
        public readonly ShaderProgram Shader;

        public Material(string name, ShaderProgram shader)
        {
            Name = name;
            Shader = shader;
        }
    }
}
