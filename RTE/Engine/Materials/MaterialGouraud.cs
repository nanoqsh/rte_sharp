using OpenTK.Graphics;
using RTE.Engine.MaterialRenderers;

namespace RTE.Engine.Materials
{
    class MaterialGouraud : MaterialDefault
    {
        public MaterialGouraud(
            string name,
            Texture diffuse,
            Color4 specular,
            float shininess = 16
            )
            : base(name, diffuse, specular, shininess)
        {
        }

        private GouraudRenderer renderer;
        public override MaterialRenderer Renderer
        {
            get => renderer ?? (renderer = new GouraudRenderer(this));
        }
    }
}
