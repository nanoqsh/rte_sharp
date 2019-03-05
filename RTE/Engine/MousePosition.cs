using OpenTK;
using OpenTK.Input;

namespace RTE.Engine
{
    class MousePosition
    {
        private Vector2 lastPosition;

        public MousePosition()
        {
            lastPosition = new Vector2(
                Mouse.GetState().X,
                Mouse.GetState().Y
                );
        }

        public Vector2 Update()
        {
            Vector2 mouse = new Vector2(
                Mouse.GetState().X,
                Mouse.GetState().Y
                );

            Vector2 delta = lastPosition - mouse;
            lastPosition = mouse;

            return delta;
        }
    }
}
