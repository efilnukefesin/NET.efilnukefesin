using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    [DataContract]
    /// <summary>
    /// class representing a result value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleResult<T> : BaseObject/*, IResult<T>*/
    {
        #region Properties

        [DataMember]
        public T Payload { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public ErrorInfo Error { get; set; }

        #endregion Properties

        #region Construction

        public SimpleResult(T Payload, bool IsError = false)
        {
            this.IsError = IsError;
            this.Payload = Payload;
        }

        public SimpleResult(ErrorInfo Error)
        {
            this.IsError = true;
            this.Error = Error;
        }

        public SimpleResult()
        {

        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Payload = default(T);
            this.Error = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
