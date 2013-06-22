﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Core.Abstractions
{
    public interface IPluginRepositoryFactory
    {
        IPluginRepository GetRepository();
    }
}
