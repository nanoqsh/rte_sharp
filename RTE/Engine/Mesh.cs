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
    class Mesh : IDisposable
    {
        private ArrayObject<Vector5> arrayObject;
        private ShaderProgram shaderProgram;

        public Mesh(string meshName, ShaderProgram shader)
        {
            DataObject data = ReadObjectsFromFile(meshName).First();

            MeshData meshData = new MeshData(data);

            arrayObject = new ElementArrayObject<Vector5>(meshData.Vectors)
                .CreateElementBuffer(meshData.Indexes)
                .SetDrawMode(DrawMode.Triangles)
                .AddAttributes(
                    new Attribute("coord", shader.GetAttribute("coord"), 3, 5, 0),
                    new Attribute("texCoord", shader.GetAttribute("texCoord"), 2, 5, 3)
                    );

            shaderProgram = shader;
        }

        private static IEnumerable<DataObject> ReadObjectsFromFile(string meshName)
        {
            string path = Environment.CurrentDirectory + "/Models/" + meshName;

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found!");

            string objText = File.ReadAllText(path);
            return Reader.TextToObjects(objText);
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
