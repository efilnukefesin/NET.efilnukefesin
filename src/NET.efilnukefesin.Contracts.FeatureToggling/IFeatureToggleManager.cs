using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.FeatureToggling
{
    public interface IFeatureToggleManager
    {
        #region Methods

        #region Add
        /// <summary>
        /// adds a new featuretoggle
        /// </summary>
        /// <param name="FeatureToggle">the featuretoggle to add</param>
        /// <returns>wether adding was successful or not</returns>
        bool Add(IFeatureToggle FeatureToggle);
        #endregion Add

        #region GetValue: returns wether the toggle is on or off
        /// <summary>
        /// returns wether the toggle is on or off
        /// </summary>
        /// <param name="Name">the name of the toggle</param>
        /// <returns>the value of the toggle or false if not found</returns>
        bool GetValue(string Name);
        #endregion GetValue

        #region Exists: indicates, if a Feature Toggle with the specified name already exists
        /// <summary>
        /// indicates, if a Feature Toggle with the specified name already exists
        /// </summary>
        /// <param name="Name">the name of the Toggle</param>
        /// <returns>true if it is already known, false if not</returns>
        bool Exists(string Name);
        #endregion Exists

        #region Clear: resets all known toggles to zero
        /// <summary>
        /// resets all known toggles to zero
        /// </summary>
        void Clear();
        #endregion Clear

        #endregion Methods
    }
}