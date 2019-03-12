using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine
{
    static class ColorExtension
    {
        public static Vector3 ToVector3(this Color4 color)
        {
            return new Vector3(color.R, color.G, color.B);
        }

        public static Color4 ToColor4(this Vector3 vector)
        {
            return new Color4(vector.X, vector.Y, vector.Z, 1.0f);
        }
    }
}
