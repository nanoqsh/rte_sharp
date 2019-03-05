using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    class Game : GameWindow
    {
        public readonly string VideoVersion;

        private readonly ShaderProgram MeshShaderProgram;
        private Mesh mesh;

        private Postprocessor postprocessor;
        private readonly int pixelSize;

        private readonly UniformMatrix model;
        private readonly UniformMatrix view;
        private readonly UniformMatrix projection;

        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;

        private readonly Camera camera;

        private readonly MousePosition mousePosition;

        private readonly HashSet<Key> pressedKeys = new HashSet<Key>();

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

            mousePosition = new MousePosition();

            GL.Enable(EnableCap.Texture2D);

            VideoVersion = GL.GetString(StringName.Version);

            MeshShaderProgram = new ShaderProgram(
                new ShaderVertex("meshVS.glsl"),
                new ShaderFragment("meshFS.glsl")
                );

            MeshShaderProgram.AddUniforms(
                new UniformTexture("tex", new Texture("BaseTexture.png"), 0),
                new UniformColor("color", Color.Coral)
                );

            camera = new Camera(
                new Vector3(0.0f, 0.0f, -2.0f),
                new Vector3(0.0f, 0.0f, -1.0f)
                );

            view = new UniformMatrix("view", camera.View);

            projection = new UniformMatrix(
                "projection",
                CreatePerspective(width / (float) height)
                );
            
            MeshShaderProgram.AddUniforms(view, projection);

            position = new Vector3(0.0f, 0.0f, 0.0f);
            rotation = new Vector3(0.0f, 0.0f, 0.0f);
            scale = new Vector3(1.0f, 1.0f, 1.0f);

            Matrix4 matrix =
                  Matrix4.CreateTranslation(position)
                * Matrix4.CreateFromQuaternion(new Quaternion(rotation))
                * Matrix4.CreateScale(scale);

            model = new UniformMatrix("model", matrix);
            MeshShaderProgram.AddUniforms(model);
        }

        private static Matrix4 CreatePerspective(float aspect)
        {
            return Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
                aspect,
                0.1f,
                100.0f
                );
        }

        public string GetDebugInfo()
        {
            string[] attributes = new string[] { "coord", "texCoord" };
            string[] uniforms = new string[] { "color", "pixelSize", "tex", "projView", "model" };
            string res = "";

            foreach ((string key, int value) in MeshShaderProgram.GetAttributes(attributes))
                res += key + ": " + value + "\n";

            foreach ((string key, int value) in MeshShaderProgram.GetUniforms(uniforms))
                res += key + ": " + value + "\n";

            foreach (Shader sh in MeshShaderProgram.Shaders)
                res += sh.Name + ": " + sh.GetLogInfo();

            return res;
        }

        private static void CheckOpenGLError()
        {
            ErrorCode errCode = GL.GetError();

            if (errCode != ErrorCode.NoError)
                throw new Exception(
                    $"OpenGl error! - {errCode}"
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

            mesh = new Mesh("Block.obj", MeshShaderProgram);

            postprocessor = new Postprocessor(ClientRectangle, pixelSize);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            CheckOpenGLError();
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            mousePosition.Update();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Focused)
            {
                const float sensitivity = 0.3f;

                Vector2 delta = mousePosition.Update();

                camera.Rotate(delta * sensitivity);
            }


            float cameraSpeed = 1.0f * (float) e.Time;

            if (pressedKeys.Contains(Key.W))
                camera.Move(cameraSpeed * camera.Front);

            if (pressedKeys.Contains(Key.S))
                camera.Move(cameraSpeed * -camera.Front);

            if (pressedKeys.Contains(Key.A))
                camera.Move(-Vector3.Normalize(Vector3.Cross(camera.Front, Vector3.UnitY)) * cameraSpeed);

            if (pressedKeys.Contains(Key.D))
                camera.Move(Vector3.Normalize(Vector3.Cross(camera.Front, Vector3.UnitY)) * cameraSpeed);


            const float rotationSpeed = 0.1f;

            if (pressedKeys.Contains(Key.Number1))
                rotation.X = rotation.X + rotationSpeed;

            if (pressedKeys.Contains(Key.Number2))
                rotation.X -= rotationSpeed;

            if (pressedKeys.Contains(Key.Number3))
                rotation.Y += rotationSpeed;

            if (pressedKeys.Contains(Key.Number4))
                rotation.Y -= rotationSpeed;

            if (pressedKeys.Contains(Key.Number5))
                rotation.Z += rotationSpeed;

            if (pressedKeys.Contains(Key.Number6))
                rotation.Z -= rotationSpeed;

            const float scaleSpeed = 0.1f;

            if (pressedKeys.Contains(Key.Z))
                scale.X += scaleSpeed;

            if (pressedKeys.Contains(Key.X))
                scale.X -= scaleSpeed;

            const float positionSpeed = 0.1f;

            if (pressedKeys.Contains(Key.T))
                position.Z -= positionSpeed;

            if (pressedKeys.Contains(Key.G))
                position.Z += positionSpeed;

            if (pressedKeys.Contains(Key.F))
                position.X += positionSpeed;

            if (pressedKeys.Contains(Key.H))
                position.X -= positionSpeed;
            

            model.Matrix =
                  Matrix4.CreateScale(scale)
                * Matrix4.CreateFromQuaternion(new Quaternion(rotation))
                * Matrix4.CreateTranslation(position);

            
            view.Matrix = camera.View;
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

            MeshShaderProgram.Enable();
            mesh.Draw();
            MeshShaderProgram.Disable();

            //
            postprocessor.DrawFrame();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            postprocessor.Resize(ClientRectangle);

            projection.Matrix = CreatePerspective(
                ClientRectangle.Width / (float)ClientRectangle.Height
                );
        }
    }
}
