using OpenTK;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    static class MeshRenderer
    {
        private static UniformMatrix model;
        private static UniformMatrix projView;
        private static Matrix4 projection;

        private static readonly int modelUniformKey;
        private static readonly int projViewUniformKey;

        private static readonly ShaderProgram shaderProgram;
        public static ShaderProgram ShaderProgram
        {
            get => shaderProgram;
        }

        private static Camera camera;

        private static readonly int texUniformKey;
        private static UniformColor ambient;
        private static readonly int ambientUniformKey;

        static MeshRenderer()
        {
            shaderProgram = new ShaderProgram(
                new ShaderVertex("meshVS.glsl"),
                new ShaderFragment("meshFS.glsl")
                );

            UniformTexture texUniform = new UniformTexture(
                "tex",
                new Texture("BaseTexture.png"),
                0
                );

            shaderProgram.AddUniforms(texUniform);

            model = new UniformMatrix("model", Matrix4.Identity);
            projView = new UniformMatrix("projView", Matrix4.Identity);

            shaderProgram.AddUniforms(model, projView);

            modelUniformKey = shaderProgram.GetUniformKey(model.Name);
            projViewUniformKey = shaderProgram.GetUniformKey(projView.Name);
            texUniformKey = shaderProgram.GetUniformKey(texUniform.Name);

            ambient = new UniformColor("ambient", Color.Black);
            shaderProgram.AddUniforms(ambient);
            ambientUniformKey = shaderProgram.GetUniformKey(ambient.Name);
        }

        public static void Draw(Actor[] actors, Scene scene)
        {
            Capabilities.DepthTest = true;

            shaderProgram.Enable();

            // Reversed order of matrix multiplication (;_;)
            projView.Matrix = camera.View * projection;

            shaderProgram.BindUniform(projViewUniformKey);
            shaderProgram.BindUniform(texUniformKey);

            ambient.Color = scene.AmbientColor;
            shaderProgram.BindUniform(ambientUniformKey);

            foreach (Actor actor in actors)
            {
                model.Matrix = actor.Transform.GetModel();
                shaderProgram.BindUniform(modelUniformKey);
                actor.Mesh.Draw();
            }

            shaderProgram.Disable();
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

        public static string GetDebugInfo(string[] attributes, string[] uniforms)
        {
            string res = "";

            foreach ((string key, int value) in shaderProgram.GetAttributes(attributes))
                res += key + ": " + value + "\n";

            foreach ((string key, int value) in shaderProgram.GetUniforms(uniforms))
                res += key + ": " + value + "\n";

            foreach (Shader sh in shaderProgram.Shaders)
                res += sh.Name + ": " + sh.GetLogInfo();

            return res;
        }
    }
}
