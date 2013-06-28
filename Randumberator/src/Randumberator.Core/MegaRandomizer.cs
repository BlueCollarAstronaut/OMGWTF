using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core.Abstractions;

namespace Randumberator.Core
{
    public class MegaRandomizer
     : IRandomizer
    {
        private const int hoursInDay = 24;
        private const int minutesInHours = 60;
        private const int secondsInMinutes = 60;
        private const int millisecondsInSeconds = 1000;


        // Creates a random seed to be used by the Random object to get random value.
        private int GetRandomSeed()
        {
            return new Random().Next();
        }

        // Returns a random value from the specified pool of integers.
        public int Randomize(int pool)
        {
            try
            {
                // Random in and of itself isn't bad, per se, but it needs to be
                // seeded to properly randomize things.   Using a timestamp helps 
                // a little, but to get truly random, add a random amount of time
                // before using the time for a seed.
                var now = DateTime.Now;
                var seedTimeSpan = new TimeSpan(
                    new Random(GetRandomSeed()).Next(hoursInDay + 1),
                    new Random(GetRandomSeed()).Next(minutesInHours + 1),
                    new Random(GetRandomSeed()).Next(secondsInMinutes + 1),
                    new Random(GetRandomSeed()).Next(millisecondsInSeconds + 1));
                var seededTime = now.Add(seedTimeSpan);
                var seed = (int)seedTimeSpan.Ticks;
                return new Random(seed).Next(pool + 1);
            }
            catch
            {
                // If there are problems here, then use the failsafe version.
                var failSafe = new FailSafe.FailsafeRandomizer();
                return failSafe.Randomize(pool);
            }


        }
    }
}

