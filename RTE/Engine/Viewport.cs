using OpenTK;
using System;

namespace RTE.Engine
{
    static class Viewport
    {
        private static Rectangle size;
        public static Rectangle Size { get => size; }

        private static int pixelSize;
        public static int PixelSize { get => pixelSize; }

        public delegate void OnResizeHandler(Rectangle size);

        public static event OnResizeHandler OnResize;

        static Viewport()
        {
            size = new Rectangle();
            pixelSize = 1;
        }

        public static void Resize(Rectangle size)
        {
            Viewport.size = size;

            OnResize?.Invoke(size);
        }

        public static void SetPixelSize(int pixelSize)
        {
            if (pixelSize < 1)
                throw new ArgumentException($"pixelSize must be at least 1");

            Viewport.pixelSize = pixelSize;
        }
    }
}
