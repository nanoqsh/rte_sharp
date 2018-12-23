

namespace OpenGLEngine.Engine
{
    class ShaderAttribute
    {
        public readonly string Name;
        public readonly int Index;
        public readonly int SizeOfElements;
        public readonly int Stride;
        public readonly int Offset;

        public ShaderAttribute(
            string name,
            int index,
            int sizeOfElements,
            int stride = 0,
            int offset = 0
            )
        {
            Name = name;
            Index = index;
            SizeOfElements = sizeOfElements;
            Stride = stride;
            Offset = offset;
        }
    }
}
