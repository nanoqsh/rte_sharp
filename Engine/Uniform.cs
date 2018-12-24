

namespace OpenGLEngine.Engine
{
    abstract class Uniform
    {
        public string Name { get; }

        public Uniform(string name)
        {
            Name = name;
        }

        public abstract void Bind(int unit);
    }
}
