using NET.efilnukefesin.Contracts.Timing;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NET.efilnukefesin.Implementations.Timing
{
    public class StandardTimeService : BaseObject, ITimeService
    {
        #region Properties

        public TimeSpan ElapsedTimeAbsolute { get; set; } = new TimeSpan();
        public TimeSpan ElapsedTimeRelative { get; set; } = new TimeSpan();

        private Timer timeKeeperTimer;

        private readonly double secondsPerCount;
        private long baseTime;
        private long previousTime;
        private long currentTime;

        #region DeltaTime: the time between two frames
        /// <summary>
        /// the time between two frames
        /// </summary>
        public double DeltaTime { get; private set; }
        #endregion DeltaTime: 

        private int updateIntervalInMilliseconds = 1;

        #region CurrentMultiplicator
        /// <summary>
        /// the current factor, in which ElapsedTimeRelative is increased compared to ElapsedTimeAbsolute
        /// </summary>
        public double CurrentMultiplicator { get; set; } = 1f;
        #endregion CurrentMultiplicator

        #region CurrentTarget: the current target, where ElapsedTimeRelative is being moved to, null if there is none
        /// <summary>
        /// the current target, where ElapsedTimeRelative is being moved to, null if there is none
        /// </summary>
        public TimeSpan? CurrentTarget { get; set; } = null;
        #endregion CurrentTarget

        #endregion Properties

        #region Construction

        public StandardTimeService(int UpdateIntervalInMilliseconds = 1)
        {
            Debug.Assert(Stopwatch.IsHighResolution, "System does not support high-resolution performance counter");

            this.updateIntervalInMilliseconds = UpdateIntervalInMilliseconds;

            this.secondsPerCount = 0.0;
            this.DeltaTime = -1.0;
            this.baseTime = 0;
            this.previousTime = 0;
            this.currentTime = 0;

            long countsPerSec = Stopwatch.Frequency;
            secondsPerCount = 1.0 / countsPerSec;

            this.reset();

            this.timeKeeperTimer = new Timer(this.timeKeeperTimerCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(this.updateIntervalInMilliseconds));
        }

        #endregion Construction

        #region Methods

        #region reset
        private void reset()
        {
            long currentTime = Stopwatch.GetTimestamp();
            this.baseTime = currentTime;
            this.previousTime = currentTime;

            this.ElapsedTimeAbsolute = TimeSpan.FromTicks(currentTime - this.baseTime);
            this.ElapsedTimeRelative = TimeSpan.FromTicks(currentTime - this.baseTime);
        }
        #endregion reset

        #region timeKeeperTimerCallback
        private void timeKeeperTimerCallback(object state)
        {
            this.tick();
        }
        #endregion timeKeeperTimerCallback

        #region tick
        private void tick()
        {
            this.currentTime = Stopwatch.GetTimestamp();
            this.DeltaTime = (this.currentTime - this.previousTime) * this.secondsPerCount;

            this.previousTime = this.currentTime;
            if (this.DeltaTime < 0.0)
            {
                this.DeltaTime = 0.0;
            }

            this.ElapsedTimeAbsolute = TimeSpan.FromTicks(this.currentTime - this.baseTime);

            this.ElapsedTimeRelative += TimeSpan.FromSeconds(this.DeltaTime * this.CurrentMultiplicator);

            if (this.ElapsedTimeRelative < TimeSpan.Zero)
            {
                this.ElapsedTimeRelative = TimeSpan.Zero;  //reset to zero, when below zero
            }

            //TODO: check if target is not null
            //TODO: check if target has been reached, CurrentMultiplicator > 0 to determine direction
        }
        #endregion tick

        #region Play: puts the progress of ElapsedTimeRelative back to standard
        /// <summary>
        /// puts the progress of ElapsedTimeRelative back to standard
        /// </summary>
        public void Play()
        {
            this.CurrentMultiplicator = 1f;
        }
        #endregion Play

        #region moveTime
        private void moveTime(double Multiplier)
        {
            this.CurrentMultiplicator = Multiplier;
        }
        #endregion moveTime

        #region moveTimeTo
        private void moveTimeTo(double Multiplier, TimeSpan TargetPoint)
        {
            this.CurrentMultiplicator = Multiplier;
            this.CurrentTarget = TargetPoint;
        }
        #endregion moveTimeTo

        #region FastForward: Fast forwards ElapsedTimeRelative
        /// <summary>
        /// Fast forwards ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        public void FastForward(double Multiplier)
        {
            this.moveTime(Multiplier);
        }
        #endregion FastForward

        #region FastForward: Fast forwards ElapsedTimeRelative to a certain point in time since start
        /// <summary>
        /// Fast forwards ElapsedTimeRelative to a certain point in time since start
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        public void FastForward(double Multiplier, TimeSpan TargetPoint)
        {
            this.moveTimeTo(Multiplier, TargetPoint);
        }
        #endregion FastForward

        #region Rewind: Rewinds ElapsedTimeRelative
        /// <summary>
        /// Rewinds ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        public void Rewind(double Multiplier)
        {
            this.moveTime((-1) * Multiplier);
        }
        #endregion Rewind

        #region Rewind: Rewinds ElapsedTimeRelative to a certain point in time since start
        /// <summary>
        /// Rewinds ElapsedTimeRelative to a certain point in time since start
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        public void Rewind(double Multiplier, TimeSpan TargetPoint)
        {
            this.moveTimeTo((-1) * Multiplier, TargetPoint);
        }
        #endregion Rewind

        #region Pause: puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// <summary>
        /// puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// </summary>
        public void Pause()
        {
            this.CurrentMultiplicator = 0f;
        }
        #endregion Pause

        #region JumpTo: Jumps directly to a target time for ElapsedTimeRelative
        /// <summary>
        /// Jumps directly to a target time for ElapsedTimeRelative 
        /// </summary>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        public void JumpTo(TimeSpan TargetPoint)
        {
            this.ElapsedTimeRelative = TargetPoint;
            this.CurrentMultiplicator = 1.0f;
            this.CurrentTarget = null;
        }
        #endregion JumpTo

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods
    }
}
