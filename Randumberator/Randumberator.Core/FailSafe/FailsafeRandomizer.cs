using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core.Abstractions;

namespace Randumberator.Core.FailSafe
{
    public class FailsafeRandomizer
        : IRandomizer
    {
        public int Randomize(int pool)
        {
           try
           {
               return new Random().Next(pool + 1);
           }
           catch (Exception ex)
           {
               // If that didn't work, then I don't know what else to do...
               return (pool - 1);
           }

        }
    }
}
