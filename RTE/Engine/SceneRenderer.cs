using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class SceneRenderer
    {
        private readonly Scene scene;

        public Color4 BackgroundColor;

        private Postprocessor postprocessor;

        public SceneRenderer(Scene scene)
        {
            this.scene = scene;

            postprocessor = new Postprocessor();

            BackgroundColor = Color4.Black;
        }

        public void Draw(Actor[] actors)
        {
            postprocessor.Bind();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(BackgroundColor);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit
                );

            MeshRenderer.Draw(actors, scene);

            postprocessor.DrawFrame();
        }
    }
}
