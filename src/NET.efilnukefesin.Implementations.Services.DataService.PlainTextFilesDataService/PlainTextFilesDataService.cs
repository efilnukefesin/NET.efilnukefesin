using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.PlainTextFilesDataService
{
    public class PlainTextFilesDataService : BaseObject, IDataService
    {
        #region Properties
        public IEndpointRegister EndpointRegister { get; set; }

        private string baseFolder;

        #endregion Properties

        #region Construction

        public PlainTextFilesDataService(string BaseFolder, IEndpointRegister EndpointRegister)
        {
            this.EndpointRegister = EndpointRegister;
            this.baseFolder = BaseFolder;
        }

        #endregion Construction

        #region Methods

        #region AddOrReplaceAuthentication: does nothing in this case.
        /// <summary>
        /// does nothing in this case.
        /// </summary>
        /// <param name="Authenticationstring"></param>
        public void AddOrReplaceAuthentication(string Authenticationstring)
        {
            //do nothing.
        }
        #endregion AddOrReplaceAuthentication

        #region CreateOrUpdateAsync
        public Task<bool> CreateOrUpdateAsync<T>(string Action, T Value)
        {
            throw new NotImplementedException();
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public Task<bool> DeleteAsync<T>(string Action, params object[] Parameters)
        {
            throw new NotImplementedException();
        }
        #endregion DeleteAsync

        #region GetAsync
        public Task<T> GetAsync<T>(string Action, params object[] Parameters)
        {
            throw new NotImplementedException();
        }
        #endregion GetAsync

        #region dispose
        protected override void dispose()
        {
            this.EndpointRegister = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
