using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class VersionbasedFeatureToggle : BaseFeatureToggle
    {
        #region Properties

        #endregion Properties

        #region Construction

        public VersionbasedFeatureToggle(string Name)
        {
            this.Name = Name;
        }

        #endregion Construction

        #region Methods

        #region GetIsActive
        public override bool GetIsActive()
        {
            return false;
        }
        #endregion GetIsActive

        #region dispose
        protected override void dispose()
        {
            //do nothing here
        }
        #endregion dispose

        #endregion Methods
    }
}
