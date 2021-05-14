using RockPaperScissors.Basics;
using RockPaperScissors.Factory;
using RockPaperScissors.Gameplay;
using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main()
        {
            var factory = new StrategyFactory<IRPSStrategy>();

            Console.Write("Player One Name? ");

            string p1Name = Console.ReadLine();

            Console.Write($"Player One Strategy [{factory.Options}]? ");

            string p1Strategy = Console.ReadLine();

            Console.Write("Player Two Name? ");

            string p2Name = Console.ReadLine();

            Console.Write($"Player Two Strategy [{factory.Options}]? ");

            string p2Strategy = Console.ReadLine();

            Console.WriteLine();

            Console.Write("How many throws? ");

            int count = int.Parse(Console.ReadLine());

            Console.WriteLine();

            Game game = new Game(new Player(p1Name, factory.Create(p1Strategy)), new Player(p2Name, factory.Create(p2Strategy)));

            for (int i = 0; i < count; ++i)
                game.Iterate();
        }
    }
}
