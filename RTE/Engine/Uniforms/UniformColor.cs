using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformColor4 : Uniform<Color4>
    {
        public UniformColor4(int index, Color4 value)
            : base(index, value)
        {
        }

        public override void Bind()
        {
            if (isModified)
                GL.Uniform4(Index, value);

            isModified = false;
        }
    }
}
