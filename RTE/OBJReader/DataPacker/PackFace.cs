using System.Collections.Generic;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    class PackFace : IPackElement<Face>
    {
        public readonly List<Vertex> Vertices;
        public readonly List<VertexTexture> VertexTextures;
        public readonly List<VertexNormal> VertexNormals;
        
        public Face OBJElement { get; }

        public PackFace(Face face)
        {
            OBJElement = face;
            
            Vertices = new List<Vertex>();
            VertexTextures = new List<VertexTexture>();
            VertexNormals = new List<VertexNormal>();
        }

        public IPackElement<Face> PackVertices(IVertexContainer container)
        {
            for (int i = 0; i < OBJElement.Units.Count; i++)
                switch (OBJElement.Units[i])
                {
                    case FaceUnitV f:
                        AddVertex(container, ref f);
                        OBJElement.Units[i] = f;
                        break;
                    
                    case FaceUnitVN f:
                        AddVertex(container, ref f);
                        OBJElement.Units[i] = f;
                        break;
                    
                    case FaceUnitVT f:
                        AddVertex(container, ref f);
                        OBJElement.Units[i] = f;
                        break;
                    
                    case FaceUnitVTN f:
                        AddVertex(container, ref f);
                        OBJElement.Units[i] = f;
                        break;
                
                    default:
                        throw new PackException("Unknown face type!");
                }

            return this;
        }

        private void AddVertex(IVertexContainer container, ref FaceUnitV face)
        {
            (Vertex v, int vi) = container.Vertices.Get(face.V);
            Vertices.Add(v);
            
            face = new FaceUnitV(vi);
        }
        
        private void AddVertex(IVertexContainer container, ref FaceUnitVT face)
        {
            (Vertex v, int vi) = container.Vertices.Get(face.V);
            Vertices.Add(v);

            (VertexTexture vt, int vti) = container.VertexTextures.Get(face.T);
            VertexTextures.Add(vt);
            
            face = new FaceUnitVT(vi, vti);
        }
        
        private void AddVertex(IVertexContainer container, ref FaceUnitVN face)
        {
            (Vertex v, int vi) = container.Vertices.Get(face.V);
            Vertices.Add(v);

            (VertexNormal vn, int vni) = container.VertexNormals.Get(face.N);
            VertexNormals.Add(vn);
            
            face = new FaceUnitVN(vi, vni);
        }
        
        private void AddVertex(IVertexContainer container, ref FaceUnitVTN face)
        {
            (Vertex v, int vi) = container.Vertices.Get(face.V);
            Vertices.Add(v);
            
            (VertexTexture vt, int vti) = container.VertexTextures.Get(face.T);
            VertexTextures.Add(vt);

            (VertexNormal vn, int vni) = container.VertexNormals.Get(face.N);
            VertexNormals.Add(vn);
            
            face = new FaceUnitVTN(vi, vti, vni);
        }
    }
}
