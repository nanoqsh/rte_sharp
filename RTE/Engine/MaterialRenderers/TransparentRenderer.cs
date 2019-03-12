using OpenTK;
using RTE.Engine.Materials;
using RTE.Engine.Uniforms;

namespace RTE.Engine.MaterialRenderers
{
    class TransparentRenderer : MaterialRenderer
    {
        private readonly UniformTexture diffuse;
        private readonly UniformFloat opacity;

        private readonly UniformVector3 ambient;

        public TransparentRenderer(MaterialTransparent material)
            : base(material)
        {
            diffuse = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                material.Diffuse,
                0
                );

            opacity = new UniformFloat(
                Shader.GetUniformIndex("opacity"),
                material.Opacity
                );

            ambient = new UniformVector3(
                Shader.GetUniformIndex("ambient"),
                Vector3.Zero
                );
        }

        public override ShaderProgram Shader => MaterialShaders.Transparent;

        public override void Bind()
        {
            diffuse.SetUniform();
            opacity.SetUniform();
        }

        public override void BindAmbient(Vector3 viewPosition, Vector3 ambient)
        {
            this.ambient.Value = ambient;
            this.ambient.Bind();
        }

        private LightRenderer LightRenderer;

        public override void BindLight(Light light)
        {
            if (LightRenderer == null)
                LightRenderer = new LightRenderer(light, Shader);

            LightRenderer.Bind();
        }
    }
}
