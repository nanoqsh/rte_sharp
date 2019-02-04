using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class ArrayObject<V> : IDisposable
        where V : struct
    {
        protected int drawCount;
        private readonly List<Attribute> shaderAttributes;
        private PrimitiveType primitiveType = PrimitiveType.Triangles;

        protected int Index { get; }

        public ArrayObject(params V[] vertices)
        {
            drawCount = vertices.Length;

            Index = GL.GenVertexArray();
            GL.BindVertexArray(Index);

            int arrayBufferIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferIndex);

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

            GL.DeleteVertexArray(Index);
        }

        public virtual ArrayObject<V> SetDrawMode(DrawMode mode)
        {
            primitiveType = mode.GetPrimitiveType();
            return this;
        }

        public ArrayObject<V> AddAttributes(params Attribute[] attributes)
        {
            GL.BindVertexArray(Index);

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

            shaderAttributes.AddRange(attributes);

            GL.BindVertexArray(0);
            return this;
        }

        public virtual void Draw()
        {
            GL.BindVertexArray(Index);
            GL.DrawArrays(primitiveType, 0, drawCount);
            GL.BindVertexArray(0);
        }
    }
}
