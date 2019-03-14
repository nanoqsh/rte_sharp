using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine
{
    class DirectionalLight
    {
        public readonly Color4 Color;
        public readonly Vector3 Direction;

        public DirectionalLight(
            Color4 color = new Color4(),
            Vector3 direction = new Vector3()
            )
        {
            Color = color;
            Direction = direction;
        }
    }
}
