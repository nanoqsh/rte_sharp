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
            Vector3 specularColor,
            float shininess
            )
            : base(name, texture, lightPosition, lightColor, specularColor, shininess)
        {
        }

        public override ShaderProgram Shader => MaterialShaders.GouraudMeshShader;
    }
}
