using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Services.DataService
{
    public interface IEndpointRegister : IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        bool AddEndpoint(string Action, string Endpoint);
        string GetEndpoint(string Action);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
