using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.FeatureToggling
{
    public interface IFeatureToggle
    {
        #region Properties

        string Name { get; set; }

        #endregion Properties

        #region Methods

        bool GetIsActive();

        #endregion Methods
    }
}
