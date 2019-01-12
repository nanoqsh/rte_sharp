using System;
using System.IO;
using OpenTK;


namespace RTE.Engine
{
    class ObjModel
    {
        public readonly string Name;
        public readonly Vector3[] Vertices;

        public ObjModel(string name)
        {
            Name = name;
            string path = Environment.CurrentDirectory + "/Models/" + name;

            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("File {0} not found!", path));

            // Load
            Vertices = new Vector3[0];
        }
    }
}
