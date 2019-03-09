using OpenTK;
using RTE.Engine.Shaders;

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

            foreach (Actor actor in actors)
            {
                actor.Mesh.Material.Shader.Enable();

                actor.Mesh.Material.BindGlobal(
                    actor.Transform.GetModel(),
                    camera.View * projection,
                    scene.AmbientColor
                    );

                actor.Mesh.Material.Bind();

                actor.Mesh.Draw();

                actor.Mesh.Material.Shader.Disable();
            }
        }

        public static void SetPerspectiveAspect(float aspect)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
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
