using RTE.OBJReader.OBJFormat;
using System.Collections.Generic;
using System.Linq;

namespace RTE.Engine
{
    public class Indexer
    {
        public readonly uint[] Indexes;
        public readonly IFaceUnit[] UniqueFaces;

        public Indexer(IFaceUnit[] faces, uint start = 0)
        {
            Dictionary<IFaceUnit, uint> units = new Dictionary<IFaceUnit, uint>();
            uint index = start;

            foreach (IFaceUnit unit in faces)
                if (!units.ContainsKey(unit))
                    units.Add(unit, index++);

            Indexes = new uint[faces.Length];

            for (int i = 0; i < Indexes.Length; i++)
                Indexes[i] = units[faces[i]];

            UniqueFaces = units.Keys.ToArray();
        }
    }
}
