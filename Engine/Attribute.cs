

namespace OpenGLEngine.Engine
{
    class Attribute
    {
        public readonly string Name;
        public readonly int Index;
        public readonly int SizeOfElements;
        public readonly int StrideOfElements;
        public readonly int OffsetOfElements;

        public Attribute(
            string name,
            int index,
            int sizeOfElements,
            int strideOfElements = 0,
            int offsetOfElements = 0
            )
        {
            Name = name;
            Index = index;
            SizeOfElements = sizeOfElements;
            StrideOfElements = strideOfElements;
            OffsetOfElements = offsetOfElements;
        }
    }
}
