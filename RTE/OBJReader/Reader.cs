using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RTE.OBJReader.Data;
using RTE.OBJReader.DataPacker;
using RTE.OBJReader.OBJFormat;

namespace RTE.OBJReader
{
    public static class Reader
    {
        public static IEnumerable<OBJType> LoadFile(string filePath)
        {
            string path = Environment.CurrentDirectory + "/" + filePath;

            if (!File.Exists(path))
                throw new Exception("File doesn't exists! File: " + path);

            string text = File.ReadAllText(path);
            return TextToTypes(text);
        }

        public static IEnumerable<DataObject> TextToObjects(string text)
        {
            IEnumerable<OBJType> types = TextToTypes(text);
            
            return new Packer()
                .Pack(types)
                .Objects;
        }

        public static IEnumerable<OBJType> TextToTypes(string text)
        {
            return TextToTypes<OBJParser, OBJType>(new OBJParser(), text);
        }

        public static IEnumerable<R> TextToTypes<T, R>(T parser, string text)
            where T : IParser<R>
            where R : IType
        {
            return text
                .Replace("\\" + Environment.NewLine, " ")
                .Split(Environment.NewLine.ToCharArray())
                .Select(s => s.Trim())
                .Select(parser.Parse)
                .Where(res => res != null);
        }
    }
}
