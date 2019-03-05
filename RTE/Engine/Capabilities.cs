using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine
{
    class Capabilities
    {
        private static Capabilities instance;
        public static Capabilities Instance
        {
            get
            {
                if (instance == null)
                    instance = new Capabilities();

                return instance;
            }
        }

        private bool depthTest;
        public bool DepthTest
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

        private bool stencilTest;
        public bool StencilTest
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

        private Capabilities()
        {
            GL.Disable(EnableCap.DepthTest);
            depthTest = false;

            GL.Disable(EnableCap.StencilTest);
            stencilTest = false;
        }
    }
}
