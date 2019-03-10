using OpenTK.Graphics;

namespace RTE.Engine.Materials
{
    class EmissiveMaterial : Material
    {
        public override ShaderProgram Shader => MaterialShaders.EmissiveMeshShader;

        private readonly UniformTexture uniformTexture;
        private readonly UniformColor uniformLightColor;

        public EmissiveMaterial(string name)
            : this(name, new Texture("EmptyTexture.png"), Color4.White)
        {
        }

        public EmissiveMaterial(string name, Texture texture)
            : this(name, texture, Color4.White)
        {
        }

        public EmissiveMaterial(string name, Texture texture, Color4 lightColor)
            : base(name)
        {
            uniformTexture = new UniformTexture(
                Shader.GetAttributeIndex("tex"),
                texture,
                0
                );

            uniformLightColor = new UniformColor(
                Shader.GetUniformIndex("lightColor"),
                lightColor
                );
        }

        public override void Bind()
        {
            uniformTexture.Bind();
            uniformLightColor.Bind();
        }
    }
}
