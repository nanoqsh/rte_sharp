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
        private readonly UniformVector3 ambient;
        private readonly UniformMatrix3 normalMatrix;
        private readonly UniformVector3 viewPosition;

        protected Material(string name)
        {
            Name = name;

            model = new UniformMatrix4(Shader.GetUniformIndex("model"));
            projView = new UniformMatrix4(Shader.GetUniformIndex("projView"));
            ambient = new UniformVector3(Shader.GetUniformIndex("ambient"));
            normalMatrix = new UniformMatrix3(Shader.GetUniformIndex("normalMatrix"));
            viewPosition = new UniformVector3(Shader.GetUniformIndex("viewPosition"));

            Console.WriteLine("{0}:", name);
            Console.WriteLine("Model: {0}", model.Index);
            Console.WriteLine("ProjView: {0}", projView.Index);
            Console.WriteLine("Ambient: {0}", ambient.Index);
            Console.WriteLine("normalMatrix: {0}", normalMatrix.Index);
            Console.WriteLine("viewPosition: {0}", viewPosition.Index);
            Console.WriteLine();
        }

        public abstract void Bind();

        public void BindGlobal(
            Matrix4 model,
            Matrix4 projView,
            Matrix3 normalMatrix,
            Vector3 viewPosition,
            Vector3 ambient
            )
        {
            this.model.Matrix = model;
            this.model.Bind();

            this.projView.Matrix = projView;
            this.projView.Bind();

            this.normalMatrix.Matrix = normalMatrix;
            this.normalMatrix.Bind();

            this.viewPosition.Vector = viewPosition;
            this.viewPosition.Bind();

            if (this.ambient.Index != -1)
            {
                this.ambient.Vector = ambient;
                this.ambient.Bind();
            }
        }
    }
}
