using OpenTK.Graphics;
using RTE.Engine.MaterialRenderers;

namespace RTE.Engine.Materials
{
    class MaterialSolid : Material
    {
        public readonly Color4 Color;
        public readonly Color4 Specular;
        public readonly float Shininess;

        public MaterialSolid(
            string name,
            Color4 color,
            Color4 specular,
            float shininess = 16.0f
            )
            : base(name)
        {
            Color = color;
            Specular = specular;
            Shininess = shininess;
        }

        private SolidRenderer renderer;
        public override MaterialRenderer Renderer
        {
            get => renderer ?? (renderer = new SolidRenderer(this));
        }
    }
}
