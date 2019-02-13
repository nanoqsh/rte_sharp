using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RTE.OBJReader
{
    static class Writer
    {
        public static string FloatArrayToString(IEnumerable<float> data)
        {
            string[] textData = data
                .Select(x => x.ToString(CultureInfo.InvariantCulture))
                .ToArray();

            return string.Join(" ", textData);
        }

        public static string FloatToString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
