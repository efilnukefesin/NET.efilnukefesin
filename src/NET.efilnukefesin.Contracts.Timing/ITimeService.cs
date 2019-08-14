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

        #region CurrentMultiplicator
        /// <summary>
        /// the current factor, in which ElapsedTimeRelative is increased compared to ElapsedTimeAbsolute
        /// </summary>
        double CurrentMultiplicator { get; set; }
        #endregion CurrentMultiplicator

        #region CurrentTarget: the current target, where ElapsedTimeRelative is being moved to, null if there is none
        /// <summary>
        /// the current target, where ElapsedTimeRelative is being moved to, null if there is none
        /// </summary>
        TimeSpan? CurrentTarget { get; set; }
        #endregion CurrentTarget

        #endregion Properties

        #region Methods

        #region Play: puts the progress of ElapsedTimeRelative back to standard
        /// <summary>
        /// puts the progress of ElapsedTimeRelative back to standard
        /// </summary>
        void Play();
        #endregion Play

        #region FastForward: Fast forwards ElapsedTimeRelative
        /// <summary>
        /// Fast forwards ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        void FastForward(double Multiplier);
        #endregion FastForward

        #region FastForward: Fast forwards ElapsedTimeRelative to a certain point in time since start
        /// <summary>
        /// Fast forwards ElapsedTimeRelative to a certain point in time since start
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        void FastForward(double Multiplier, TimeSpan TargetPoint);
        #endregion FastForward

        #region Rewind: Rewinds ElapsedTimeRelative
        /// <summary>
        /// Rewinds ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        void Rewind(double Multiplier);
        #endregion Rewind

        #region Rewind: Rewinds ElapsedTimeRelative to a certain point in time since start
        /// <summary>
        /// Rewinds ElapsedTimeRelative to a certain point in time since start
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        void Rewind(double Multiplier, TimeSpan TargetPoint);
        #endregion Rewind

        #region Pause: puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// <summary>
        /// puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// </summary>
        void Pause();
        #endregion Pause

        #region JumpTo: Jumps directly to a target time for ElapsedTimeRelative
        /// <summary>
        /// Jumps directly to a target time for ElapsedTimeRelative 
        /// </summary>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        void JumpTo(TimeSpan TargetPoint);
        #endregion JumpTo

        #endregion Methods
    }
}
