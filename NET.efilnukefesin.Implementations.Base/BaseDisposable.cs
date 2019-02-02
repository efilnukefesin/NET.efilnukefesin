using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Implementations.Base
{
    [DataContract]
    public abstract class BaseDisposable : IDisposable
    {
        #region Properties

        [IgnoreDataMember]
        [XmlIgnore]
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
