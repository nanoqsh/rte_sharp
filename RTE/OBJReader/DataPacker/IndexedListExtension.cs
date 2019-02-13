using System.Collections.Generic;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    static class IndexedListExtension
    {
        public static (V, int) Get<V>(this List<V> list, int index)
            where V : VertexData
        {
            if (index == 0)
                throw new PackException(
                    "The index can't be equals zero"
                    );

            if (index < 0)
                index = list.Count + index + 1;
            
            if (index - 1 >= list.Count)
                throw new PackException(
                    $"VertexData ({typeof(V)}) with index {index} doesn't exists!"
                    );
            
            return (list[index - 1], index);
        }
    }
}
