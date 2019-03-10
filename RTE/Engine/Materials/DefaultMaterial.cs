namespace RTE.Engine.Materials
{
    class DefaultMaterial : Material
    {
        public override ShaderProgram Shader => MaterialShaders.DefaultMeshShader;

        private readonly UniformTexture uniformTexture;

        public DefaultMaterial(string name)
            : this(name, new Texture("EmptyTexture.png"))
        {
        }

        public DefaultMaterial(string name, Texture texture)
            : base(name)
        {
            uniformTexture = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                texture,
                0
                );
        }

        public override void Bind()
        {
            uniformTexture.Bind();
        }
    }
}
