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
        private Rectangle client;

        public Postprocessor(Rectangle client, int pixelSize = 1)
        {
            PixelSize = pixelSize;
            this.client = client;

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
            frameBuffer = new FrameBuffer(
                client.Width / PixelSize,
                client.Height / PixelSize
                );

            shaderProgram.ClearUniforms();
            shaderProgram.AddUniforms(
                new UniformTexture("tex", frameBuffer.Frame, 0)
                );

            this.client = client;
        }

        public void Bind()
        {
            GL.Viewport(new Rectangle(
                0,
                0,
                client.Width / PixelSize,
                client.Height / PixelSize
                ));

            frameBuffer.Bind();
        }

        public void Unbind()
        {
            frameBuffer.Unbind();
        }

        public void DrawFrame()
        {
            bool isDepthEnabled = GL.IsEnabled(EnableCap.DepthTest);
            GL.Disable(EnableCap.DepthTest);

            frameBuffer.Unbind();

            GL.Viewport(client);

            shaderProgram.Enable();
            shaderProgram.BindUniforms();
            quad.Draw();
            shaderProgram.Disable();

            if (isDepthEnabled)
                GL.Enable(EnableCap.DepthTest);
        }
    }
}
