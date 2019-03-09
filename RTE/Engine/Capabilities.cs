using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    static class Capabilities
    {
        private static bool depthTest;
        public static bool DepthTest
        {
            get => depthTest;
            set
            {
                if (value && !depthTest)
                    GL.Enable(EnableCap.DepthTest);
                else if (!value && depthTest)
                    GL.Disable(EnableCap.DepthTest);

                depthTest = value;
            }
        }

        private static bool stencilTest;
        public static bool StencilTest
        {
            get => stencilTest;
            set
            {
                if (value && !stencilTest)
                    GL.Enable(EnableCap.StencilTest);
                else if (!value && stencilTest)
                    GL.Disable(EnableCap.StencilTest);

                stencilTest = value;
            }
        }

        static Capabilities()
        {
            GL.Disable(EnableCap.DepthTest);
            depthTest = false;

            GL.Disable(EnableCap.StencilTest);
            stencilTest = false;
        }
    }
}
