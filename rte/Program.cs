using System;
using RTE.Engine;


namespace RTE
{
    class Program
    {
        const int WIDTH = 800;
        const int HEIGHT = 600;

        static void Main(string[] args)
        {
            Game game = new Game(WIDTH, HEIGHT, "RTE", 1);

            Console.WriteLine(game.VideoVersion);
            Console.WriteLine(game.GetDebugInfo());
            
            game.Run(30);
        }
    }
}
