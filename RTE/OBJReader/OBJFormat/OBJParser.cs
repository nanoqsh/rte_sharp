using System;
using System.Linq;

namespace RTE.OBJReader.OBJFormat
{
    public class OBJParser : IParser<OBJType>
    {
        public OBJType Parse(string line)
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

                case "v":
                    return new Vertex(args);
                
                case "vp":
                    return new VertexPoint(args);
                
                case "vn":
                    return new VertexNormal(args);

                case "vt":
                    return new VertexTexture(args);

                case "f":
                    return new Face(args);

                case "o":
                    return new Object(args);
                
                case "p":
                    return new Point(args);
                
                case "l":
                    return new Line(args);
                
                case "g":
                    return new Group(args);
                
                case "s":
                    return new SmoothingGroup(args);
                
                case "mg":
                    return new MergingGroup(args);
                
                case "usemtl":
                    return new UseMaterial(args);
                
                case "usemap":
                    return new UseMap(args);
                
                case "mtllib":
                    return new MaterialLibrary(args);
                
                case "maplib":
                    return new MapLibrary(args);

                case "call":
                case "scmp":
                case "csh":
                    throw new Exception("Unsafe code!");

                default:
                    return new NotSupportedType(words);
            }
        }
    }
}
