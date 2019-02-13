using System.Collections.Generic;
using System.Linq;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    public class PackObject : IVertexContainer, IPackElementContainer
    {
        public readonly string Name;

        public readonly Dictionary<string, PackGroup> Groups;
        private string[] currentGroups;

        public readonly Dictionary<string, PackMaterial> Materials;
        private string currentMaterial;
        
        public readonly Dictionary<int, PackSmoothingGroup> SmoothingGroups;
        private int currentSmoothingGroup;

        public List<Vertex> Vertices => vertexContainer.Vertices;
        public List<VertexTexture> VertexTextures => vertexContainer.VertexTextures;
        public List<VertexNormal> VertexNormals => vertexContainer.VertexNormals;
        public List<VertexPoint> VertexPoints => vertexContainer.VertexPoints;

        private readonly VertexContainer vertexContainer;

        public void Add(VertexData vertex) => vertexContainer.Add(vertex);

        public List<IPackElement<Face>> Faces => packElementContainer.Faces;
        public List<IPackElement<Line>> Lines => packElementContainer.Lines;
        public List<IPackElement<Point>> Points => packElementContainer.Points;

        private readonly PackElementContainer packElementContainer;
        
        public PackObject(string name)
        {
            Name = name;
            
            vertexContainer = new VertexContainer();
            packElementContainer = new PackElementContainer();

            Groups = new Dictionary<string, PackGroup>
            {
                {Group.DefaultName, new PackGroup()}
            };

            currentGroups = Groups.Keys.ToArray();

            Materials = new Dictionary<string, PackMaterial>();
            SmoothingGroups = new Dictionary<int, PackSmoothingGroup>();
        }

        public void Add(IPackElement<Element> packElement)
        {
            packElementContainer.Add(packElement);

            foreach (string group in currentGroups)
                Groups[group].Add(packElement);

            if (currentMaterial != null)
                Materials[currentMaterial].Add(packElement);
            
            if (currentSmoothingGroup != 0)
                SmoothingGroups[currentSmoothingGroup].Add(packElement);
        }

        public void AddGroups(string[] names)
        {
            currentGroups = names;

            foreach (string name in names)
                if (!Groups.ContainsKey(name))
                    Groups.Add(name, new PackGroup(name));
        }

        public void AddMaterial(string name)
        {
            currentMaterial = name;
            
            if (!Materials.ContainsKey(name))
                Materials.Add(name, new PackMaterial(name));
        }

        public void AddSmoothingGroup(int number)
        {
            currentSmoothingGroup = number;
            
            if (!SmoothingGroups.ContainsKey(number) && number != 0)
                SmoothingGroups.Add(number, new PackSmoothingGroup(number));
        }
    }
}
