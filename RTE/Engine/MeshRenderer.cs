using OpenTK;
using RTE.Engine.MaterialRenderers;

namespace RTE.Engine
{
    static class MeshRenderer
    {
        private static Matrix4 projection;

        private static Camera camera;

        static MeshRenderer()
        {
        }

        public static void Draw(Actor[] actors, Scene scene)
        {
            Capabilities.DepthTest = true;
            
            Matrix4 projView = camera.View * projection;

            foreach (Actor actor in actors)
            {
                MaterialRenderer renderer = actor.Mesh.Material.Renderer;

                renderer.Shader.Enable();

                renderer.BindMVP(
                    actor.Transform.GetModel(),
                    projView,
                    actor.Transform.GetNormalMatrix()
                    );

                renderer.BindAmbient(
                    camera.Position,
                    scene.AmbientColor
                    );

                renderer.Bind();
                renderer.BindLight(scene.Lights);

                actor.Mesh.Draw();

                renderer.Shader.Disable();
            }
        }

        public static void SetPerspectiveAspect(float aspect)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView(
                1.5f,
                aspect,
                0.1f,
                100.0f
                );
        }

        public static void SetCamera(Camera camera)
        {
            MeshRenderer.camera = camera;
        }
    }
}
