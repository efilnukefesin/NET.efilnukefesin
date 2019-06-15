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

        void AddOrReplaceAuthentication(string Authenticationstring);

        Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject;
        Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject;
        Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject;
        Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject;

        #endregion Methods
    }
}
