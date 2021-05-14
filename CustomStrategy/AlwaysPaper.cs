using RockPaperScissors.Basics;

namespace CustomStrategy
{
    public class AlwaysPaper : IRPSStrategy
    {
        public Sign Throw()
        {
            return Sign.Paper;
        }
    }
}
