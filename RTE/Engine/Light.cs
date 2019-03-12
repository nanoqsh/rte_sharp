using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine
{
    class Light
    {
        public readonly Color4 Color;
        public readonly Vector3 Position;

        public Light(
            Color4 color = new Color4(),
            Vector3 position = new Vector3()
            )
        {
            Color = color;
            Position = position;
        }
    }
}
