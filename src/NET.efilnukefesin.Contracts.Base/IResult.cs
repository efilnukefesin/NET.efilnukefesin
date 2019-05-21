using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IResult<T>
    {
        #region Properties

        T Payload { get; }
        bool IsError { get; set; }
        IErrorInfo Error { get; }

        #endregion Properties
    }
}
