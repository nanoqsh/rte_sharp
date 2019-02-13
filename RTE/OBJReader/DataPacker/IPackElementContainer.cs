using System.Collections.Generic;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader.DataPacker
{
    interface IPackElementContainer
    {
        List<IPackElement<Face>> Faces { get; }
        List<IPackElement<Line>> Lines { get; }
        List<IPackElement<Point>> Points { get; }

        void Add(IPackElement<Element> packElement);
    }
}
