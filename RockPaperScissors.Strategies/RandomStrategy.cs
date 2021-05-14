using RockPaperScissors.Basics;

namespace RockPaperScissors.Strategies
{
    public class RandomStrategy : IRPSStrategy
    {
        public Sign Throw()
        {
            return (Sign)IRPSStrategy._random.Next(3);
        }
    }
}
