using OpenTK.Graphics.OpenGL4;
using System;

namespace RTE.Engine
{
    class UniformTexture : Uniform
    {
        public readonly Texture Texture;
        public readonly int Unit;

        public UniformTexture(int index, Texture texture, int unit)
            : base(index)
        {
            if (unit < 0 || unit > 31)
                throw new Exception("Unit must be from 0 to 32 !");

            Texture = texture;
            Unit = unit;
        }

        public override void Bind()
        {
            GL.Uniform1(Index, Unit);
            GL.ActiveTexture(TextureUnit.Texture0 + Unit);
            GL.BindTexture(TextureTarget.Texture2D, Texture.Index);
        }
    }
}
