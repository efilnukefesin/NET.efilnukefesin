using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class StaticFeatureToggle : BaseFeatureToggle
    {
        #region Properties
        private bool isActive { get; set; }

        #endregion Properties

        #region Construction

        public StaticFeatureToggle(string Name, bool IsActive)
        {
            this.Name = Name;
            this.isActive = IsActive;
        }

        #endregion Construction

        #region Methods

        #region GetIsActive
        public override bool GetIsActive()
        {
            return this.isActive;
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
