using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;

namespace Randumberator.FailSafe
{
    public class FailsafePlugin
        : PluginBase
    {
        protected override bool AmIValid()
        {
            return true;
        }
        public override int Randomize(int pool)
        {
            var rnd = new Random();
            return rnd.Next(pool);
        }
    }
}
