using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class Result<T> : IResult<T>
    {
        #region Properties

        public T Value { get; }

        #region IsError
        public bool IsError
        {
            get
            {
                return this.Error == null;
            }
        }
        #endregion IsError

        public IErrorInfo Error { get; }

        #endregion Properties

        #region Construction

        public Result(T Value)
        {
            this.Value = Value;
            this.Error = null;
        }

        public Result(IErrorInfo Error)
        {
            this.Error = Error;
            this.Value = default(T);
        }

        #endregion Construction

        #region Methods

        #endregion Methods

    }
}
