using System;
using RockPaperScissors.Basics;

namespace RockPaperScissors.Gameplay
{
    public class Game
    {
        private readonly Player _player1;
        private readonly Player _player2;

        public Game(Player p1, Player p2)
        {
            _player1 = p1;
            _player2 = p2;
        }

        public void Iterate()
        {
            _player1.Throw();
            _player2.Throw();

           Player winner = Winner(_player1, _player2);

            if (winner == null)
                Console.WriteLine($"Draw with {_player1.Sign}.");
            else
            {
                Player loser = winner == _player1 ? _player2 : _player1;

                ++winner.Score;

                Console.WriteLine($"{winner.Name} beats {loser.Name} with {winner.Sign} vs {loser.Sign}.");
            }

            Console.WriteLine($"{_player1.Name}: {_player1.Score}, {_player2.Name}: {_player2.Score}.");
            Console.WriteLine();
        }

        private Player Winner(Player player1, Player player2)
        {
            bool? result = Signs.Beats(player1.Sign, player2.Sign);

            return result == null ? null : result.Value ? player1 : player2;
        }
    }
}
