using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine
{
    class PointLight
    {
        public readonly Color4 Color;
        public readonly Vector3 Position;

        public PointLight(
            Color4 color = new Color4(),
            Vector3 position = new Vector3()
            )
        {
            Color = color;
            Position = position;
        }
    }
}
