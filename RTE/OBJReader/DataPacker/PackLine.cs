using System.Collections.Generic;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    class PackLine : IPackElement<Line>
    {
        public readonly List<Vertex> Vertices;
        public readonly List<VertexTexture> VertexTextures;
        
        public Line OBJElement { get; }

        public PackLine(Line line)
        {
            OBJElement = line;
            
            Vertices = new List<Vertex>();
            VertexTextures = new List<VertexTexture>();
        }
        
        public IPackElement<Line> PackVertices(IVertexContainer container)
        {
            for (int i = 0; i < OBJElement.Units.Count; i++)
                switch (OBJElement.Units[i])
                {
                    case LineUnitV l:
                        AddVertex(container, ref l);
                        OBJElement.Units[i] = l;
                        break;
                    
                    case LineUnitVT l:
                        AddVertex(container, ref l);
                        OBJElement.Units[i] = l;
                        break;
                
                    default:
                        throw new PackException("Unknown line type!");
                }

            return this;
        }
        
        private void AddVertex(IVertexContainer container, ref LineUnitV line)
        {
            (Vertex v, int vi) = container.Vertices.Get(line.V);
            Vertices.Add(v);
            
            line = new LineUnitV(vi);
        }
        
        private void AddVertex(IVertexContainer container, ref LineUnitVT line)
        {
            (Vertex v, int vi) = container.Vertices.Get(line.V);
            Vertices.Add(v);

            (VertexTexture vt, int vti) = container.VertexTextures.Get(line.T);
            VertexTextures.Add(vt);
            
            line = new LineUnitVT(vi, vti);
        }
    }
}
