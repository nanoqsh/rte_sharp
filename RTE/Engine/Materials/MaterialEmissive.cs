using OpenTK.Graphics;
using RTE.Engine.Uniforms;

namespace RTE.Engine.Materials
{
    class MaterialEmissive : Material
    {
        public override ShaderProgram Shader => MaterialShaders.EmissiveMeshShader;

        private readonly UniformTexture uniformTexture;
        private readonly Texture texture;
        private readonly UniformColor4 uniformLightColor;

        public MaterialEmissive(string name)
            : this(name, new Texture("EmptyTexture.png"), Color4.White)
        {
        }

        public MaterialEmissive(string name, Texture texture)
            : this(name, texture, Color4.White)
        {
        }

        public MaterialEmissive(string name, Texture texture, Color4 lightColor)
            : base(name)
        {
            uniformTexture = new UniformTexture(
                Shader.GetAttributeIndex("tex"),
                texture,
                0
                );

            this.texture = texture;

            uniformLightColor = new UniformColor4(
                Shader.GetUniformIndex("lightColor"),
                lightColor
                );
        }

        public override void Bind()
        {
            uniformTexture.Value = texture;
            uniformTexture.Bind();
            uniformLightColor.Bind();
        }
    }
}
