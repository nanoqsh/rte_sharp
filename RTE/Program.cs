using System;
using RTE.Engine;

namespace RTE
{
    static class Program
    {
        private const int WIDTH = 900;
        private const int HEIGHT = 600;

        private static void Main(string[] args)
        {
            Game game = new Game(WIDTH, HEIGHT, "RTE", 1);

            string[] attributes = new string[]
            {
                "coord",
                "texCoord"
            };

            string[] uniforms = new string[]
            {
                "color",
                "tex",
                "projView",
                "model"
            };

            Console.WriteLine(game.VideoVersion);
            Console.WriteLine(MeshRenderer.Instance.GetDebugInfo(attributes, uniforms));
            
            game.Run(60);
        }
    }
}
