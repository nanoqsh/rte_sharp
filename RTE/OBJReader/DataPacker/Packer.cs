using System.Collections.Generic;
using System.Linq;
using RTE.OBJReader.Data;
using RTE.OBJReader.Exceptions;
using RTE.OBJReader.OBJFormat;
using Object = RTE.OBJReader.OBJFormat.Object;

namespace RTE.OBJReader.DataPacker
{
    public class Packer
    {
        public readonly List<DataObject> Objects;
        private readonly List<PackObject> packObjects;
        private PackObject currentObject;
        private readonly VertexContainer vertexContainer;

        public Packer()
        {
            Objects = new List<DataObject>();
            packObjects = new List<PackObject>();
            vertexContainer = new VertexContainer();
        }

        public Packer Pack(IEnumerable<OBJType> types)
        {
            if (types == null)
                throw new PackException("Types are null!");
            
            foreach (OBJType type in types)
                switch (type)
                {
                    case Object o:
                        PackObject obj = new PackObject(o.Name);
                        packObjects.Add(obj);
                        currentObject = obj;
                        break;
                    
                    case NotSupportedType _:
                    case Comment _:
                        break;
                    
                    default:
                        if (packObjects.Count == 0)
                            throw new PackException("The object is not defined!");
                        
                        PackTo(type);
                        break;
                }

            foreach (PackObject packObject in packObjects)
                AddDataObject(packObject);

            return this;
        }

        private void PackTo(OBJType type)
        {
            switch (type)
            {
                case VertexData vertex:
                    currentObject.Add(vertex);
                    vertexContainer.Add(vertex);
                    break;

                case Face f:
                    currentObject.Add(
                        new PackFace(f).PackVertices(vertexContainer)
                        );
                    break;

                case Line l:
                    currentObject.Add(
                        new PackLine(l).PackVertices(vertexContainer)
                    );
                    break;

                case Point p:
                    currentObject.Add(
                        new PackPoint(p).PackVertices(vertexContainer)
                    );
                    break;

                case UseMaterial usemtl:
                    currentObject.AddMaterial(usemtl.Name);
                    break;

                case SmoothingGroup s:
                    currentObject.AddSmoothingGroup(s.Number);
                    break;

                case Group g:
                    currentObject.AddGroups(
                        g.Names.Distinct().ToArray()
                        );
                    break;
                
                case MapLibrary _:
                case UseMap _:
                case MaterialLibrary _:
                case MergingGroup _:
                    break;

                default:
                    throw new PackException($"Undefined type {type.GetType()}");
            }
        }

        private void AddDataObject(PackObject packObject)
        {
            DataFace[] faces = packObject
                .Faces
                .Select(ElementToFace)
                .Where(d => d != null)
                .ToArray();
            
            DataLine[] lines = packObject
                .Lines
                .Select(ElementToLine)
                .Where(d => d != null)
                .ToArray();
            
            DataPoint[] points = packObject
                .Points
                .Select(ElementToPoint)
                .Where(d => d != null)
                .ToArray();

            DataGroup[] groups = packObject
                .Groups
                .Values
                .Select(g => new DataGroup()
                    {
                        Name = g.Name,
                        Faces = g.Faces
                            .Select(ElementToFace)
                            .ToArray(),
                        Lines = g.Lines
                            .Select(ElementToLine)
                            .ToArray(),
                        Points = g.Points
                            .Select(ElementToPoint)
                            .ToArray()
                    })
                .ToArray();

            DataSmoothingGroup[] smoothingGroups = packObject
                .SmoothingGroups
                .Values
                .Select(g => new DataSmoothingGroup()
                    {
                        Number = g.Number,
                        Faces = g.Faces
                            .Select(ElementToFace)
                            .ToArray(),
                        Lines = g.Lines
                            .Select(ElementToLine)
                            .ToArray(),
                        Points = g.Points
                            .Select(ElementToPoint)
                            .ToArray()
                    })
                .ToArray();

            DataMaterial[] materials = packObject
                .Materials
                .Values
                .Select(m => new DataMaterial()
                    {
                        Name = m.Name,
                        Faces = m.Faces
                            .Select(ElementToFace)
                            .ToArray(),
                        Lines = m.Lines
                            .Select(ElementToLine)
                            .ToArray(),
                        Points = m.Points
                            .Select(ElementToPoint)
                            .ToArray()
                    })
                .ToArray();
            
            DataObject data = new DataObject
            {
                Name = packObject.Name,
                Vertices = packObject.Vertices.ToArray(),
                VertexTextures = packObject.VertexTextures.ToArray(),
                VertexNormals = packObject.VertexNormals.ToArray(),
                VertexPoints = packObject.VertexPoints.ToArray(),
                Faces = faces,
                Lines = lines,
                Points = points,
                Groups = groups,
                SmoothingGroups = smoothingGroups,
                Materials = materials
            };

            Objects.Add(data);
        }
        
        private static DataFace ElementToFace(IPackElement<Face> face)
        {
            if (face is PackFace pack)
                return new DataFace
                {
                    Vertices = pack.Vertices.ToArray(),
                    VertexTextures = pack.VertexTextures.ToArray(),
                    VertexNormals = pack.VertexNormals.ToArray(),
                    OBJFace = pack.OBJElement
                };
        
            return null;
        }
        
        private static DataLine ElementToLine(IPackElement<Line> line)
        {
            if (line is PackLine pack)
                return new DataLine
                {
                    Vertices = pack.Vertices.ToArray(),
                    VertexTextures = pack.VertexTextures.ToArray(),
                    OBJLine = pack.OBJElement
                };
            
            return null;
        }

        private static DataPoint ElementToPoint(IPackElement<Point> point)
        {
            if (point is PackPoint pack)
                return new DataPoint
                {
                    Vertices = pack.Vertices.ToArray(),
                    OBJPoint = pack.OBJElement
                };
                
            return null;
        }
    }
}
