using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.FeatureToggling
{
    public interface IFeatureToggle
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Methods

        bool GetIsActive();

        #endregion Methods
    }
}
