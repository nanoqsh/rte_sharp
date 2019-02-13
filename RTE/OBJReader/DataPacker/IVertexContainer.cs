using System.Collections.Generic;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    public interface IVertexContainer
    {
        List<Vertex> Vertices { get; }
        List<VertexTexture> VertexTextures { get; }
        List<VertexNormal> VertexNormals { get; }
        List<VertexPoint> VertexPoints { get; }

        void Add(VertexData vertex);
    }
}
