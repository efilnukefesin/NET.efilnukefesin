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

        #region Add: adds a new featuretoggle
        /// <summary>
        /// adds a new featuretoggle
        /// </summary>
        /// <param name="FeatureToggle">the featuretoggle to add</param>
        /// <returns>wether adding was successful or not</returns>
        public bool Add(IFeatureToggle FeatureToggle)
        {
            bool result = false;
            this.logger?.Log($"FeatureToggleManager.Add: started with '{FeatureToggle.Name}'");
            if (this.Exists(FeatureToggle.Name))
            {
                this.logger?.Log($"FeatureToggleManager.Add: '{FeatureToggle.Name}' already exists, not added!", Contracts.Logger.Enums.LogLevel.Warning);
            }
            else
            {
                this.toggles.Add(FeatureToggle);
                this.logger?.Log($"FeatureToggleManager.Add: added '{FeatureToggle.Name}'");
                result = true;
            }
            
            this.logger?.Log($"FeatureToggleManager.Add: ended with '{FeatureToggle.Name}', result is '{result}'");
            return result;
        }
        #endregion Add

        #region GetValue: returns wether the toggle is on or off
        /// <summary>
        /// returns wether the toggle is on or off
        /// </summary>
        /// <param name="Name">the name of the toggle</param>
        /// <returns>the value of the toggle or false if not found</returns>
        public bool GetValue(string Name)
        {
            bool result = false;
            this.logger?.Log($"FeatureToggleManager.GetValue: started with '{Name}'");

            if (this.Exists(Name))
            {
                result = this.toggles.Where(x => x.Name.Equals(Name)).FirstOrDefault().GetIsActive();
            }

            this.logger?.Log($"FeatureToggleManager.GetValue: ended with '{Name}', result is '{result}'");
            return result;
        }
        #endregion GetValue

        #region Exists: indicates, if a Feature Toggle with the specified name already exists
        /// <summary>
        /// indicates, if a Feature Toggle with the specified name already exists
        /// </summary>
        /// <param name="Name">the name of the Toggle</param>
        /// <returns>true if it is already known, false if not</returns>
        public bool Exists(string Name)
        {
            bool result = false;
            this.logger?.Log($"FeatureToggleManager.Exists: started with '{Name}'");
            result = this.toggles.Any(x => x.Name.Equals(Name));
            this.logger?.Log($"FeatureToggleManager.Exists: ended with '{Name}', result is '{result}'");
            return result;
        }
        #endregion Exists

        #region Clear: resets all known toggles to zero
        /// <summary>
        /// resets all known toggles to zero
        /// </summary>
        public void Clear()
        {
            this.logger?.Log($"FeatureToggleManager.Clear: started");
            this.toggles.Clear();
            this.logger?.Log($"FeatureToggleManager.Clear: ended");
        }
        #endregion Clear

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
