using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class BufferObject : IDisposable
    {
        private int index;
        private readonly int sizeOfElements;
        private readonly int count;
        private readonly BufferTarget BufferTarget;

        public int Index
        {
            get => index;
        }
        public int AttributeIndex { get; }

        public BufferObject(
            BufferTarget bufferTarget,
            Vertex2D[] vertices,
            int sizeOfElements,
            int attributeIndex
            )
        {
            BufferTarget = bufferTarget;
            this.sizeOfElements = sizeOfElements;
            count = vertices.Length;

            GL.GenBuffers(1, out index);
            GL.BindBuffer(bufferTarget, index);

            GL.BufferData(
                bufferTarget,
                vertices.Length * Marshal.SizeOf(typeof(Vertex2D)),
                vertices,
                BufferUsageHint.StaticDraw
                );


            // Set attribute
            GL.EnableVertexAttribArray(attributeIndex);
            GL.VertexAttribPointer(
                attributeIndex,
                sizeOfElements,
                VertexAttribPointerType.Float,
                false,
                0,
                0
                );

            AttributeIndex = attributeIndex;
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget, 0);
            GL.DisableVertexAttribArray(AttributeIndex);
            GL.DeleteBuffers(1, ref index);
        }

        public void Draw()
        {
            GL.BindBuffer(BufferTarget, index);
            GL.DrawArrays(PrimitiveType.Quads, 0, count);
            GL.BindBuffer(BufferTarget, 0);
        }
    }
}
