using OpenTK;
using RTE.Engine.Uniforms;
using RTE.Engine.Materials;

namespace RTE.Engine.MaterialRenderers
{
    class EmmissiveRenderer : MaterialRenderer
    {
        private readonly UniformTexture texture;
        private readonly UniformColor4 lightColor;

        public EmmissiveRenderer(MaterialEmissive material)
            : base(material)
        {
            texture = new UniformTexture(
                Shader.GetUniformIndex("tex"),
                material.texture,
                0
                );

            lightColor = new UniformColor4(
                Shader.GetUniformIndex("lightColor"),
                material.lightColor
                );
        }

        public override ShaderProgram Shader => MaterialShaders.Emissive;

        public override void Bind()
        {
            texture.SetUniform();
            lightColor.SetUniform();
        }

        public override void BindAmbient(Vector3 viewPosition, Vector3 ambient)
        {
        }

        public override void BindLight(Light light)
        {
        }
    }
}
