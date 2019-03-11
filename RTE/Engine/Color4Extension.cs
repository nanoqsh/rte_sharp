using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine
{
    static class Color4Extension
    {
        public static Vector3 ToVector3(this Color4 color)
        {
            return new Vector3(color.R, color.G, color.B);
        }
    }
}
