using Randumberator.Core;
using System;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.SuperRandom
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
            // Create a random 32-bit seed for the Random object

            byte[] bytes = new byte[4];
            new Random().NextBytes(bytes);
            var seed = BitConverter.ToInt32(bytes, 0);

            var rnd = new System.Random(seed);
            return rnd.Next(0, pool + 1);
        }
    }
}
