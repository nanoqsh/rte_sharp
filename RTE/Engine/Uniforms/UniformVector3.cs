using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Uniforms
{
    class UniformVector3 : Uniform
    {
        public Vector3 Vector;

        public UniformVector3(int index)
            : this(index, Vector3.Zero)
        {
        }

        public UniformVector3(int index, Color4 color)
            : this(index, new Vector3(color.R, color.G, color.B))
        {
        }

        public UniformVector3(int index, Vector3 vector)
            : base(index)
        {
            Vector = vector;
        }

        public override void Bind()
        {
            GL.Uniform3(Index, ref Vector);
        }
    }
}
