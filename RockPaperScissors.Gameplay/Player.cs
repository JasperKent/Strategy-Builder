using RockPaperScissors.Basics;

namespace RockPaperScissors.Gameplay
{
    public class Player
    {
        private readonly IRPSStrategy _strategy;

        public string Name { get; private set; }
        public int Score { get; set; }
        public Sign Sign { get; private set; }

        public Player(string name, IRPSStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
        }

        public void Throw()
        {
            Sign = _strategy.Throw();
        }
    }
}
