using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace RTE.Engine
{
    abstract class Shader
    {
        public readonly int Index;
        public readonly string Name;

        protected abstract ShaderType ShaderType { get; }

        public Shader(string shaderName)
        {
            Name = shaderName;

            string path = Environment.CurrentDirectory + "/Shaders/" + shaderName;

            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("File {0} not found!", path));

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
