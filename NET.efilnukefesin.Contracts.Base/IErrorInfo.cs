using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IErrorInfo : IId
    {
        #region Properties
        string Message { get; set; }
        int ErrorId { get; }
        Exception Ex { get; }
        bool HasException { get; }
        string StackTrace { get; }
        bool HasStackTrace { get; }
        bool IsFatal { get; set; }
        #endregion Properties
    }
}
