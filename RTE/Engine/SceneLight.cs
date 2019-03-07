using OpenTK.Graphics;

namespace RTE.Engine
{
    class SceneLight
    {
        public readonly Color4 AmbientColor;

        public SceneLight() : this(new Color4(1, 1, 1, 0.5f))
        {
        }

        public SceneLight(Color4 ambientColor)
        {
            AmbientColor = ambientColor;
        }
    }
}
