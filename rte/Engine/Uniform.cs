namespace RTE.Engine
{
    abstract class Uniform
    {
        public string Name { get; }

        protected Uniform(string name)
        {
            Name = name;
        }

        public abstract void Bind(int index);
    }
}
