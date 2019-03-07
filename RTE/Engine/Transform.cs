using OpenTK;

namespace RTE.Engine
{
    class Transform
    {
        private Vector3 position;
        public Vector3 Position
        {
            get => position;
        }

        private Quaternion rotation;
        public Quaternion Rotation
        {
            get => rotation;
        }

        private Vector3 scale;
        public Vector3 Scale
        {
            get => scale;
        }

        private bool isModified;
        private Matrix4 model;

        public Transform()
            : this(Vector3.Zero, Quaternion.Identity, Vector3.One)
        { }

        public Transform(Vector3 position)
            : this(position, Quaternion.Identity, Vector3.One)
        { }

        public Transform(Vector3 position, Quaternion rotation)
            : this(position, rotation, Vector3.One)
        { }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;

            isModified = true;
        }

        public Transform SetPosition(float x, float y, float z)
        {
            return SetPosition(new Vector3(x, y, z));
        }

        public Transform SetPosition(Vector3 position)
        {
            this.position = position;

            isModified = true;

            return this;
        }

        public Transform Move(float x, float y, float z)
        {
            return Move(new Vector3(x, y, z));
        }

        public Transform Move(Vector3 position)
        {
            this.position += position;

            isModified = true;

            return this;
        }

        public Transform SetRotation(float x, float y, float z)
        {
            return SetRotation(new Quaternion(x, y, z));
        }

        public Transform SetRotation(Quaternion rotation)
        {
            this.rotation = rotation;

            isModified = true;

            return this;
        }

        public Transform RotateByX(float angle)
        {
            rotation *= new Quaternion(angle, 0, 0);

            isModified = true;

            return this;
        }

        public Transform RotateByY(float angle)
        {
            rotation *= new Quaternion(0, angle, 0);

            isModified = true;

            return this;
        }

        public Transform RotateByZ(float angle)
        {
            rotation *= new Quaternion(0, 0, angle);

            isModified = true;

            return this;
        }

        public Transform SetScale(float x, float y, float z)
        {
            return SetScale(new Vector3(x, y, z));
        }

        public Transform SetScale(Vector3 scale)
        {
            this.scale = scale;

            isModified = true;

            return this;
        }

        public Transform ScaleByX(float value)
        {
            scale.X += value;

            isModified = true;

            return this;
        }

        public Transform ScaleByY(float value)
        {
            scale.Y += value;

            isModified = true;

            return this;
        }

        public Transform ScaleByZ(float value)
        {
            scale.Z += value;

            isModified = true;

            return this;
        }

        public Matrix4 GetModel()
        {
            if (isModified)
                model =
                      Matrix4.CreateScale(scale)
                    * Matrix4.CreateFromQuaternion(rotation)
                    * Matrix4.CreateTranslation(position);

            return model;
        }
    }
}
