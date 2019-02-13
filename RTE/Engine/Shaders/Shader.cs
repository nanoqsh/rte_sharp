using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace RTE.Engine.Shaders
{
    abstract class Shader
    {
        public readonly int Index;
        public readonly string Name;

        protected abstract ShaderType ShaderType { get; }

        protected Shader(string shaderName)
        {
            Name = shaderName;

            string path = Environment.CurrentDirectory + "/Shaders/" + shaderName;

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found!");

            string shaderCode = File.ReadAllText(path);

            Index = GL.CreateShader(ShaderType);
            GL.ShaderSource(Index, shaderCode);
            GL.CompileShader(Index);
        }

        public string GetLogInfo()
        {
            GL.GetShaderInfoLog(Index, out string infoLog);
            return infoLog;
        }
    }
}
