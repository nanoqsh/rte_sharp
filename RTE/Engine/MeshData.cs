using RTE.Engine.Vectors;
using RTE.OBJReader.Data;
using RTE.OBJReader.OBJFormat;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RTE.Engine
{
    class MeshData
    {
        public Vector5[] Vectors;
        public uint[] Indexes;

        public MeshData(DataObject data)
        {
            List<IFaceUnit> units = new List<IFaceUnit>();

            foreach (DataFace face in data.Faces)
                units.AddRange(face.OBJFace.Units);

            Indexes = Indexer.GetIndexes(units.ToArray()).ToArray();

            Vectors = new Vector5[Indexes.Max() + 1];

            foreach (uint index in Indexes.Distinct())
            {
                FaceUnitVT face;

                switch (units[(int) index])
                {
                    case FaceUnitVT vt:
                        face = vt;
                        break;

                    case FaceUnitVTN vtn:
                        face = new FaceUnitVT(vtn.V, vtn.T);
                        break;

                    default:
                        throw new Exception("Not supported type!");
                }

                Vertex vertex = data.Vertices[face.V - 1];
                VertexTexture vertexTexture = data.VertexTextures[face.T - 1];

                Vectors[index] = new Vector5(
                    vertex.Data[0],
                    vertex.Data[1],
                    vertex.Data[2],
                    vertexTexture.Data[0],
                    vertexTexture.Data[1]
                    );
            }
        }
    }
}
