using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformVector3 : Uniform<Vector3>
    {
        public UniformVector3(int index)
            : this(index, Vector3.Zero)
        {
        }

        public UniformVector3(int index, Color4 color)
            : this(index, new Vector3(color.R, color.G, color.B))
        {
        }

        public UniformVector3(int index, Vector3 vector)
            : base(index, vector)
        {
        }

        public override void Bind()
        {
            if (isModified)
                GL.Uniform3(Index, ref value);

            isModified = false;
        }
    }
}
