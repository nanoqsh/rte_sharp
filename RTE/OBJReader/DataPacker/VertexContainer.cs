using System.Collections.Generic;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    class VertexContainer : IVertexContainer
    {
        public List<Vertex> Vertices { get; }
        public List<VertexTexture> VertexTextures { get; }
        public List<VertexNormal> VertexNormals { get; }
        public List<VertexPoint> VertexPoints { get; }

        public VertexContainer()
        {
            Vertices = new List<Vertex>();
            VertexTextures = new List<VertexTexture>();
            VertexNormals = new List<VertexNormal>();
            VertexPoints = new List<VertexPoint>();
        }

        public void Add(VertexData vertex)
        {
            switch (vertex)
            {
                case Vertex v:
                    Vertices.Add(v);
                    break;
                
                case VertexTexture vt:
                    VertexTextures.Add(vt);
                    break;
                
                case VertexNormal vn:
                    VertexNormals.Add(vn);
                    break;
                
                case VertexPoint vp:
                    VertexPoints.Add(vp);
                    break;
                
                default:
                    throw new PackException("Undefined vertex data type!");
            }
        }
    }
}
