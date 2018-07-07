using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IResult<T>
    {
        #region Properties

        T Value { get; }
        bool IsError { get; }
        IErrorInfo Error { get; }

        #endregion Properties
    }
}
