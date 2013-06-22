using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Core.FailSafe
{
    public class FailsafePluginRepository
        : IPluginRepository
    {
        private IPluginFactory Factory = new FailsafePluginFactory();
        private PluginBase[] _plugins;

        public PluginBase[] Plugins
        {
            get
            {
             if (_plugins == null)
                 _plugins = new PluginBase[]{ Factory.GetPlugin() };
                return _plugins;
            }
        }
    }
}
