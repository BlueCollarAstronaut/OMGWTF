using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Core.FailSafe
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
