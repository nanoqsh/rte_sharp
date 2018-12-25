using System;
using System.Collections.Generic;
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
        private List<Attribute> shaderAttributes;

        public int Index
        {
            get => index;
        }

        public BufferObject(params V[] vertices)
        {
            BufferTarget = BufferTarget.ArrayBuffer;
            count = vertices.Length;

            GL.GenBuffers(1, out index);
            GL.BindBuffer(BufferTarget, index);

            GL.BufferData(
                BufferTarget,
                vertices.Length * Marshal.SizeOf(typeof(V)),
                vertices,
                BufferUsageHint.StaticDraw
                );

            shaderAttributes = new List<Attribute>();
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget, 0);

            foreach (Attribute attribute in shaderAttributes)
                GL.DisableVertexAttribArray(attribute.Index);

            GL.DeleteBuffers(1, ref index);
        }

        public void AddAttributes(params Attribute[] attributes)
        {
            shaderAttributes.AddRange(attributes);

            GL.BindBuffer(BufferTarget, index);

            foreach (Attribute attribute in attributes)
            {
                GL.EnableVertexAttribArray(attribute.Index);
                GL.VertexAttribPointer(
                    attribute.Index,
                    attribute.SizeOfElements,
                    VertexAttribPointerType.Float,
                    false,
                    attribute.StrideOfElements * sizeof(float),
                    attribute.OffsetOfElements * sizeof(float)
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
