using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.FileDataService
{
    public class FileDataService : BaseObject, IDataService
    {
        #region Properties
        public IEndpointRegister EndpointRegister { get; set; }

        private string baseFolder;

        #endregion Properties

        #region Construction

        public FileDataService(string BaseFolder, IEndpointRegister EndpointRegister)
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
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value)
        {
            throw new NotImplementedException();
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters)
        {
            throw new NotImplementedException();
        }
        #endregion DeleteAsync

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters)
        {
            T result = default;

            string filename = Path.Join(Directory.GetCurrentDirectory(), Path.Join(this.baseFolder, this.EndpointRegister.GetEndpoint(Action)));

            if (File.Exists(filename))
            {
                string text = File.ReadAllText(filename, Encoding.UTF8);
                result = JsonConvert.DeserializeObject<T>(text);
            }

            return result;
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
