using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.InMemoryDataService
{
    public class InMemoryDataService : BaseObject, IDataService
    {
        #region Properties

        public IEndpointRegister EndpointRegister { get; set; }
        private ILogger logger;

        #endregion Properties

        #region Construction

        public InMemoryDataService(IEndpointRegister EndpointRegister, ILogger Logger = null)
        {
            this.EndpointRegister = EndpointRegister;
            this.logger = Logger;
        }

        #endregion Construction

        #region Methods

        public void AddOrReplaceAuthentication(string Authenticationstring)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            throw new NotImplementedException();
        }

        protected override void dispose()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
