using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;


namespace OpenGLEngine.Engine
{
    class ShaderProgram : IDisposable
    {
        public readonly int Index;
        public readonly Shader[] Shaders;

        public ShaderProgram(params Shader[] shaders)
        {
            Shaders = shaders;
            Index = GL.CreateProgram();

            foreach (Shader shader in shaders)
                GL.AttachShader(Index, shader.Index);
            

            GL.LinkProgram(Index);

            GL.GetProgram(
                Index,
                GetProgramParameterName.LinkStatus,
                out int linkOK);

            if (linkOK == 0)
                throw new Exception("Error attach shaders!");
        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DeleteProgram(Index);
        }

        public Dictionary<string, int> GetAttributes(params string[] names)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string name in names)
                map.Add(name, GL.GetAttribLocation(Index, name));

            return map;
        }

        public Dictionary<string, int> GetUniforms(params string[] names)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string name in names)
                map.Add(name, GL.GetUniformLocation(Index, name));

            return map;
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
