using OpenTK.Graphics.OpenGL4;
using System;

namespace RTE.Engine
{
    class UniformTexture : Uniform
    {
        public readonly Texture Texture;
        public readonly int Unit;

        public UniformTexture(string name, Texture texture, int unit)
            : base(name)
        {
            if (unit < 0 || unit > 31)
                throw new Exception("Unit must be from 0 to 32 !");

            Texture = texture;
            Unit = unit;
        }

        public override void Bind(int index)
        {
            GL.Uniform1(index, Unit);
            GL.ActiveTexture(TextureUnit.Texture0 + Unit);
            GL.BindTexture(TextureTarget.Texture2D, Texture.Index);
        }
    }
}
