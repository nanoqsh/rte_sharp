using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.Data
{
    public class DataObject
    {
        public string Name;
        
        // Vertex Data
        public Vertex[] Vertices;
        public VertexPoint[] VertexPoints;
        public VertexNormal[] VertexNormals;
        public VertexTexture[] VertexTextures;
        
        // Elements
        public DataFace[] Faces;
        public DataLine[] Lines;
        public DataPoint[] Points;
        
        // Grouping
        public DataGroup[] Groups;
        public DataSmoothingGroup[] SmoothingGroups;
        public DataMaterial[] Materials;
    }
}
