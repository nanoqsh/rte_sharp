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

        public Postprocessor()
        {
            Viewport viewport = Viewport.Instance;

            frameBuffer = new FrameBuffer(
                viewport.Size.Width / viewport.PixelSize,
                viewport.Size.Height / viewport.PixelSize
                );

            viewport.OnResize += Resize;

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

            Viewport viewport = Viewport.Instance;

            frameBuffer = new FrameBuffer(
                viewport.Size.Width / viewport.PixelSize,
                viewport.Size.Height / viewport.PixelSize
                );

            shaderProgram.ClearUniforms();
            shaderProgram.AddUniforms(
                new UniformTexture("tex", frameBuffer.Frame, 0)
                );
        }

        public void Bind()
        {
            Viewport viewport = Viewport.Instance;

            GL.Viewport(new Rectangle(
                0,
                0,
                viewport.Size.Width / viewport.PixelSize,
                viewport.Size.Height / viewport.PixelSize
                ));

            frameBuffer.Bind();
        }

        public void Unbind()
        {
            frameBuffer.Unbind();
        }

        public void DrawFrame()
        {
            Capabilities.Instance.DepthTest = false;

            frameBuffer.Unbind();

            GL.Viewport(Viewport.Instance.Size);

            shaderProgram.Enable();
            shaderProgram.BindUniforms();
            quad.Draw();
            shaderProgram.Disable();
        }
    }
}
