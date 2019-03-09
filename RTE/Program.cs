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

            Console.WriteLine(game.VideoVersion);
            
            game.Run(60);
        }
    }
}
