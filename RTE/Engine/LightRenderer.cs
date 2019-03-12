using RTE.Engine.Uniforms;

namespace RTE.Engine
{
    class LightRenderer
    {
        public readonly UniformVector3 Color;
        public readonly UniformVector3 Position;

        public LightRenderer(Light light, ShaderProgram shader)
        {
            Color = new UniformVector3(
                shader.GetUniformIndex("lightColor"),
                light.Color.ToVector3()
                );

            Position = new UniformVector3(
                shader.GetUniformIndex("lightPosition"),
                light.Position
                );
        }

        public void Bind()
        {
            Color.Bind();
            Position.Bind();
        }
    }
}
