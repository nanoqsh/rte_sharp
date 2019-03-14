using RTE.Engine.Uniforms;

namespace RTE.Engine
{
    class LightRenderer
    {
        public readonly UniformVector3[] Colors;
        public readonly UniformVector3[] Positions;

        public LightRenderer(PointLight[] lights, ShaderProgram shader)
        {
            Colors = new UniformVector3[lights.Length];
            Positions = new UniformVector3[lights.Length];

            for (int i = 0; i < lights.Length; i++)
            {
                Colors[i] = new UniformVector3(
                    shader.GetUniformIndex($"lights[{i}].color"),
                    lights[i].Color.ToVector3()
                );

                Positions[i] = new UniformVector3(
                    shader.GetUniformIndex($"lights[{i}].position"),
                    lights[i].Position
                    );
            }
        }

        public void Bind()
        {
            foreach (UniformVector3 color in Colors)
                color.Bind();

            foreach (UniformVector3 position in Positions)
                position.Bind();
        }
    }
}
