using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class TimebasedFeatureToggle : BaseFeatureToggle
    {
        #region Properties

        private DateTimeOffset triggerTime;

        #endregion Properties

        #region Construction

        public TimebasedFeatureToggle(string Name, DateTimeOffset TriggerTime)
        {
            this.Name = Name;
            this.triggerTime = TriggerTime;
        }

        #endregion Construction

        #region Methods

        #region GetIsActive
        public override bool GetIsActive()
        {
            return this.triggerTime <= DateTimeOffset.Now;
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
