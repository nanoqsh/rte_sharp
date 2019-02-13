using System.Collections.Generic;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    class PackPoint : IPackElement<Point>
    {
        public readonly List<Vertex> Vertices;
        
        public Point OBJElement { get; }

        public PackPoint(Point point)
        {
            OBJElement = point;
            
            Vertices = new List<Vertex>();
        }
        
        public IPackElement<Point> PackVertices(IVertexContainer container)
        {
            for (int i = 0; i < OBJElement.VertexIndexes.Length; i++)
            {
                int index = OBJElement.VertexIndexes[i];

                (Vertex v, int newIndex) = container.Vertices.Get(index);
                Vertices.Add(v);

                OBJElement.VertexIndexes[i] = newIndex;
            }

            return this;
        }
    }
}
