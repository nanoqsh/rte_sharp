using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class BufferObject<V>
        : IDisposable
        where V : struct
    {
        private int index;
        private readonly int count;
        private readonly BufferTarget BufferTarget;
        private ShaderAttribute[] shaderAttributes;

        public int Index
        {
            get => index;
        }

        public BufferObject(
            BufferTarget bufferTarget,
            V[] vertices
            )
        {
            BufferTarget = bufferTarget;
            count = vertices.Length;

            GL.GenBuffers(1, out index);
            GL.BindBuffer(bufferTarget, index);

            GL.BufferData(
                bufferTarget,
                vertices.Length * Marshal.SizeOf(typeof(V)),
                vertices,
                BufferUsageHint.StaticDraw
                );
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget, 0);

            foreach (ShaderAttribute attribute in shaderAttributes)
                GL.DisableVertexAttribArray(attribute.Index);

            GL.DeleteBuffers(1, ref index);
        }

        public void SetAttributes(params ShaderAttribute[] attributes)
        {
            shaderAttributes = attributes;

            GL.BindBuffer(BufferTarget, index);

            foreach (ShaderAttribute attribute in attributes)
            {
                GL.EnableVertexAttribArray(attribute.Index);
                GL.VertexAttribPointer(
                    attribute.Index,
                    attribute.SizeOfElements,
                    VertexAttribPointerType.Float,
                    false,
                    attribute.Stride,
                    attribute.Offset
                    );
            }
        }

        public void Draw()
        {
            GL.BindBuffer(BufferTarget, index);
            GL.DrawArrays(PrimitiveType.Quads, 0, count);
            GL.BindBuffer(BufferTarget, 0);
        }
    }
}
