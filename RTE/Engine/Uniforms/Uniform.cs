namespace RTE.Engine.Uniforms
{
    abstract class Uniform<T>
    {
        public readonly int Index;

        private bool isModified;
        protected T value;

        protected Uniform(int index, T value)
        {
            Index = index;
            this.value = value;
            isModified = true;
        }

        public void Bind()
        {
            if (Index == -1)
                return;

            if (isModified)
                SetUniform();

            isModified = false;
        }

        public abstract void SetUniform();

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
