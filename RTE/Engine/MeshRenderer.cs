using OpenTK;
using RTE.Engine.Shaders;
using System;

namespace RTE.Engine
{
    class MeshRenderer
    {
        private UniformMatrix model;
        private UniformMatrix projView;
        private Matrix4 projection;

        private int modelUniformKey;
        private int projViewUniformKey;
        private int texUniformKey;

        private readonly ShaderProgram shaderProgram;
        public ShaderProgram ShaderProgram
        {
            get => shaderProgram;
        }

        private Camera camera;

        private static MeshRenderer instance;
        public static MeshRenderer Instance
        {
            get
            {
                if (instance == null)
                    instance = new MeshRenderer();

                return instance;
            }
        }

        private MeshRenderer()
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
        }

        public void Draw(params Actor[] actors)
        {
            Capabilities.Instance.DepthTest = true;

            shaderProgram.Enable();

            // Reversed order of matrix multiplication (;_;)
            projView.Matrix = camera.View * projection;

            shaderProgram.BindUniform(projViewUniformKey);
            shaderProgram.BindUniform(texUniformKey);

            foreach (Actor actor in actors)
            {
                model.Matrix = actor.Transform.GetModel();
                shaderProgram.BindUniform(modelUniformKey);
                actor.Mesh.Draw();
            }

            shaderProgram.Disable();
        }

        public MeshRenderer SetPerspectiveAspect(float aspect)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView(
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
