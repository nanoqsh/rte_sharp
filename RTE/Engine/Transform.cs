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

        private Vector3 rotation;
        public Vector3 Rotation
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
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;

            isModified = true;
        }

        public Transform SetPosition(Vector3 position)
        {
            this.position = position;

            isModified = true;

            return this;
        }

        public Transform Move(Vector3 position)
        {
            this.position += position;

            isModified = true;

            return this;
        }

        public Transform SetRotation(Vector3 rotation)
        {
            this.rotation = rotation;

            isModified = true;

            return this;
        }

        public Transform RotateByX(float angle)
        {
            rotation.X += angle;

            isModified = true;

            return this;
        }

        public Transform RotateByY(float angle)
        {
            rotation.Y += angle;

            isModified = true;

            return this;
        }

        public Transform RotateByZ(float angle)
        {
            rotation.Z += angle;

            isModified = true;

            return this;
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
                    * Matrix4.CreateFromQuaternion(new Quaternion(rotation))
                    * Matrix4.CreateTranslation(position);

            return model;
        }
    }
}
