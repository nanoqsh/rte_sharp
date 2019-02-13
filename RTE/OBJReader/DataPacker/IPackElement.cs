using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    public interface IPackElement<out E> where E : Element
    {
        E OBJElement { get; }
        IPackElement<E> PackVertices(IVertexContainer container);
    }
}
