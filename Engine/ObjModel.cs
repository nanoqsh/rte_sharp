using System;
using System.IO;


namespace OpenGLEngine.Engine
{
    class ObjModel
    {
        public readonly string Name;
        public readonly Vertex3D[] Vertices;

        public ObjModel(string name)
        {
            Name = name;
            string path = Environment.CurrentDirectory + "/Models/" + name;

            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("File {0} not found!", path));

            // Load
            Vertices = new Vertex3D[0];
        }
    }
}
