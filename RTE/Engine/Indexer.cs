using RTE.OBJReader.OBJFormat;
using System.Collections.Generic;

namespace RTE.Engine
{
    public static class Indexer
    {
        public static IEnumerable<int> GetIndexes(IFaceUnit[] faces, int start = 0)
        {
            Dictionary<IFaceUnit, int> units = new Dictionary<IFaceUnit, int>();
            int index = start;

            foreach (IFaceUnit unit in faces)
                if (!units.ContainsKey(unit))
                    units.Add(unit, index++);
            
            int[] indexes = new int[faces.Length];

            for (int i = 0; i < indexes.Length; i++)
                indexes[i] = units[faces[i]];

            return indexes;
        }
    }
}
