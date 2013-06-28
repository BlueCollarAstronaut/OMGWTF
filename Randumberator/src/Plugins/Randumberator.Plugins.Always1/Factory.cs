using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.Always1
{
    public class Factory
        : IPluginFactory
    {
        public Factory()
        { }

        public PluginBase GetPlugin()
        {
            return new Plugin();
        }
    }
}
