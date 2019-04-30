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

        public bool IsValid { get; private set; }

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

        public Result(bool IsValid, T Value = default(T))
        {
            this.Value = Value;
            this.Error = null;
            this.IsValid = IsValid;
        }

        public Result(T Value)
        {
            this.Value = Value;
            this.Error = null;
            this.IsValid = true;
        }

        public Result(IErrorInfo Error)
        {
            this.Error = Error;
            this.Value = default(T);
            this.IsValid = false;
        }

        #endregion Construction

        #region Methods

        #endregion Methods
    }
}
