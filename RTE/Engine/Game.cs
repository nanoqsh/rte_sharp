using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using RTE.Engine.Materials;

namespace RTE.Engine
{
    class Game : GameWindow
    {
        public readonly string VideoVersion;

        private readonly Camera camera;

        private readonly MousePosition mousePosition;

        private readonly HashSet<Key> pressedKeys = new HashSet<Key>();
        
        private Actor actor;
        private Scene scene;

        public Game(int width, int height, string title, int pixelSize = 1)
            : base(
                  width,
                  height,
                  new GraphicsMode(new ColorFormat(32), 8),
                  title,
                  GameWindowFlags.Default
                  )
        {
            VSync = VSyncMode.On;
            CursorVisible = false;

            mousePosition = new MousePosition();

            GL.Enable(EnableCap.Texture2D);

            VideoVersion = GL.GetString(StringName.Version);

            Viewport.SetPixelSize(pixelSize);

            camera = new Camera(
                new Vector3(0.0f, 0.0f, -2.0f),
                new Vector3(0.0f, 0.0f, -1.0f)
                );

            MeshRenderer.SetCamera(camera);
            MeshRenderer.SetPerspectiveAspect(
                    ClientRectangle.Width / (float)ClientRectangle.Height
                    );
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

            Viewport.Resize(ClientRectangle);

            Material defMaterial = new DefaultMaterial("def");
            Material texMaterial = new DefaultMaterial(
                "tex",
                new Texture("BaseTexture.png")
                );

            Material emissiveMaterial = new EmissiveMaterial(
                "em",
                new Texture("BaseTexture.png")
                );

            scene = new Scene("main")
                .AddActor(new Actor(
                    "actor",
                    new Mesh("Icosphere.obj", defMaterial)
                    ))
                .AddActor(new Actor(
                    "block",
                    new Mesh("Block.obj", defMaterial),
                    new Transform(new Vector3(4, 0, 0), new Quaternion(0, 0.1f, 0.6f))
                    ))
                .AddActor(new Actor(
                    "block2",
                    new Mesh("Block.obj", texMaterial),
                    new Transform(new Vector3(2, 1, 2), new Quaternion(0.4f, 0, -0.6f))
                    ))
                .AddActor(new Actor(
                    "stone",
                    new Mesh("Stone.obj", emissiveMaterial),
                    new Transform(new Vector3(-3, 0, 2))
                    ));

            actor = scene.GetActor("actor");

            scene.AmbientColor = Color4.DarkOliveGreen;

            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

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
                camera.Move(-Vector3.Normalize(
                    Vector3.Cross(camera.Front, Vector3.UnitY)) * cameraSpeed
                    );

            if (pressedKeys.Contains(Key.D))
                camera.Move(Vector3.Normalize(
                    Vector3.Cross(camera.Front, Vector3.UnitY)) * cameraSpeed
                    );


            const float rotationSpeed = 0.1f;

            Transform tr = actor.Transform;

            if (pressedKeys.Contains(Key.Number1))
                tr.RotateByX(rotationSpeed);

            if (pressedKeys.Contains(Key.Number2))
                tr.RotateByX(-rotationSpeed);

            if (pressedKeys.Contains(Key.Number3))
                tr.RotateByY(rotationSpeed);

            if (pressedKeys.Contains(Key.Number4))
                tr.RotateByY(-rotationSpeed);

            if (pressedKeys.Contains(Key.Number5))
                tr.RotateByZ(rotationSpeed);

            if (pressedKeys.Contains(Key.Number6))
                tr.RotateByZ(-rotationSpeed);

            const float scaleSpeed = 0.1f;

            if (pressedKeys.Contains(Key.Z))
                tr.ScaleByX(scaleSpeed);

            if (pressedKeys.Contains(Key.X))
                tr.ScaleByX(-scaleSpeed);
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            scene.Draw();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Viewport.Resize(ClientRectangle);

            MeshRenderer.SetPerspectiveAspect(
                ClientRectangle.Width / (float)ClientRectangle.Height
                );
        }
    }
}
