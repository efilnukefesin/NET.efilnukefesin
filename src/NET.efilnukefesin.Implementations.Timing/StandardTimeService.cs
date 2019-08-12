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

        #endregion Properties

        #region Construction

        public StandardTimeService()
        {
            Debug.Assert(Stopwatch.IsHighResolution, "System does not support high-resolution performance counter");

            this.secondsPerCount = 0.0;
            this.DeltaTime = -1.0;
            this.baseTime = 0;
            this.previousTime = 0;
            this.currentTime = 0;

            long countsPerSec = Stopwatch.Frequency;
            secondsPerCount = 1.0 / countsPerSec;

            this.reset();

            this.timeKeeperTimer = new Timer(this.timeKeeperTimerCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(1));
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

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods
    }
}
