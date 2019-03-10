using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    class Postprocessor : IDisposable
    {
        private readonly ShaderProgram shaderProgram;
        private readonly ArrayObject<Vector4> quad;
        private FrameBuffer frameBuffer;
        private UniformTexture uniformTexture;

        public Postprocessor()
        {
            frameBuffer = new FrameBuffer(
                Viewport.Size.Width / Viewport.PixelSize,
                Viewport.Size.Height / Viewport.PixelSize
                );

            Viewport.OnResize += Resize;

            shaderProgram = new ShaderProgram(
                new ShaderVertex("postVS.glsl"),
                new ShaderFragment("postFS.glsl")
                );

            uniformTexture = new UniformTexture(
                shaderProgram.GetUniformIndex("tex"),
                frameBuffer.Frame,
                0
                );

            quad = Quad.Make().AddAttributes(
                new Attribute(shaderProgram.GetAttributeIndex("position"), 2, 4, 0),
                new Attribute(shaderProgram.GetAttributeIndex("texCoords"), 2, 4, 2)
                );
        }

        public void Dispose()
        {
            shaderProgram.Dispose();
            quad.Dispose();
            frameBuffer.Dispose();
        }

        public void Resize(Rectangle client)
        {
            frameBuffer.Dispose();

            frameBuffer = new FrameBuffer(
                Viewport.Size.Width / Viewport.PixelSize,
                Viewport.Size.Height / Viewport.PixelSize
                );

            uniformTexture = new UniformTexture(
                shaderProgram.GetUniformIndex("tex"),
                frameBuffer.Frame,
                0
                );
        }

        public void Bind()
        {
            GL.Viewport(new Rectangle(
                0,
                0,
                Viewport.Size.Width / Viewport.PixelSize,
                Viewport.Size.Height / Viewport.PixelSize
                ));

            frameBuffer.Bind();
        }

        public void Unbind()
        {
            frameBuffer.Unbind();
        }

        public void DrawFrame()
        {
            Capabilities.DepthTest = false;

            frameBuffer.Unbind();

            GL.Viewport(Viewport.Size);

            shaderProgram.Enable();
            uniformTexture.Bind();
            quad.Draw();
            shaderProgram.Disable();
        }
    }
}
