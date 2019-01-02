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
        private ArrayObject<Vertex5D> cube;

        private Postprocessor postprocessor;
        private readonly int pixelSize;

        private UniformMatrix model;

        private UniformMatrix view;
        private Vector3 cameraPos;

        private HashSet<Key> pressedKeys;

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
            VSync = VSyncMode.On;

            GL.Enable(EnableCap.Texture2D);

            VideoVersion = GL.GetString(StringName.Version);

            pressedKeys = new HashSet<Key>();

            ShaderProgram = new ShaderProgram(
                new VertexShader("vertexShader.glsl"),
                new FragmentShader("fragmentShader.glsl")
                );

            ShaderProgram.AddUniforms(
                new UniformTexture("tex", new Texture("sky.png"), 0),
                new UniformTexture("tex2", new Texture("cube_zombie.png"), 1),
                new UniformInt("pixelSize", pixelSize),
                new UniformColor("color", Color.Coral)
                );


            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
                width / (float) height,
                0.5f,
                100.0f
                );

            view = new UniformMatrix("view", Matrix4.LookAt(
                new Vector3(0.0f, 0.0f, 2.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f)
                ));

            ShaderProgram.AddUniforms(
                new UniformMatrix("projection", projection),
                view
                );

            Matrix4 transform = Matrix4.CreateTranslation(0.1f, 0.1f, 0.0f);
            Matrix4 rotation = Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, 0.0f), 0.6f);

            model = new UniformMatrix("model", rotation * transform);
            ShaderProgram.AddUniforms(model);
        }

        public string GetDebugInfo()
        {
            string[] attributes = new string[] { "coord", "tex_coord" };
            string[] uniforms = new string[] { "color", "pixelSize", "tex", "tex2" };
            string res = "";

            foreach (KeyValuePair<string, int> pair in ShaderProgram.GetAttributes(attributes))
                res += pair.Key + ": " + pair.Value + "\n";

            foreach (KeyValuePair<string, int> pair in ShaderProgram.GetUniforms(uniforms))
                res += pair.Key + ": " + pair.Value + "\n";

            foreach (Shader sh in ShaderProgram.Shaders)
                res += sh.Name + ": " + sh.GetLogInfo();

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

            pressedKeys.Add(e.Key);

            if (e.Key == Key.Escape)
                Exit();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);

            pressedKeys.Remove(e.Key);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cube = Cube.Make().AddAttributes(
                new Attribute("coord", ShaderProgram.GetAttribute("coord"), 3, 5, 0),
                new Attribute("tex_coord", ShaderProgram.GetAttribute("tex_coord"), 2, 5, 3)
                );


            postprocessor = new Postprocessor(ClientRectangle, pixelSize);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            CheckOpenGLError();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            float cameraSpeed = 2.5f;

            if (pressedKeys.Contains(Key.W))
                cameraPos += new Vector3(0.0f, 0.0f, cameraSpeed);

            if (pressedKeys.Contains(Key.S))
                cameraPos += new Vector3(0.0f, 0.0f, -cameraSpeed);

            if (pressedKeys.Contains(Key.A))
                cameraPos += new Vector3(cameraSpeed, 0.0f, 0.0f);

            if (pressedKeys.Contains(Key.D))
                cameraPos += new Vector3(-cameraSpeed, 0.0f, 0.0f);


            model.Matrix =
                  Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, 0.0f), 0.5f * (float) e.Time)
                * model.Matrix;

            view.Matrix =
                  Matrix4.CreateTranslation(cameraPos * (float) e.Time)
                * view.Matrix;

            cameraPos = Vector3.Zero;
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
            cube.Draw();
            ShaderProgram.Disable();

            //
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
