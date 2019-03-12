using RTE.Engine.MaterialRenderers;

namespace RTE.Engine.Materials
{
    class MaterialTransparent : Material
    {
        public readonly Texture Diffuse;
        public readonly float Opacity;

        public MaterialTransparent(
            string name,
            Texture diffuse,
            float opacity
            )
            : base(name)
        {
            Diffuse = diffuse;
            Opacity = opacity;
        }

        private TransparentRenderer renderer;
        public override MaterialRenderer Renderer
        {
            get => renderer ?? (renderer = new TransparentRenderer(this));
        }
    }
}
