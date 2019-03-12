using RTE.Engine.Materials;

namespace RTE.Engine.MaterialRenderers
{
    class GouraudRenderer : DefaultRenderer
    {
        public GouraudRenderer(MaterialDefault material) : base(material)
        {
        }

        public override ShaderProgram Shader => MaterialShaders.Gouraud;
    }
}
