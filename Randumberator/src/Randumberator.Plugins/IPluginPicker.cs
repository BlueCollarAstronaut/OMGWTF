﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Plugins
{
    public interface IPluginPicker
    {
        PluginBase PickPlugin(IPluginRepository repository);
    }
}
