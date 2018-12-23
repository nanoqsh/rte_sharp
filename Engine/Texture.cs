using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
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
                throw new FileNotFoundException(string.Format("File {0} not found!", path));

            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Index);

            Bitmap bitmap = new Bitmap(path);
            BitmapData bitdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                bitdata.Width,
                bitdata.Height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                bitdata.Scan0
                );

            bitmap.UnlockBits(bitdata);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.ClampToBorder
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.ClampToBorder
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

        public void Dispose()
        {
            GL.DeleteTexture(Index);
        }
    }
}
