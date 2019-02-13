using System.Collections.Generic;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    public class PackSmoothingGroup : IPackElementContainer
    {
        public readonly int Number;
        
        public List<IPackElement<Face>> Faces => packElementContainer.Faces;
        public List<IPackElement<Line>> Lines => packElementContainer.Lines;
        public List<IPackElement<Point>> Points => packElementContainer.Points;

        private readonly PackElementContainer packElementContainer;
        
        public PackSmoothingGroup(int number)
        {
            Number = number;
            packElementContainer = new PackElementContainer();
        }
        
        public void Add(IPackElement<Element> packElement) =>
            packElementContainer.Add(packElement);
    }
}
