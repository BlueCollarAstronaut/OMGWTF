using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;

namespace Randumberator.FailSafe
{
    public class FailsafePluginPicker
        : IPluginPicker
    {
        public PluginBase PickPlugin(IPluginRepository repository)
        {
            // Determine pool of available plugins
            int pool = repository.Plugins.Length;
            
            // If there are no items in the pool, then use a failsafe
            if (pool == 0)
            {
                var pluginFactory = new FailsafePluginFactory();
                return pluginFactory.GetPlugin();
            }
            
            // Otherwise, pick a plugin at random
            var selectedItem = new Random().Next(pool);
            return repository.Plugins[selectedItem];
        }
    }
}
