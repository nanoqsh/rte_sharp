using System;
using OpenGLEngine.Engine;


namespace OpenGLEngine
{
    class Program
    {
        const int WIDTH = 600;
        const int HEIGHT = 600;

        static void Main(string[] args)
        {
            Game game = new Game(WIDTH, HEIGHT, "RTE", 1);

            Console.WriteLine(game.VideoVersion);
            Console.WriteLine(game.GetDebugInfo());

            game.Run(10);
        }
    }
}
