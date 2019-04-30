using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class ErrorInfo : BaseObject, IErrorInfo
    {
        #region Properties
        public string Message { get; set; }
        public int ErrorId { get; set; }
        public Exception Ex { get; set; }

        #region HasException
        public bool HasException
        {
            get
            {
                return this.Ex != null;
            }
        }
        #endregion HasException

        #region StackTrace
        public string StackTrace
        {
            get
            {
                return this.Ex?.StackTrace;
            }
        }
        #endregion StackTrace

        #region HasStackTrace
        public bool HasStackTrace
        {
            get
            {
                return !string.IsNullOrEmpty(this.StackTrace);
            }
        }
        #endregion HasStackTrace

        public bool IsFatal { get; set; }
        #endregion Properties

        #region Construction

        public ErrorInfo(int ErrorId, string Message, Exception Ex, bool IsFatal = false)
        {
            this.ErrorId = ErrorId;
            this.Message = Message;
            this.Ex = Ex;
            this.IsFatal = IsFatal;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Ex = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
