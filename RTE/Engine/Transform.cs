using OpenTK;
using System;

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
            position.X = x;
            position.Y = y;
            position.Z = z;

            isModified = true;

            return this;
        }

        public Transform SetPosition(Vector3 position)
        {
            this.position = position;

            isModified = true;

            return this;
        }

        public Transform Move(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;

            isModified = true;

            return this;
        }

        public Transform Move(Vector3 position)
        {
            this.position += position;

            isModified = true;

            return this;
        }

        public Transform SetRotation(float x, float y, float z)
        {
            rotation = new Quaternion(x, y, z);

            isModified = true;

            return this;
        }

        public Transform SetRotation(Vector3 rotation)
        {
            this.rotation = new Quaternion(rotation);

            isModified = true;

            return this;
        }

        public Transform SetRotation(Quaternion rotation)
        {
            this.rotation = rotation;

            isModified = true;

            return this;
        }

        public Transform Rotate(float x, float y, float z)
        {
            return Rotate(new Quaternion(x, y, z));
        }

        public Transform Rotate(Vector3 rotation)
        {
            return Rotate(new Quaternion(rotation));
        }

        public Transform Rotate(Quaternion rotation)
        {
            this.rotation *= rotation;

            isModified = true;

            return this;
        }

        public Transform RotateByX(float radians)
        {
            rotation *= new Quaternion(radians, 0, 0);

            isModified = true;

            return this;
        }
        
        public Transform RotateByY(float radians)
        {
            rotation *= new Quaternion(0, radians, 0);

            isModified = true;

            return this;
        }

        public Transform RotateByZ(float radians)
        {
            rotation *= new Quaternion(0, 0, radians);

            isModified = true;

            return this;
        }

        public Transform RotateAroundAxis(Vector3 axis, float radians)
        {
            rotation *= Quaternion.FromAxisAngle(axis, radians);

            isModified = true;

            return this;
        }

        public Transform SetScale(float x, float y, float z)
        {
            scale.X = x;
            scale.Y = y;
            scale.Z = z;

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
                    * Matrix4.CreateFromQuaternion(rotation)
                    * Matrix4.CreateTranslation(position);

            isModified = false;

            return model;
        }
    }
}
