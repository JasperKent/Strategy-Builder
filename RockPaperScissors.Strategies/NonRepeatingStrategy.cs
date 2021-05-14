using RockPaperScissors.Basics;

namespace RockPaperScissors.Strategies
{
    public class NonRepeatingStrategy : IRPSStrategy
    {
        private Sign? _lastSign;

        public Sign Throw()
        {
            if (_lastSign == null)
                _lastSign = (Sign)IRPSStrategy._random.Next(3);
            else
            {
                var sign = (Sign)IRPSStrategy._random.Next(2);

                if (sign == _lastSign)
                    sign = (Sign)2;

                _lastSign = sign;
            }

            return _lastSign.Value;
        }
    }
}
