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

        #region Constants

        #endregion Constants

        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        public abstract bool GetIsActive();

        #endregion Methods

        #region Events

        #endregion Events
    }
}
