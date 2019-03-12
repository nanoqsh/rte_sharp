using OpenTK.Graphics;
using RTE.Engine.MaterialRenderers;

namespace RTE.Engine.Materials
{
    class MaterialEmissive : Material
    {
        public readonly Texture texture;
        public readonly Color4 lightColor;

        public MaterialEmissive(string name)
            : this(name, new Texture("EmptyTexture.png"), Color4.White)
        {
        }

        public MaterialEmissive(string name, Texture texture)
            : this(name, texture, Color4.White)
        {
        }

        public MaterialEmissive(string name, Texture texture, Color4 lightColor)
            : base(name)
        {
            this.texture = texture;
            this.lightColor = lightColor;
        }

        private EmmissiveRenderer renderer;
        public override MaterialRenderer Renderer
        {
            get => renderer ?? (renderer = new EmmissiveRenderer(this));
        }
    }
}
