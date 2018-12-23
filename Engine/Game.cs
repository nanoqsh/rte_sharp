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

        private Dictionary<string, int> Uniforms;
        private Dictionary<string, int> Attributes;

        private BufferObject BufferObject;


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
        }

        private static void CheckOpenGLError()
        {
            ErrorCode errCode = GL.GetError();

            if (errCode != ErrorCode.NoError)
                throw new Exception(
                    string.Format("OpenGl error! - {0}: {1}", errCode, errCode.ToString())
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

            Attributes = ShaderProgram.GetAttributes("coord");
            Uniforms = ShaderProgram.GetUniforms("color");

            CheckOpenGLError();

            Vertex2D[] vertices =
            {
                new Vertex2D(0.0f, 0.0f),
                new Vertex2D(0.0f, 1.0f),
                new Vertex2D(0.8f, 1.0f),
                new Vertex2D(1.0f, 0.2f)
            };

            BufferObject = new BufferObject(
                BufferTarget.ArrayBuffer,
                vertices,
                2,
                Attributes["coord"]
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

            GL.ClearColor(Color4.Khaki);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ShaderProgram.Enable();

            GL.Uniform4(Uniforms["color"], Color.CadetBlue);

            BufferObject.Draw();

            ShaderProgram.Disable();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Rectangle client = ClientRectangle;

            GL.Viewport(
                client.X,
                client.Y,
                client.Width,
                client.Height
                );
        }

    }
}
