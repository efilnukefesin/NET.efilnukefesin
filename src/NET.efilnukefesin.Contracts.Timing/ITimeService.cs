using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Timing
{
    public interface ITimeService : IBaseObject
    {
        #region Properties

        #region ElapsedTimeAbsolute: the time which has been elapsed in the real world since creation of this Service
        /// <summary>
        /// the time which has been elapsed in the real world since creation of this Service
        /// </summary>
        TimeSpan ElapsedTimeAbsolute { get; set; }
        #endregion ElapsedTimeAbsolute

        #region ElapsedTimeRelative: the time which has been passed by developer interaction using this Service; starts aligned with ElapsedTimeAbsolute
        /// <summary>
        /// the time which has been passed by developer interaction using this Service; starts aligned with <seealso cref="ElapsedTimeAbsolute"/>
        /// </summary>
        TimeSpan ElapsedTimeRelative { get; set; }
        #endregion ElapsedTimeRelative

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
