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

        public MeshData(DataObject data)
        {
            List<IFaceUnit> units = new List<IFaceUnit>();
            List<Vector5> vectors = new List<Vector5>();

            foreach (DataFace face in data.Faces)
                for (int i = 0; i < face.OBJFace.Units.Count; i++)
                    switch (face.OBJFace.Units[i])
                    {
                        case FaceUnitVTN vtn:
                            vectors.Add(new Vector5(
                                face.Vertices[i].Data[0],
                                face.Vertices[i].Data[1],
                                face.Vertices[i].Data[2],
                                face.VertexTextures[i].Data[0],
                                face.VertexTextures[i].Data[1]
                                ));
                            break;

                        default:
                            throw new Exception("Unsupported type!");
                    }

            Vectors = vectors.ToArray();
        }
    }
}
