using System;
using OpenTK;

namespace RTE.Engine
{
    class Camera
    {
        private const float MAX_PITCH = 89.0f;
        private const float MIN_PITCH = -89.0f;

        private Vector2 rotation;

        public Vector3 Position { get; private set; }

        public Vector3 Front { get; private set; }

        public Camera(Vector3 position, Vector3 front)
            : this(position, front, Vector2.Zero)
        {}

        public Camera(Vector3 position, Vector3 front, Vector2 rotation)
        {
            Position = position;
            Front = Vector3.Normalize(front);
            this.rotation = rotation;
        }

        public Matrix4 View =>
            Matrix4.LookAt(
                Position,
                Position + Front,
                Vector3.UnitY
            );

        public void Move(Vector3 delta)
        {
            Position += delta;
        }

        private static double Convert(double x) => Math.PI / 180 * x;

        public void Rotate(Vector2 delta)
        {
            rotation.X = (rotation.X + delta.X) % 360;
            rotation.Y += delta.Y;

            if (rotation.Y > MAX_PITCH)
                rotation.Y = MAX_PITCH;

            if (rotation.Y < MIN_PITCH)
                rotation.Y = MIN_PITCH;

            Front = Vector3.Normalize(new Vector3(
                (float)Math.Sin(Convert(rotation.X)) * (float)Math.Cos(Convert(rotation.Y)),
                (float)Math.Sin(Convert(rotation.Y)),
                (float)Math.Cos(Convert(rotation.X)) * (float)Math.Cos(Convert(rotation.Y))
                ));
        }
    }
}
