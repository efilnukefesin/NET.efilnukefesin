using NET.efilnukefesin.Contracts.FeatureToggling;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Implementations.FeatureToggling
{
    public class FeatureToggleManager : BaseObject, IFeatureToggleManager
    {
        #region Properties

        private List<IFeatureToggle> toggles;
        private ILogger logger;

        #endregion Properties

        #region Construction

        public FeatureToggleManager(ILogger Logger)
        {
            this.logger = Logger;
            this.toggles = new List<IFeatureToggle>();
        }

        #endregion Construction

        #region Methods

        #region Add
        public void Add(IFeatureToggle FeatureToggle)
        {
            this.toggles.Add(FeatureToggle);
        }
        #endregion Add

        #region GetValue
        public bool GetValue(string Name)
        {
            bool result = false;

            if (this.Exists(Name))
            {
                result = this.toggles.Where(x => x.Name.Equals(Name)).FirstOrDefault().IsActive;
            }

            return result;
        }
        #endregion GetValue

        #region Exists
        public bool Exists(string Name)
        {
            return this.toggles.Any(x => x.Name.Equals(Name));
        }
        #endregion Exists

        #region dispose
        protected override void dispose()
        {
            this.toggles.Clear();
            this.toggles = null;
            this.logger = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
