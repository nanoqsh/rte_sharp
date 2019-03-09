using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using RTE.Engine.Shaders;

namespace RTE.Engine
{
    class ShaderProgram : IDisposable
    {
        public readonly int Index;
        public readonly Shader[] Shaders;

        public readonly Dictionary<int, Uniform> Uniforms;

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
            
            Uniforms = new Dictionary<int, Uniform>();

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

        public void AddUniforms(params Uniform[] uniforms)
        {
            foreach (Uniform uniform in uniforms)
            {
                int key = GetUniformIndex(uniform.Name);

                if (key >= 0)
                    Uniforms.Add(key, uniform);
            }
        }

        public void ClearUniforms()
        {
            Uniforms.Clear();
        }

        public void Enable()
        {
            GL.UseProgram(Index);
        }

        public void BindUniforms()
        {
            foreach (KeyValuePair<int, Uniform> pair in Uniforms)
                pair.Value.Bind(pair.Key);
        }

        public void BindUniform(int uniformKey)
        {
            Uniforms[uniformKey].Bind(uniformKey);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }
    }
}
