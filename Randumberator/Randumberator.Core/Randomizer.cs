using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = System.Random;

namespace Randumberator.Core
{
    public class Randomizer
    {
        private System.Random _mainCore;
        
        public Randomizer()
        {
            // It's tempting to seed the randomizer with the current time, but that's kind of 
            // predictable, so instead, add a random time span to the current time and use that
            TimeSpan randomTime = new TimeSpan(
                GetBasicRandomNumber(365),
                GetBasicRandomNumber(24),
                GetBasicRandomNumber(60),
                GetBasicRandomNumber(60),
                GetBasicRandomNumber(1000));
            var seedTime = DateTime.Now.Add(randomTime);
            _mainCore = new System.Random((int)seedTime.Ticks);
        }

        public int GetRandomNumber(int pool)
        {
            return _mainCore.Next(pool);
        }

        private int GetBasicRandomNumber(int pool)
        {
            try
            {
                // For a good random value, create a string of numbers and find the mod. of that against
                // the pool.  If possible make the string twice as long as the pool is (to give a good
                // range of randomness), but keep it to 17 chars. or things might get out of hand.
                int length = Math.Max(2*pool.ToString().Length, 17);

                var stringVal = "";
                for (int i = 0; i < length; i++)
                    stringVal += _mainCore.Next(9);

                Int64 numVal = Int64.Parse(stringVal);
                return (int)(numVal%pool);
            }
            catch
            {
                // This is too simple to be *really* random, but if there's an error,
                // it's all we've got left
                return new System.Random().Next(pool);
            }

        }

    }
}
