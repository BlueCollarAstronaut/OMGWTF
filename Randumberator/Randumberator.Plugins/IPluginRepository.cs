using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Plugins
{
    public interface IPluginRepository
    {
        PluginBase[] Plugins { get; }
    }
}
