using System;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class ElementArrayObject<V> : ArrayObject<V>
        where V : struct
    {
        private bool elementBufferCreated;
        private BeginMode beginMode = BeginMode.Triangles;

        public ElementArrayObject(params V[] vertices)
            : base(vertices)
        {
            elementBufferCreated = false;
        }

        public ElementArrayObject<V> CreateElementBuffer(params uint[] indices)
        {
            if (elementBufferCreated)
                throw new Exception("Element Buffer already created!");

            elementBufferCreated = true;
            drawCount = indices.Length;

            int elementBufferIndex = GL.GenBuffer();
            GL.BindVertexArray(Index);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferIndex);

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indices.Length * sizeof(uint),
                indices,
                BufferUsageHint.StaticDraw
                );

            GL.BindVertexArray(0);
            return this;
        }

        public override ArrayObject<V> SetDrawMode(DrawMode mode)
        {
            beginMode = mode.GetBeginMode();
            return this;
        }

        public override void Draw()
        {
            GL.BindVertexArray(Index);
            GL.DrawElements(beginMode, drawCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }
    }
}
