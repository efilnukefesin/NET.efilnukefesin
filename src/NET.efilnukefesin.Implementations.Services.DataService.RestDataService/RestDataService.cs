using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.RestDataService
{
    public class RestDataService : BaseObject, IDataService
    {
        #region Properties

        protected HttpClient httpClient;

        public IEndpointRegister EndpointRegister { get; private set; }

        #endregion Properties

        #region Construction

        public RestDataService(Uri BaseUri, IEndpointRegister EndpointRegister, string BearerToken = null, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.EndpointRegister = EndpointRegister;
            if (OverrideMessageHandler != null)
            {
                this.httpClient = new HttpClient(OverrideMessageHandler);
            }
            else
            {
                this.httpClient = new HttpClient();
            }
            this.httpClient.BaseAddress = BaseUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (BearerToken != null)
            {
                this.addAuthenticationHeader(BearerToken);
            }
        }

        #endregion Construction

        #region Methods

        #region AddOrReplaceAuthentication
        public void AddOrReplaceAuthentication(string BearerToken)
        {
            this.addAuthenticationHeader(BearerToken);
        }
        #endregion AddOrReplaceAuthentication

        #region addAuthenticationHeader
        private void addAuthenticationHeader(string value, string type = "Bearer")
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion addAuthenticationHeader

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters)
        {
            T result = default(T);

            string parameters = string.Empty;
            if (Parameters.Count() > 0)
            {
                foreach (object parameter in Parameters)
                {
                    parameters += "/" + parameter.ToString();
                }
            }

            HttpResponseMessage response = await this.httpClient.GetAsync(this.EndpointRegister.GetEndpoint(Action) + parameters);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<T> requestResult = JsonConvert.DeserializeObject<SimpleResult<T>>(json);
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
            }
            return result;
        }
        #endregion GetAsync

        #region PostAsync
        public async Task<bool> PostAsync<T>(string Action, T Value)
        {
            bool result = false;
            HttpResponseMessage response = await this.httpClient.PostAsync(this.EndpointRegister.GetEndpoint(Action), new StringContent(JsonConvert.SerializeObject(Value)));
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<bool> requestResult = JsonConvert.DeserializeObject<SimpleResult<bool>>(json);
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
            }
            return result;
        }
        #endregion PostAsync

        #region dispose
        protected override void dispose()
        {
            this.httpClient = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
