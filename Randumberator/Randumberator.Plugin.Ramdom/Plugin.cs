using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugin.Ramdom
{
    public class Plugin
        : PluginBase
    {
        protected override bool AmIValid()
        {
            return true;
        }
        public override int Randomize(int pool)
        {
            return (int)(System.Environment.WorkingSet % pool);
        }
    }
}
