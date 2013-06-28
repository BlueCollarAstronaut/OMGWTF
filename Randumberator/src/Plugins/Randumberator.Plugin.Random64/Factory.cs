using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.Random64
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
