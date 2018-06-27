using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public abstract class BaseDisposable : IDisposable
    {
        #region Properties

        public virtual bool IsDisposing { get; private set; }

        #endregion Properties

        #region Construction

        public BaseDisposable()
        {
            this.IsDisposing = false;
        }

        #endregion Construction

        #region Methods

        #region Dispose
        public void Dispose()
        {
            this.IsDisposing = true;
            this.dispose();
            GC.SuppressFinalize(this);
        }
        #endregion Dispose

        #region dispose: Method to be overwritten by using classes. Put your Dispose code here.
        /// <summary>
        /// Method to be overwritten by using classes. Put your Dispose code here.
        /// </summary>
        /// <param name="isDisposing"></param>
        protected abstract void dispose();
        #endregion dispose

        #endregion Methods
    }
}
