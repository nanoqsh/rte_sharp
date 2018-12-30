using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class ArrayObject<V> : IDisposable
        where V : struct
    {
        protected int drawCount;
        private List<Attribute> shaderAttributes;
        private PrimitiveType primitiveType;

        public int Index { get; }

        public ArrayObject(params V[] vertices)
        {
            drawCount = vertices.Length;
            primitiveType = PrimitiveType.Triangles;

            Index = GL.GenVertexArray();
            GL.BindVertexArray(Index);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Index);

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
            
            GL.DeleteBuffer(Index);
        }

        public ArrayObject<V> SetDrawMode(DrawMode mode)
        {
            primitiveType = mode.GetPrimitiveType();
            return this;
        }

        public ArrayObject<V> AddAttributes(params Attribute[] attributes)
        {
            shaderAttributes.AddRange(attributes);

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
