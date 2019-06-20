using NET.efilnukefesin.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class VersionbasedFeatureToggle : BaseFeatureToggle
    {
        #region Properties

        private Version targetVersion;
        private Type whoseVersionToUse;

        #endregion Properties

        #region Construction

        public VersionbasedFeatureToggle(string Name, Version TargetVersion, Type WhoseVersionToUse)
        {
            this.Name = Name;
            this.targetVersion = TargetVersion;
            this.whoseVersionToUse = WhoseVersionToUse;
        }

        #endregion Construction

        #region Methods

        #region GetIsActive
        public override bool GetIsActive()
        {
            Version version = AssemblyHelper.GetAssemblyVersion(this.whoseVersionToUse);
            return this.targetVersion <= version;
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
