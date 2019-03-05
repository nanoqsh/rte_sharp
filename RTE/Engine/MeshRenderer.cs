using OpenTK;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    class MeshRenderer
    {
        private UniformMatrix model;
        private UniformMatrix view;
        private UniformMatrix projection;

        private readonly ShaderProgram meshShaderProgram;

        private Camera camera;

        public MeshRenderer()
        {
            meshShaderProgram = new ShaderProgram(
                new ShaderVertex("meshVS.glsl"),
                new ShaderFragment("meshFS.glsl")
                );

            meshShaderProgram.AddUniforms(
                new UniformTexture("tex", new Texture("BaseTexture.png"), 0),
                new UniformColor("color", Color.Coral)
                );

            model = new UniformMatrix("model", Matrix4.Identity);
            view = new UniformMatrix("view", Matrix4.Identity);
            projection = new UniformMatrix("projection", Matrix4.Identity);

            meshShaderProgram.AddUniforms(model, view, projection);
        }

        public void Draw(params Actor[] actors)
        {
            meshShaderProgram.Enable();

            view.Matrix = camera.View;

            foreach (Actor actor in actors)
            {
                model.Matrix = actor.Transform.GetModel();
                meshShaderProgram.BindUniforms();
                actor.Mesh.Draw();
            }

            meshShaderProgram.Disable();
        }

        public Mesh CreateMesh(string meshName)
        {
            return new Mesh(meshName, meshShaderProgram);
        }

        public MeshRenderer SetPerspectiveAspect(float aspect)
        {
            projection.Matrix = Matrix4.CreatePerspectiveFieldOfView(
                1.6f,
                aspect,
                0.1f,
                100.0f
                );

            return this;
        }

        public MeshRenderer SetCamera(Camera camera)
        {
            this.camera = camera;

            return this;
        }

        public string GetDebugInfo(string[] attributes, string[] uniforms)
        {
            string res = "";

            foreach ((string key, int value) in meshShaderProgram.GetAttributes(attributes))
                res += key + ": " + value + "\n";

            foreach ((string key, int value) in meshShaderProgram.GetUniforms(uniforms))
                res += key + ": " + value + "\n";

            foreach (Shader sh in meshShaderProgram.Shaders)
                res += sh.Name + ": " + sh.GetLogInfo();

            return res;
        }
    }
}
