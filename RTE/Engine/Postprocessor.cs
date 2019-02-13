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
        public readonly int PixelSize;

        public Postprocessor(Rectangle client, int pixelSize = 1)
        {
            PixelSize = pixelSize;

            frameBuffer = new FrameBuffer(
                client.Width / pixelSize,
                client.Height / pixelSize
                );

            shaderProgram = new ShaderProgram(
                new ShaderVertex("defaultVertexShader.glsl"),
                new ShaderFragment("defaultFragmentShader.glsl")
                );

            shaderProgram.AddUniforms(
                new UniformTexture("tex", frameBuffer.Frame, 0)
                );

            quad = Quad.Make().AddAttributes(
                new Attribute("position", shaderProgram.GetAttribute("position"), 2, 4, 0),
                new Attribute("texCoords", shaderProgram.GetAttribute("texCoords"), 2, 4, 2)
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
            frameBuffer = null;
            frameBuffer = new FrameBuffer(
                client.Width / PixelSize,
                client.Height / PixelSize
                );

            shaderProgram.ClearUniforms();
            shaderProgram.AddUniforms(
                new UniformTexture("tex", frameBuffer.Frame, 0)
                );
        }

        public void Bind()
        {
            frameBuffer.Bind();
        }

        public void Unbind()
        {
            FrameBuffer.Unbind();
        }

        public void DrawFrame()
        {
            bool isDepthEnabled = GL.IsEnabled(EnableCap.DepthTest);
            GL.Disable(EnableCap.DepthTest);

            FrameBuffer.Unbind();

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shaderProgram.Enable();
            quad.Draw();
            shaderProgram.Disable();

            if (isDepthEnabled)
                GL.Enable(EnableCap.DepthTest);
        }
    }
}
