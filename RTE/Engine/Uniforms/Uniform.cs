namespace RTE.Engine.Uniforms
{
    abstract class Uniform<T>
    {
        public readonly int Index;

        protected bool isModified;
        protected T value;

        protected Uniform(int index, T value)
        {
            Index = index;
            this.value = value;
            isModified = true;
        }

        public abstract void Bind();

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                isModified = true;
            }
        }
    }
}
