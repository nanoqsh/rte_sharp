using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class SceneRenderer
    {
        private readonly Scene scene;

        public Color4 BackgroundColor { get => backgroundColor; }
        private Color4 backgroundColor;

        private Postprocessor postprocessor;

        public SceneRenderer(Scene scene)
        {
            this.scene = scene;

            postprocessor = new Postprocessor();

            backgroundColor = Color4.Black;
        }

        public SceneRenderer SetBackgroundColor(Color4 color)
        {
            backgroundColor = color;

            return this;
        }

        public void Draw(Actor[] actors)
        {
            postprocessor.Bind();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(backgroundColor);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit
                );

            MeshRenderer.Instance.Draw(actors);

            postprocessor.DrawFrame();
        }
    }
}
