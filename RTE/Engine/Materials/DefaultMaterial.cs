namespace RTE.Engine.Materials
{
    class DefaultMaterial : Material
    {
        public override ShaderProgram Shader => MaterialShaders.DefaultMeshShader;

        private readonly UniformTexture uniformTexture;
        private readonly Texture texture;
        private readonly int uniformTextureKey;

        public DefaultMaterial(string name)
            : this(name, new Texture("EmptyTexture.png"))
        {
        }

        public DefaultMaterial(string name, Texture texture)
            : base(name)
        {
            this.texture = texture;
            uniformTexture = new UniformTexture("tex", texture, 0);
            Shader.AddUniforms(uniformTexture);
            uniformTextureKey = Shader.GetUniformIndex(uniformTexture.Name);
        }

        public override void Bind()
        {
            Shader.BindUniform(uniformTextureKey);
        }
    }
}
