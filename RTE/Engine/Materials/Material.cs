using RTE.Engine.MaterialRenderers;

namespace RTE.Engine.Materials
{
    abstract class Material
    {
        public readonly string Name;

        protected Material(string name)
        {
            Name = name;
        }

        public abstract MaterialRenderer Renderer { get; }
    }
}
