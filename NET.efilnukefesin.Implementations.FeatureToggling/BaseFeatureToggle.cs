using NET.efilnukefesin.Contracts.FeatureToggling;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public abstract class BaseFeatureToggle : BaseObject, IFeatureToggle
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
