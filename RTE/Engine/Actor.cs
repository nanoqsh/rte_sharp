namespace RTE.Engine
{
    class Actor
    {
        public readonly string Name;
        public readonly Transform Transform;
        public readonly Mesh Mesh;

        public Actor(string name, Mesh mesh)
            : this(name, mesh, new Transform())
        {
        }

        public Actor(string name, Mesh mesh, Transform transform)
        {
            Name = name;
            Mesh = mesh;
            Transform = transform;
        }

        public void Draw()
        {
            Mesh.Draw();
        }
    }
}
