using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;


namespace OpenGLEngine.Engine
{
    class ShaderProgram : IDisposable
    {
        public readonly int Index;
        public readonly Shader[] Shaders;

        private List<Texture> textures;
        private List<Uniform> uniforms;

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

            textures = new List<Texture>();
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

        public int GetAttribute(string name)
        {
            return GL.GetAttribLocation(Index, name);
        }

        public int GetUniform(string name)
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

        public void AddUniforms(params Uniform[] uniforms)
        {
            this.uniforms.AddRange(uniforms);
        }

        public void Bind()
        {
            foreach (Uniform uniform in uniforms)
                uniform.Bind(GetUniform(uniform.Name));
        }

        public void AddTextures(params Texture[] textures)
        {
            if (this.textures.Count + textures.Length > 31)
                throw new Exception("Max number of textures is 31");

            this.textures.AddRange(textures);
        }

        public void BindTextures()
        {
            for (int i = 0; i < textures.Count; i++)
            {
                GL.Uniform1(GetUniform("tex"), i);
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                GL.BindTexture(TextureTarget.Texture2D, textures[i].Index);
            }
        }
    }
}
