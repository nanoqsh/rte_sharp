using System.Collections.Generic;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    class PackElementContainer : IPackElementContainer
    {
        public List<IPackElement<Face>> Faces { get; }
        public List<IPackElement<Line>> Lines { get; }
        public List<IPackElement<Point>> Points { get; }

        public PackElementContainer()
        {
            Faces = new List<IPackElement<Face>>();
            Lines = new List<IPackElement<Line>>();
            Points = new List<IPackElement<Point>>();
        }
        
        public void Add(IPackElement<Element> packElement)
        {
            switch (packElement)
            {
                case IPackElement<Face> df:
                    Faces.Add(df);
                    break;
                
                case IPackElement<Line> dl:
                    Lines.Add(dl);
                    break;
                
                case IPackElement<Point> dp:
                    Points.Add(dp);
                    break;
                
                default:
                    throw new PackException("Undefined element type!");
            }
        }
    }
}
