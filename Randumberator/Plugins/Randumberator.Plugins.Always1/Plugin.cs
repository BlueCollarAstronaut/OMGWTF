using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.Always1
{
    [PluginFactory(typeof(Factory))]
    public class Plugin
        : PluginBase
    {
        protected override bool AmIValid()
        {
            return true;
        }
        public override int Randomize(int pool)
        {
            return 1;
        }
    }
}
