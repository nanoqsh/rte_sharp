namespace RTE.Engine
{
    class Attribute
    {
        public readonly int Index;
        public readonly int SizeOfElements;
        public readonly int StrideOfElements;
        public readonly int OffsetOfElements;

        public Attribute(
            int index,
            int sizeOfElements,
            int strideOfElements = 0,
            int offsetOfElements = 0
            )
        {
            Index = index;
            SizeOfElements = sizeOfElements;
            StrideOfElements = strideOfElements;
            OffsetOfElements = offsetOfElements;
        }
    }
}
