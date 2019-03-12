using OpenTK;
using RTE.Engine.Uniforms;

namespace RTE.Engine.Materials
{
    class MaterialDefault : Material
    {
        public override ShaderProgram Shader => MaterialShaders.DefaultMeshShader;

        private readonly UniformTexture uniformDiffuse;
        private readonly Texture texture;

        private readonly UniformVector3 uniformLightPosition;
        private readonly UniformVector3 uniformLightColor;
        private readonly UniformVector3 uniformSpecularColor;
        private readonly UniformFloat uniformShininess;

        public MaterialDefault(string name)
            : this(
                  name,
                  new Texture("EmptyTexture.png"),
                  Vector3.Zero,
                  Vector3.One,
                  Vector3.One,
                  1f
                  )
        {
        }

        public MaterialDefault(
            string name,
            Texture texture,
            Vector3 lightPosition,
            Vector3 lightColor,
            Vector3 specularColor,
            float shininess
            )
            : base(name)
        {
            uniformDiffuse = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                texture,
                0
                );

            this.texture = texture;

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

            uniformShininess = new UniformFloat(
                Shader.GetUniformIndex("shininess"),
                1f
                );
        }

        public override void Bind()
        {
            uniformDiffuse.Value = texture;
            uniformDiffuse.Bind();
            uniformLightPosition.Bind();
            uniformLightColor.Bind();
            uniformSpecularColor.Bind();
            uniformShininess.Bind();
        }
    }
}
