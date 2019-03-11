using OpenTK;
using RTE.Engine.Uniforms;

namespace RTE.Engine.Materials
{
    class MaterialDefault : Material
    {
        public override ShaderProgram Shader => MaterialShaders.DefaultMeshShader;

        private readonly UniformTexture uniformTexture;

        private readonly UniformVector3 uniformLightPosition;
        private readonly UniformVector3 uniformLightColor;
        private readonly UniformVector3 uniformSpecularColor;

        public MaterialDefault(string name)
            : this(
                  name,
                  new Texture("EmptyTexture.png"),
                  Vector3.Zero,
                  Vector3.One,
                  Vector3.One
                  )
        {
        }

        public MaterialDefault(
            string name,
            Texture texture,
            Vector3 lightPosition,
            Vector3 lightColor,
            Vector3 specularColor
            )
            : base(name)
        {
            uniformTexture = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                texture,
                0
                );

            uniformLightPosition = new UniformVector3(
                Shader.GetUniformIndex("lightPosition"),
                lightPosition
                );

            uniformLightColor = new UniformVector3(
                Shader.GetUniformIndex("lightColor"),
                lightColor
                );

            uniformSpecularColor = new UniformVector3(
                Shader.GetUniformIndex("specularColor"),
                specularColor
                );
        }

        public override void Bind()
        {
            uniformTexture.Bind();
            uniformLightPosition.Bind();
            uniformLightColor.Bind();
            uniformSpecularColor.Bind();
        }
    }
}
