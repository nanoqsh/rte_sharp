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

        private Postprocessor postprocessor;
        private readonly int pixelSize;

        public Game(int width, int height, string title, int pixelSize = 1) :
            base(
                  width,
                  height,
                  new GraphicsMode(new ColorFormat(32), 8),
                  title,
                  GameWindowFlags.Default
                  )
        {
            this.pixelSize = pixelSize;

            GL.Enable(EnableCap.Texture2D);

            VideoVersion = GL.GetString(StringName.Version);

            ShaderProgram = new ShaderProgram(
                new VertexShader("vertexShader.glsl"),
                new FragmentShader("fragmentShader.glsl")
                );

            ShaderProgram.AddUniforms(
                new UniformTexture("tex", new Texture("sky.png"), 0),
                new UniformTexture("tex2", new Texture("cube_zombie.png"), 1),
                new UniformInt("pixelSize", pixelSize),
                new UniformColor("color", Color.HotPink)
                );
        }

        public string GetDebugInfo()
        {
            string[] attributes = new string[] { "coord", "tex_coord" };
            string[] uniforms = new string[] { "color", "pixelSize", "tex", "tex2" };
            string res = "";

            foreach (KeyValuePair<string, int> pair in ShaderProgram.GetAttributes(attributes))
                Console.WriteLine(pair.Key + ": " + pair.Value);

            foreach (KeyValuePair<string, int> pair in ShaderProgram.GetUniforms(uniforms))
                Console.WriteLine(pair.Key + ": " + pair.Value);

            foreach (Shader sh in ShaderProgram.Shaders)
                Console.Write(sh.Name + ": " + sh.GetLogInfo());

            return res;
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
                new Vertex4D(-0.1f, 0.1f, 1.0f, 0.0f),
                new Vertex4D(0.1f, 0.1f, 0.0f, 0.0f),
                new Vertex4D(0.1f, -0.1f, 0.0f, 1.0f),
                new Vertex4D(-0.1f, -0.1f, 1.0f, 1.0f)
                );

            BufferObject.AddAttributes(
                new Attribute("coord", ShaderProgram.GetAttribute("coord"), 2, 4, 0),
                new Attribute("tex_coord", ShaderProgram.GetAttribute("tex_coord"), 2, 4, 2)
                );


            postprocessor = new Postprocessor(ClientRectangle, pixelSize);

            CheckOpenGLError();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //
            postprocessor.Bind();

            // Draw scene
            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(Color4.PowderBlue);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit);
            
            ShaderProgram.Enable();
            BufferObject.Draw();
            ShaderProgram.Disable();

            //
            GL.Disable(EnableCap.DepthTest);

            postprocessor.DrawFrame();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
            
            postprocessor.Resize(ClientRectangle);
        }
    }
}
