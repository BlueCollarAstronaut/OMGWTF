using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Plugins
{
    public interface IPluginFactory
    {
        PluginBase GetPlugin();
    }
}
