using OpenTK.Graphics;

namespace RTE.Engine.Materials
{
    class EmissiveMaterial : Material
    {
        public override ShaderProgram Shader => MaterialShaders.EmissiveMeshShader;

        private readonly UniformTexture uniformTexture;
        private readonly int uniformTextureKey;

        private readonly UniformColor uniformLightColor;
        private readonly int uniformLightColorKey;

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
            uniformTexture = new UniformTexture("tex", texture, 0);
            Shader.AddUniforms(uniformTexture);
            uniformTextureKey = Shader.GetUniformIndex(uniformTexture.Name);

            uniformLightColor = new UniformColor("lightColor", lightColor);
            Shader.AddUniforms(uniformLightColor);
            uniformLightColorKey = Shader.GetUniformIndex(uniformLightColor.Name);
        }

        public override void Bind()
        {
            Shader.BindUniform(uniformTextureKey);
            Shader.BindUniform(uniformLightColorKey);
        }
    }
}
