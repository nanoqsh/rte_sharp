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
        private UniformMatrix projection;

        private Vector3 cameraPos;
        private Vector3 cameraFront;
        private Vector2 cameraRot;

        private Vector2 lastMousePos;

        private HashSet<Key> pressedKeys = new HashSet<Key>();

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
            CursorVisible = false;

            lastMousePos = new Vector2(
                Mouse.GetState().X,
                Mouse.GetState().Y
                );

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
                new UniformColor("color", Color.Coral)
                );


            projection = new UniformMatrix("projection", Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
                width / (float) height,
                0.5f,
                100.0f
                ));

            cameraPos = new Vector3(0.0f, 0.0f, -2.0f);
            cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
            cameraRot = new Vector2(0.0f, 0.0f);

            view = new UniformMatrix("view", Matrix4.LookAt(
                cameraPos,
                cameraPos + cameraFront,
                Vector3.UnitY
                ));

            ShaderProgram.AddUniforms(projection, view);

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

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            lastMousePos = new Vector2(
                Mouse.GetState().X,
                Mouse.GetState().Y
                );
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            pressedKeys.Add(e.Key);

            if (e.Key == Key.F11)
                WindowState = WindowState == WindowState.Fullscreen
                    ? WindowState.Normal
                    : WindowState.Fullscreen;

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


            // Converting degrees to radians
            double convert(double x) => Math.PI / 180 * x;

            float MAX_PITCH = 89.0f;
            float MIN_PITCH = -89.0f;

            float sensitivity = 0.3f;

            if (Focused)
            {
                Vector2 delta = lastMousePos - new Vector2(
                Mouse.GetState().X,
                Mouse.GetState().Y
                );

                cameraRot.X = (cameraRot.X + delta.X * sensitivity) % 360;
                cameraRot.Y += delta.Y * sensitivity;

                lastMousePos = new Vector2(
                    Mouse.GetState().X,
                    Mouse.GetState().Y
                    );


                if (cameraRot.Y > MAX_PITCH)
                    cameraRot.Y = MAX_PITCH;

                if (cameraRot.Y < MIN_PITCH)
                    cameraRot.Y = MIN_PITCH;


                Vector3 front = new Vector3(
                    (float)Math.Sin(convert(cameraRot.X)) * (float)Math.Cos(convert(cameraRot.Y)),
                    (float)Math.Sin(convert(cameraRot.Y)),
                    (float)Math.Cos(convert(cameraRot.X)) * (float)Math.Cos(convert(cameraRot.Y))
                    );

                front.Normalize();
                cameraFront = front;
            }


            float cameraSpeed = 1.0f * (float) e.Time;

            if (pressedKeys.Contains(Key.W))
                cameraPos += cameraSpeed * cameraFront;

            if (pressedKeys.Contains(Key.S))
                cameraPos -= cameraSpeed * cameraFront;

            if (pressedKeys.Contains(Key.A))
                cameraPos -= Vector3.Normalize(Vector3.Cross(cameraFront, Vector3.UnitY)) * cameraSpeed;

            if (pressedKeys.Contains(Key.D))
                cameraPos += Vector3.Normalize(Vector3.Cross(cameraFront, Vector3.UnitY)) * cameraSpeed;
            

            model.Matrix =
                  Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, 0.0f), 0.5f * (float) e.Time)
                * model.Matrix;

            view.Matrix = Matrix4.LookAt(
                cameraPos,
                cameraPos + cameraFront,
                Vector3.UnitY
                );
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
                | ClearBufferMask.DepthBufferBit
                );
            
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

            projection.Matrix = Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
                ClientRectangle.Width / (float) ClientRectangle.Height,
                0.5f,
                100.0f
                );

            postprocessor.Resize(ClientRectangle);
        }
    }
}
