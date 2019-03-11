using OpenTK;

namespace RTE.Engine.Materials
{
    class MaterialDefaultGouraud : MaterialDefault
    {
        public MaterialDefaultGouraud(
            string name,
            Texture texture,
            Vector3 lightPosition,
            Vector3 lightColor,
            Vector3 specularColor
            )
            : base(name, texture, lightPosition, lightColor, specularColor)
        {
        }

        public override ShaderProgram Shader => MaterialShaders.GouraudMeshShader;
    }
}
