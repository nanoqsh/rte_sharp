namespace RTE.Engine
{
    abstract class Uniform
    {
        public readonly int Index;

        protected Uniform(int index)
        {
            Index = index;
        }

        public abstract void Bind();
    }
}
