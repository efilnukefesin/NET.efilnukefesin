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
            this.ElapsedTimeRelative = TimeSpan.FromTicks(this.currentTime - this.baseTime);
        }
        #endregion tick

        #region Play: puts the progress of ElapsedTimeRelative back to standard
        /// <summary>
        /// puts the progress of ElapsedTimeRelative back to standard
        /// </summary>
        public void Play()
        {
            throw new NotImplementedException();
        }
        #endregion Play

        #region FastForward: Fast forwards ElapsedTimeRelative
        /// <summary>
        /// Fast forwards ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        public void FastForward(double Multiplier)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion FastForward

        #region Rewind: Rewinds ElapsedTimeRelative
        /// <summary>
        /// Rewinds ElapsedTimeRelative
        /// </summary>
        /// <param name="Multiplier">The Multiplier to be applied, e.g. 0.5 for half speed or slo-mo</param>
        public void Rewind(double Multiplier)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion Rewind

        #region Pause: puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// <summary>
        /// puts the progress of ElapsedTimeRelative on 'paused', so no progress at all
        /// </summary>
        public void Pause()
        {
            throw new NotImplementedException();
        }
        #endregion Pause

        #region JumpTo: Jumps directly to a target time for ElapsedTimeRelative
        /// <summary>
        /// Jumps directly to a target time for ElapsedTimeRelative 
        /// </summary>
        /// <param name="TargetPoint">the target time in terms of Elapsed Time since start</param>
        public void JumpTo(TimeSpan TargetPoint)
        {
            throw new NotImplementedException();
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
