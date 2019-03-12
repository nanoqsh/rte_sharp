using OpenTK;
using RTE.Engine.Materials;
using RTE.Engine.Uniforms;

namespace RTE.Engine.MaterialRenderers
{
    class SolidRenderer : MaterialRenderer
    {
        private readonly UniformVector3 color;
        private readonly UniformVector3 specular;
        private readonly UniformFloat shininess;

        private readonly UniformVector3 viewPosition;
        private readonly UniformVector3 ambient;

        public SolidRenderer(MaterialSolid material)
            : base(material)
        {
            color = new UniformVector3(
                Shader.GetUniformIndex("color"),
                material.Color
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

        public override ShaderProgram Shader => MaterialShaders.Solid;

        public override void Bind()
        {
            color.SetUniform();
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

        public override void BindLight(Light light)
        {
            if (LightRenderer == null)
                LightRenderer = new LightRenderer(light, Shader);

            LightRenderer.Bind();
        }
    }
}
