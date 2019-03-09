using OpenTK;
using OpenTK.Graphics;

namespace RTE.Engine.Materials
{
    abstract class Material
    {
        public readonly string Name;
        public abstract ShaderProgram Shader { get; }

        protected Material(string name)
        {
            Name = name;

            Shader.ClearUniforms();

            Shader.AddUniforms(
                new UniformMatrix("model", Matrix4.Identity),
                new UniformMatrix("projView", Matrix4.Identity),
                new UniformColor("ambient", Color4.Black)
                );
        }

        public abstract void Bind();

        public void BindGlobal(Matrix4 model, Matrix4 projView, Color4 ambient)
        {
            int modelIndex = Shader.GetUniformIndex("model");
            int projViewIndex = Shader.GetUniformIndex("projView");
            int ambientIndex = Shader.GetUniformIndex("ambient");

            (Shader.Uniforms[modelIndex] as UniformMatrix).Matrix = model;
            (Shader.Uniforms[projViewIndex] as UniformMatrix).Matrix = projView;

            int k = ambientIndex;

            if (k != -1)
                (Shader.Uniforms[k] as UniformColor).Color = ambient;

            Shader.BindUniform(modelIndex);
            Shader.BindUniform(projViewIndex);

            if (k != -1)
                Shader.BindUniform(ambientIndex);
        }
    }
}
