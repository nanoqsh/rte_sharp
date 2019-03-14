using OpenTK;
using RTE.Engine.Materials;
using RTE.Engine.Uniforms;

namespace RTE.Engine.MaterialRenderers
{
    class DefaultRenderer : MaterialRenderer
    {
        private readonly UniformTexture diffuse;
        private readonly UniformVector3 specular;
        private readonly UniformFloat shininess;

        private readonly UniformVector3 viewPosition;
        private readonly UniformVector3 ambient;

        public DefaultRenderer(MaterialDefault material)
            : base(material)
        {
            diffuse = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                material.Diffuse,
                0
                );

            specular = new UniformVector3(
                Shader.GetUniformIndex("specularColor"),
                material.Specular
                );

            shininess = new UniformFloat(
                Shader.GetUniformIndex("shininess"),
                material.Shininess
                );

            viewPosition = new UniformVector3(
                Shader.GetUniformIndex("viewPosition"),
                Vector3.Zero
                );

            ambient = new UniformVector3(
                Shader.GetUniformIndex("ambient"),
                Vector3.Zero
                );
        }

        public override ShaderProgram Shader => MaterialShaders.Default;

        public override void Bind()
        {
            diffuse.SetUniform();
            specular.SetUniform();
            shininess.SetUniform();
        }

        public override void BindAmbient(Vector3 viewPosition, Vector3 ambient)
        {
            this.viewPosition.Value = viewPosition;
            this.viewPosition.Bind();

            this.ambient.Value = ambient;
            this.ambient.Bind();
        }

        private LightRenderer LightRenderer;

        public override void BindLight(PointLight[] lights)
        {
            if (LightRenderer == null)
                LightRenderer = new LightRenderer(lights, Shader);

            LightRenderer.Bind();
        }
    }
}
