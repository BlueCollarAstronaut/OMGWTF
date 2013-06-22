using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;

namespace Randumberator.FailSafe
{
    public class FailsafePluginFactory
        : IPluginFactory
    {
        public PluginBase GetPlugin()
        {
            return new FailsafePlugin();
        }
    }
}
