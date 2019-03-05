using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Shaders
{
    class ShaderProgram : IDisposable
    {
        public readonly int Index;
        public readonly Shader[] Shaders;
        
        private readonly Dictionary<int, Uniform> uniforms;

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
                out int linkStatus);

            if (linkStatus == 0)
                throw new Exception("Error attach shaders!");
            
            uniforms = new Dictionary<int, Uniform>();

            foreach (Shader shader in shaders)
                GL.DeleteShader(shader.Index);
        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DeleteProgram(Index);
        }

        public Dictionary<string, int> GetAttributes(params string[] names)
        {
            return names
                .ToDictionary(n => n, n => GL.GetAttribLocation(Index, n));
        }

        public Dictionary<string, int> GetUniforms(params string[] names)
        {
            return names
                .ToDictionary(n => n, n => GL.GetUniformLocation(Index, n));
        }

        public int GetAttribute(string name)
        {
            return GL.GetAttribLocation(Index, name);
        }

        public int GetUniform(string name)
        {
            return GL.GetUniformLocation(Index, name);
        }

        public void AddUniforms(params Uniform[] uniforms)
        {
            foreach (Uniform uniform in uniforms)
            {
                int key = GetUniform(uniform.Name);

                if (key >= 0)
                    this.uniforms.Add(key, uniform);
            }
        }

        public void ClearUniforms()
        {
            uniforms.Clear();
        }

        public void Enable()
        {
            GL.UseProgram(Index);
        }

        public void BindUniforms()
        {
            foreach (KeyValuePair<int, Uniform> pair in uniforms)
                pair.Value.Bind(pair.Key);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }
    }
}
