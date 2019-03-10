using System;
using OpenTK.Graphics.OpenGL4;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    class ShaderProgram : IDisposable
    {
        public readonly int Index;

        public ShaderProgram(params Shader[] shaders)
        {
            Index = GL.CreateProgram();

            foreach (Shader shader in shaders)
                GL.AttachShader(Index, shader.Index);
            
            GL.LinkProgram(Index);

            GL.GetProgram(
                Index,
                GetProgramParameterName.LinkStatus,
                out int linkStatus);

            if (linkStatus == 0)
                throw new Exception("Error attach shaders!");

            foreach (Shader shader in shaders)
                GL.DeleteShader(shader.Index);
        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DeleteProgram(Index);
        }

        public int GetAttributeIndex(string name)
        {
            return GL.GetAttribLocation(Index, name);
        }

        public int GetUniformIndex(string name)
        {
            return GL.GetUniformLocation(Index, name);
        }

        public void Enable()
        {
            GL.UseProgram(Index);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }
    }
}
