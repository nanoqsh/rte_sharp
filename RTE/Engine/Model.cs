using RTE.Engine.Shaders;
using RTE.Engine.Vectors;
using RTE.OBJReader;
using RTE.OBJReader.Data;
using RTE.OBJReader.OBJFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RTE.Engine
{
    class Model : IDisposable
    {
        private ArrayObject<Vector5> arrayObject;
        private ShaderProgram shaderProgram;

        public Model(string modelName, ShaderProgram shader)
        {
            string path = Environment.CurrentDirectory + "/Models/" + modelName;

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found!");

            string objText = File.ReadAllText(path);
            IEnumerable<DataObject> objects = Reader.TextToObjects(objText);

            DataObject data = objects.First();

            List<Vector5> vertexData = new List<Vector5>();

            foreach (DataFace face in data.Faces)
            {
                for (int i = 0; i < face.Vertices.Length; i++)
                {
                    Vector5 vec = new Vector5(
                        face.Vertices[i].Data[0],
                        face.Vertices[i].Data[1],
                        face.Vertices[i].Data[2],
                        face.VertexTextures[i].Data[0],
                        face.VertexTextures[i].Data[1]
                        );

                    vertexData.Add(vec);
                }
            }

            List<IFaceUnit> faces = new List<IFaceUnit>();

            foreach (DataFace face in data.Faces)
            {
                faces.AddRange(face.OBJFace.Units);
            }

            uint[] indexes = Indexer.GetIndexes(faces.ToArray()).ToArray();

            arrayObject = new ArrayObject<Vector5>(vertexData.ToArray());

            arrayObject.AddAttributes(
                new Attribute("coord", shader.GetAttribute("coord"), 3, 5, 0),
                new Attribute("texCoord", shader.GetAttribute("texCoord"), 2, 5, 3)
                );

            arrayObject.SetDrawMode(DrawMode.Triangles);

            shaderProgram = shader;
        }

        public void Dispose()
        {
            arrayObject.Dispose();
        }

        public void Draw()
        {
            shaderProgram.Enable();
            arrayObject.Draw();
            shaderProgram.Disable();
        }
    }
}
