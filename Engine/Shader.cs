using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace OpenGLEngine.Engine
{
    class Shader
    {
        public readonly int Index;
        public readonly string Name;

        public Shader(string shaderName, ShaderType shaderType)
        {
            Name = shaderName;

            string path = Environment.CurrentDirectory + "/Shaders/" + shaderName;

            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("File {0} not found!", path));

            string shaderCode = File.ReadAllText(path);
            

            Index = GL.CreateShader(shaderType);
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
