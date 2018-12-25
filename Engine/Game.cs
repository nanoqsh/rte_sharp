using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;


namespace OpenGLEngine.Engine
{
    class Game : GameWindow
    {
        public readonly string VideoVersion;
        private ShaderProgram ShaderProgram;

        private BufferObject<Vertex4D> BufferObject;


        public Game(int width, int height, string title) :
            base(
                  width,
                  height,
                  new GraphicsMode(new ColorFormat(32), 8),
                  title,
                  GameWindowFlags.Default
                  )
        {
            GL.Enable(EnableCap.Texture2D);

            VideoVersion = GL.GetString(StringName.Version);
        }

        public void SetShaderProgram(ShaderProgram shader)
        {
            ShaderProgram = shader;

            ShaderProgram.AddUniforms(
                new UniformTexture("tex", new Texture("sky.png"), 0),
                new UniformColor("color", Color.HotPink)
                );
        }

        private static void CheckOpenGLError()
        {
            ErrorCode errCode = GL.GetError();

            if (errCode != ErrorCode.NoError)
                throw new Exception(
                    string.Format("OpenGl error! - {0}", errCode)
                    );
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape)
                Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BufferObject = new BufferObject<Vertex4D>(
                new Vertex4D(-0.6f, 0.8f, 1.0f, 0.0f),
                new Vertex4D(0.6f, 0.6f, 0.0f, 0.0f),
                new Vertex4D(0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex4D(-0.6f, -0.6f, 1.0f, 1.0f)
                );

            BufferObject.AddAttributes(
                new Attribute("coord", ShaderProgram.GetAttribute("coord"), 2, 4, 0),
                new Attribute("tex_coord", ShaderProgram.GetAttribute("tex_coord"), 2, 4, 2)
                );

            CheckOpenGLError();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(Color4.PowderBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ShaderProgram.Enable();

            BufferObject.Draw();

            ShaderProgram.Disable();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
        }
    }
}
