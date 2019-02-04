using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class Texture : IDisposable
    {
        public readonly string Name;
        public readonly int Index;

        public Texture(string textureName)
        {
            Name = textureName;

            string path = Environment.CurrentDirectory + "/Textures/" + textureName;

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found!");

            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Index);

            Bitmap bitmap = new Bitmap(path);
            BitmapData bitData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                bitData.Width,
                bitData.Height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                bitData.Scan0
                );

            bitmap.UnlockBits(bitData);

            SetDefaultParameters();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public Texture(int width, int height, string name)
        {
            Name = name;

            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Index);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                width,
                height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                IntPtr.Zero
                );

            SetDefaultParameters();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Index);
        }

        private void SetDefaultParameters()
        {
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.ClampToEdge
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.ClampToEdge
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest
                );
        }
    }
}
