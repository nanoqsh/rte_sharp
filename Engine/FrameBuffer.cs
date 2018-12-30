using System;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class FrameBuffer : IDisposable
    {
        public readonly int Index;
        public readonly Texture Frame;

        private readonly int renderBuffer;

        public FrameBuffer(int width, int height)
        {
            Index = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Index);

            Frame = new Texture(width, height, "FrameBuffer #" + Index);
            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D,
                Frame.Index,
                0
                );

            // Gen Renderbuffer for depth and -stencil-
            renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);
            GL.RenderbufferStorage(
                RenderbufferTarget.Renderbuffer,
                RenderbufferStorage.DepthComponent24,
                width,
                height
                );

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.FramebufferRenderbuffer(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthAttachment,
                RenderbufferTarget.Renderbuffer,
                renderBuffer
                );

            // Check complition
            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw new Exception(string.Format(
                    "Error do not complete! Status: {0}",
                    status
                    ));

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(Index);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Index);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
