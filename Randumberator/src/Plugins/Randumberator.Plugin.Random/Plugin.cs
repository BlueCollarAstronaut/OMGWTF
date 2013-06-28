using Randumberator.Core;
using System;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.Random
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
            var rnd = new System.Random();
            return rnd.Next(0, pool + 1);
        }
    }
}
