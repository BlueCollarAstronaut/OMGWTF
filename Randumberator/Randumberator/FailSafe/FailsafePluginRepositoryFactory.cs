using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;

namespace Randumberator.FailSafe
{
    public class FailsafePluginRepositoryFactory
        : IPluginRepositoryFactory
    {

        public IPluginRepository GetRepository()
        {
           return new FailsafePluginRepository();
        }
    }
}
