using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Contracts.Services.DataService
{
    public interface IDataService : IBaseObject
    {
        #region Properties

        IEndpointRegister EndpointRegister { get; }

        #endregion Properties

        #region Methods

        void AddOrReplaceAuthentication(string BearerToken);

        Task<T> GetAsync<T>(string Action, params object[] Parameters);
        Task<bool> PostAsync<T>(string Action, T Value);

        //TODO: implement CRUD methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
