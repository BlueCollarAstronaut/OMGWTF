using Randumberator.Core;
using System;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.Random64
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
            // Create a random 64-bit seed for the Random object

            byte[] bytes = new byte[8];
            new Random().NextBytes(bytes);
            var seed = BitConverter.ToInt64(bytes, 0);

            var rnd = new System.Random((int)seed);
            return rnd.Next(0, pool + 1);
        }
    }
}
