using RTE.OBJReader.OBJFormat;
using System.Collections.Generic;

namespace RTE.Engine
{
    public static class Indexer
    {
        public static IEnumerable<uint> GetIndexes(IFaceUnit[] faces, uint start = 0)
        {
            Dictionary<IFaceUnit, uint> units = new Dictionary<IFaceUnit, uint>();
            uint index = start;

            foreach (IFaceUnit unit in faces)
                if (!units.ContainsKey(unit))
                    units.Add(unit, index++);

            uint[] indexes = new uint[faces.Length];

            for (int i = 0; i < indexes.Length; i++)
                indexes[i] = units[faces[i]];

            return indexes;
        }
    }
}
