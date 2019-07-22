using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Flurl;

namespace NET.efilnukefesin.Implementations.Services.DataService.RestDataService
{
    public class RestDataService : BaseObject, IDataService
    {
        #region Properties

        protected HttpClient httpClient;

        public IEndpointRegister EndpointRegister { get; private set; }

        private HttpResponseMessage lastResponse = null;  //for debugging / lookup
        private string lastContent = string.Empty;  //for debugging / lookup
        private object lastResult = null;  //for debugging / lookup


        //TODO: Use BaseTypedClient dict or create TypedClient by request?
        // https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/

        private Uri baseUri;
        private string authenticationString = string.Empty;
        private Dictionary<Type, BaseClient> clients = new Dictionary<Type, BaseClient>();
        private ILogger logger;
        private HttpMessageHandler overrideMessageHandler;

        #endregion Properties

        #region Construction

        public RestDataService(Uri BaseUri, IEndpointRegister EndpointRegister, ILogger Logger, string BearerToken = null, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.overrideMessageHandler = OverrideMessageHandler;
            this.logger = Logger;
            this.baseUri = BaseUri;
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

        //TODO: add method to add and or find an appropriate client
        #region getClient
        private TypedBaseClient<T> getClient<T>(string ResourceUri) where T: IBaseObject
        {
            TypedBaseClient<T> result = default;

            if (!this.clients.ContainsKey(typeof(T)))
            {
                //create new one
                var url = this.baseUri.ToString().AppendPathSegment(ResourceUri);
                TypedBaseClient<T> client = new TypedBaseClient<T>(new Uri(url), this.logger, this.overrideMessageHandler);
                this.clients.Add(typeof(T), client); //TODO: add Uri;  //TODO: make less specific
            }

            result = (TypedBaseClient<T>)this.clients[typeof(T)];

            return result;
        }
        #endregion getClient

        #region AddOrReplaceAuthentication
        public void AddOrReplaceAuthentication(string BearerToken)
        {
            this.addAuthenticationHeader(BearerToken);
            this.authenticationString = BearerToken;
        }
        #endregion AddOrReplaceAuthentication

        #region addAuthenticationHeader
        private void addAuthenticationHeader(string value, string type = "Bearer")
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion addAuthenticationHeader

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            T result = default;
            this.logger?.Log($"RestDataService.GetAsync(): entered");
            string parameters = this.convertParameters(Parameters);
            TypedBaseClient<T> client = this.getClient<T>(this.EndpointRegister.GetEndpoint(Action));
            result = await client.GetAsync(this.EndpointRegister.GetEndpoint(Action) + parameters);
            this.logger?.Log($"RestDataService.GetAsync(): exited with result '{result}'");
            return result;
        }
        #endregion GetAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            IEnumerable<T> result = default;
            this.logger?.Log($"RestDataService.GetAllAsync(): entered");
            string parameters = this.convertParameters(Parameters);
            TypedBaseClient<T> client = this.getClient<T>(this.EndpointRegister.GetEndpoint(Action));
            result = await client.GetAllAsync();
            this.logger?.Log($"RestDataService.GetAllAsync(): exited with result '{result}'");
            return result;
        }
        #endregion GetAllAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject
        {
            bool result = false;
            HttpResponseMessage response = await this.httpClient.PostAsync(this.EndpointRegister.GetEndpoint(Action), new StringContent(JsonConvert.SerializeObject(Value)));
            this.lastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                this.lastContent = json;
                SimpleResult<ValueObject<bool>> requestResult = JsonConvert.DeserializeObject<SimpleResult<ValueObject<bool>>>(json);
                this.lastResult = requestResult;
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload.Value;
                }
            }
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject
        {
            bool result = false;
            //TODO: optimize
            foreach (T item in Values)
            {
                result &= await this.CreateOrUpdateAsync<T>(Action, item);
            }
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            bool result = false;

            string parameters = this.convertParameters(Parameters);

            HttpResponseMessage response = await this.httpClient.DeleteAsync(this.EndpointRegister.GetEndpoint(Action) + parameters);
            this.lastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                this.lastContent = json;
                SimpleResult<bool> requestResult = JsonConvert.DeserializeObject<SimpleResult<bool>>(json);
                this.lastResult = requestResult;
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
            }
            return result;
        }
        #endregion DeleteAsync

        #region convertParameters: converts the given parameters to a string delimited with '/'
        /// <summary>
        /// converts the given parameters to a string delimited with '/'
        /// </summary>
        /// <param name="Parameters">an array of objects</param>
        /// <returns>the Uri compatible string</returns>
        private string convertParameters(object[] Parameters)
        {
            string parameters = string.Empty;
            if (Parameters.Count() > 0)
            {
                foreach (object parameter in Parameters)
                {
                    parameters += "/" + parameter.ToString();
                }
            }

            return parameters;
        }
        #endregion convertParameters

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            throw new NotImplementedException();
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            throw new NotImplementedException();
        }
        #endregion DeleteAsync

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
