using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Client.Classes
{
    public class RequestInfo : BaseObject
    {
        #region Properties

        public HttpResponseMessage LastResponse { get; set; } = null;  //for debugging / lookup
        public string LastContent { get; set; } = string.Empty;  //for debugging / lookup
        public object LastResult { get; set; } = null;  //for debugging / lookup

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.LastResponse = null;
            this.LastContent = null;
            this.LastResult = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
