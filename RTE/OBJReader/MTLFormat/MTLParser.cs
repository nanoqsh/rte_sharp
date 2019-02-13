using System;
using System.Linq;

namespace RTE.OBJReader.MTLFormat
{
    public class MTLParser : IParser<MTLType>
    {
        public MTLType Parse(string line)
        {
            char[] sep = { ' ', '\t' };
            string[] words = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return null;

            string keyword = words[0];
            string[] args = words.Skip(1).ToArray();

            switch (keyword)
            {
                case "#":
                    return new Comment(new[] { line });

                default:
                    return new NotSupportedType(words);
            }
        }
    }
}
