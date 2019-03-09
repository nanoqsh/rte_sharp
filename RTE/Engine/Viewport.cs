using OpenTK;
using System;

namespace RTE.Engine
{
    class Viewport
    {
        private Rectangle size;
        public Rectangle Size { get => size; }

        private int pixelSize;
        public int PixelSize { get => pixelSize; }

        public delegate void OnResizeHandler(Rectangle size);

        public event OnResizeHandler OnResize;
        
        private static readonly Viewport instance;
        public static Viewport Instance
        {
            get => instance;
        }

        static Viewport()
        {
            instance = new Viewport();
        }

        private Viewport()
        {
            size = new Rectangle();
            pixelSize = 1;
        }

        public Viewport Resize(Rectangle size)
        {
            this.size = size;

            OnResize?.Invoke(size);

            return this;
        }

        public Viewport SetPixelSize(int pixelSize)
        {
            if (pixelSize < 1)
                throw new ArgumentException($"pixelSize must be at least 1");

            this.pixelSize = pixelSize;

            return this;
        }
    }
}
