using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.FeatureToggling
{
    public interface IFeatureToggleManager
    {
        #region Methods

        void Add(IFeatureToggle FeatureToggle);
        bool GetValue(string Name);
        bool Exists(string Name);

        #endregion Methods
    }
}