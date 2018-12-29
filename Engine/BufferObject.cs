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
        private List<Attribute> shaderAttributes;

        public int Index
        {
            get => index;
        }

        public BufferObject(params V[] vertices)
        {
            count = vertices.Length;

            GL.GenVertexArrays(1, out index);
            GL.BindVertexArray(index);
            GL.BindBuffer(BufferTarget.ArrayBuffer, index);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * Marshal.SizeOf(typeof(V)),
                vertices,
                BufferUsageHint.StaticDraw
                );

            GL.BindVertexArray(0);

            shaderAttributes = new List<Attribute>();
        }

        public void Dispose()
        {
            GL.BindVertexArray(0);

            foreach (Attribute attribute in shaderAttributes)
                GL.DisableVertexAttribArray(attribute.Index);

            GL.DeleteBuffers(1, ref index);
        }

        public void AddAttributes(params Attribute[] attributes)
        {
            shaderAttributes.AddRange(attributes);

            GL.BindVertexArray(index);

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

            GL.BindVertexArray(0);
        }

        public void Draw(PrimitiveType type)
        {
            GL.BindVertexArray(index);
            GL.DrawArrays(type, 0, count);
            GL.BindVertexArray(0);
        }
    }
}
