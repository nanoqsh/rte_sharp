using OpenTK;

namespace RTE.Engine
{
    class Transform
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        public Transform()
            : this(Vector3.Zero, Vector3.Zero, Vector3.One)
        { }

        public Transform(Vector3 position)
            : this(position, Vector3.Zero, Vector3.One)
        { }

        public Transform(Vector3 position, Vector3 rotation)
            : this(position, rotation, Vector3.One)
        { }

        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Matrix4 GetModel()
        {
            return
                  Matrix4.CreateScale(Scale)
                * Matrix4.CreateFromQuaternion(new Quaternion(Rotation))
                * Matrix4.CreateTranslation(Position);
        }
    }
}
