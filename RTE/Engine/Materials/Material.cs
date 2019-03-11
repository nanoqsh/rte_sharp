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

        private readonly UniformMatrix model;
        private readonly UniformMatrix projView;
        private readonly UniformColor ambient;
        private readonly UniformMatrix3 normalMat;

        protected Material(string name)
        {
            Name = name;

            model = new UniformMatrix(Shader.GetUniformIndex("model"));
            projView = new UniformMatrix(Shader.GetUniformIndex("projView"));
            ambient = new UniformColor(Shader.GetUniformIndex("ambient"));
            normalMat = new UniformMatrix3(Shader.GetUniformIndex("normalMat"));

            Console.WriteLine("{0}:", name);
            Console.WriteLine("Model: {0}", model.Index);
            Console.WriteLine("ProjView: {0}", projView.Index);
            Console.WriteLine("Ambient: {0}", ambient.Index);
            Console.WriteLine("NormalMat: {0}", normalMat.Index);
            Console.WriteLine();
        }

        public abstract void Bind();

        public void BindGlobal(Matrix4 model, Matrix4 projView, Color4 ambient)
        {
            this.model.Matrix = model;
            this.model.Bind();

            this.projView.Matrix = projView;
            this.projView.Bind();

            if (this.ambient.Index != -1)
            {
                this.ambient.Color = ambient;
                this.ambient.Bind();
            }
            
            normalMat.Matrix = Matrix3.Transpose(new Matrix3(model).Inverted());
            normalMat.Bind();
        }
    }
}
