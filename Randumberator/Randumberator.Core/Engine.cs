using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;
using System.Threading;

namespace Randumberator.Core
{
    public static class Engine
    {
        private static IRandomizer _randomizer;
        public static IRandomizer Randomizer
        {
            get { return _randomizer ?? new MegaRandomizer(); }
            set { _randomizer = value; }
        }

        private static IPluginRepositoryFactory _pluginRepositoryFactory;
        public static IPluginRepositoryFactory PluginRepositoryFactory
        {
            get { return _pluginRepositoryFactory ?? new FailsafePluginRepositoryFactory(); }
            set { _pluginRepositoryFactory = value; }
        }

        private static IPluginRepository _pluginRepository;
        public static IPluginRepository PluginRepository
        {
            get
            {
                _pluginRepository = PluginRepositoryFactory.GetRepository();
                return _pluginRepository;
            }
        }

        private static IPluginPicker _pluginPicker;
        public static IPluginPicker PluginPicker
        {
            get { return _pluginPicker ?? new FailsafePluginPicker(); }
            set { _pluginPicker = value; }
        }
    }
}
