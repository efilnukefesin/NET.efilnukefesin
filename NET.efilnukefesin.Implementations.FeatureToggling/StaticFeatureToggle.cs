using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class StaticFeatureToggle : BaseFeatureToggle
    {
        #region Construction

        public StaticFeatureToggle(string Name, bool IsActive)
        {
            this.Name = Name;
            this.IsActive = IsActive;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            //do nothing here
        }
        #endregion dispose

        #endregion Methods
    }
}
