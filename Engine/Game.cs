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

            Attributes = ShaderProgram.GetAttributes("coord", "tex_coord");
            Uniforms = ShaderProgram.GetUniforms("color", "tex");

            Vertex4D[] vertices =
            {
                new Vertex4D(-0.6f, 0.8f, 1.0f, 0.0f),
                new Vertex4D(0.6f, 0.6f, 0.0f, 0.0f),
                new Vertex4D(0.5f, -0.5f, 0.0f, 1.0f),
                new Vertex4D(-0.6f, -0.6f, 1.0f, 1.0f)
            };

            BufferObject = new BufferObject<Vertex4D>(BufferTarget.ArrayBuffer, vertices);
            BufferObject.AddAttributes(
                new ShaderAttribute("coord", Attributes["coord"], 2, 4, 0),
                new ShaderAttribute("tex_coord", Attributes["tex_coord"], 2, 4, 2)
                );

            CheckOpenGLError();

            // Set texture
            ShaderProgram.SetTexture(new Texture("sky.png"));

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
            ShaderProgram.BindTexture(0, "tex");

            GL.Uniform4(Uniforms["color"], Color.CadetBlue);

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
