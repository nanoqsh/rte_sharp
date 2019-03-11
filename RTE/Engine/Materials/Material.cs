using OpenTK;
using OpenTK.Graphics;
using RTE.Engine.Uniforms;
using System;

namespace RTE.Engine.Materials
{
    abstract class Material
    {
        public readonly string Name;
        public abstract ShaderProgram Shader { get; }

        private readonly UniformMatrix4 model;
        private readonly UniformMatrix4 projView;
        private readonly UniformColor4 ambient;
        private readonly UniformMatrix3 normalMatrix;

        protected Material(string name)
        {
            Name = name;

            model = new UniformMatrix4(Shader.GetUniformIndex("model"));
            projView = new UniformMatrix4(Shader.GetUniformIndex("projView"));
            ambient = new UniformColor4(Shader.GetUniformIndex("ambient"));
            normalMatrix = new UniformMatrix3(Shader.GetUniformIndex("normalMatrix"));

            Console.WriteLine("{0}:", name);
            Console.WriteLine("Model: {0}", model.Index);
            Console.WriteLine("ProjView: {0}", projView.Index);
            Console.WriteLine("Ambient: {0}", ambient.Index);
            Console.WriteLine("normalMatrix: {0}", normalMatrix.Index);
            Console.WriteLine();
        }

        public abstract void Bind();

        public void BindGlobal(
            Matrix4 model,
            Matrix4 projView,
            Matrix3 normalMatrix,
            Color4 ambient
            )
        {
            this.model.Matrix = model;
            this.model.Bind();

            this.projView.Matrix = projView;
            this.projView.Bind();

            this.normalMatrix.Matrix = normalMatrix;
            this.normalMatrix.Bind();

            if (this.ambient.Index != -1)
            {
                this.ambient.Color = ambient;
                this.ambient.Bind();
            }
        }
    }
}
