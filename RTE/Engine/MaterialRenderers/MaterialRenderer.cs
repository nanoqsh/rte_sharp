using OpenTK;
using RTE.Engine.Uniforms;
using RTE.Engine.Materials;

namespace RTE.Engine.MaterialRenderers
{
    abstract class MaterialRenderer
    {
        public readonly Material Material;

        public abstract ShaderProgram Shader { get; }

        private readonly UniformMatrix4 model;
        private readonly UniformMatrix4 projView;
        private readonly UniformMatrix3 normalMatrix;

        protected MaterialRenderer(Material material)
        {
            Material = material;

            model = new UniformMatrix4(Shader.GetUniformIndex("model"));
            projView = new UniformMatrix4(Shader.GetUniformIndex("projView"));
            normalMatrix = new UniformMatrix3(Shader.GetUniformIndex("normalMatrix"));
        }

        public abstract void Bind();

        public abstract void BindLight(PointLight[] lights);

        public abstract void BindAmbient(Vector3 viewPosition, Vector3 ambient);

        public void BindMVP(Matrix4 model, Matrix4 projView, Matrix3 normalMatrix)
        {
            this.model.Value = model;
            this.model.Bind();

            this.projView.Value = projView;
            this.projView.Bind();

            this.normalMatrix.Value = normalMatrix;
            this.normalMatrix.Bind();
        }
    }
}
