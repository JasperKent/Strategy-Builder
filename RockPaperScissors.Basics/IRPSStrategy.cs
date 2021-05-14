using System;

namespace RockPaperScissors.Basics
{
    public interface IRPSStrategy
    {
        protected readonly static Random _random = new Random();

        Sign Throw();
    }
}
