using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Core.FailSafe
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
            return Engine.Randomizer.Randomize(pool);
        }
    }
}
