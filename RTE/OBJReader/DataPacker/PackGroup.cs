using System.Collections.Generic;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    public class PackGroup : IPackElementContainer
    {
        public readonly string Name;

        public List<IPackElement<Face>> Faces => packElementContainer.Faces;
        public List<IPackElement<Line>> Lines => packElementContainer.Lines;
        public List<IPackElement<Point>> Points => packElementContainer.Points;

        private readonly PackElementContainer packElementContainer;

        public PackGroup(string name = Group.DefaultName)
        {
            Name = name;
            packElementContainer = new PackElementContainer();
        }

        public void Add(IPackElement<Element> packElement) =>
            packElementContainer.Add(packElement);
    }
}
