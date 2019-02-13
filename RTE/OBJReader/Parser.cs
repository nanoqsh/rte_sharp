using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RTE.OBJReader
{
    static class Parser
    {
        public static float ToFloat(string text)
        {
            return float.Parse(text, CultureInfo.InvariantCulture);
        }

        public static float[] ToFloatArray(IEnumerable<string> args)
        {
            return args.Select(ToFloat).ToArray();
        }
    }
}
