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

        public IEndpointRegister EndpointRegister { get; private set; }

        //TODO: Use BaseTypedClient dict or create TypedClient by request?
        // https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/

        private Uri baseUri;
        private string authenticationString = string.Empty;
        private Dictionary<Type, BaseClient> clients = new Dictionary<Type, BaseClient>();
        private ILogger logger;
        private HttpMessageHandler overrideMessageHandler;

        private object lockSync = new object();

        #endregion Properties

        #region Construction

        public RestDataService(Uri BaseUri, IEndpointRegister EndpointRegister, ILogger Logger = null, string BearerToken = null, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.logger = Logger;

            this.logger?.Log($"RestDataService.ctor: entered with Parameters OverrideMessageHandler: '{overrideMessageHandler}' | BaseUri: '{BaseUri}' | EndpointRegister: '{EndpointRegister}' | BearerToken: '{BearerToken}'");
            this.overrideMessageHandler = OverrideMessageHandler;
            this.baseUri = BaseUri;
            this.EndpointRegister = EndpointRegister;
            
            if (BearerToken != null)
            {
                this.AddOrReplaceAuthentication(BearerToken);
            }
            this.logger?.Log($"RestDataService.ctor: exited");
        }

        #endregion Construction

        #region Methods

        //TODO: add method to add and or find an appropriate client
        #region getClient
        private TypedBaseClient<T> getClient<T>(string ResourceUri) where T: IBaseObject
        {
            this.logger?.Log($"RestDataService.getClient(): entered");
            TypedBaseClient<T> result = default;

            lock (this.lockSync)
            {
                if (!this.clients.ContainsKey(typeof(T)))
                {
                    this.logger?.Log($"RestDataService.getClient(): client resource has to be created");
                    string url = string.Empty;
                    if (ResourceUri != null)
                    {
                        this.logger?.Log($"RestDataService.getClient(): resource Uri not empty: '{ResourceUri}'");
                        url = this.baseUri.ToString().AppendPathSegment(ResourceUri);
                    }
                    else
                    {
                        this.logger?.Log($"RestDataService.getClient(): resource Uri empty", Contracts.Logger.Enums.LogLevel.Warning);
                        url = this.baseUri.ToString();
                    }
                    this.logger?.Log($"RestDataService.getClient(): whole Uri is '{url}'");
                    try
                    {
                        this.logger?.Log($"RestDataService.getClient(): Attempting to create a client: 'new TypedBaseClient<T>(new Uri({url}), {this.logger}, {this.overrideMessageHandler});'");
                        TypedBaseClient<T> client = new TypedBaseClient<T>(new Uri(url), this.logger, this.overrideMessageHandler);
                        this.logger?.Log($"RestDataService.getClient(): client created successfully, adding authentication header");
                        client.AddAuthenticationHeader(this.authenticationString);
                        this.logger?.Log($"RestDataService.getClient(): authentication header added, next up: add to clients dict");
                        this.clients.Add(typeof(T), client); //TODO: add Uri;  //TODO: make less specific
                        this.logger?.Log($"RestDataService.getClient(): Client successfully created and added");
                    }
                    catch (Exception ex)
                    {
                        this.logger?.Log($"RestDataService.getClient(): Client creation failed with Exception: '{ex.Message}'", Contracts.Logger.Enums.LogLevel.Error);
                    }
                }
                else
                {
                    this.logger?.Log($"RestDataService.getClient(): client for Type '{typeof(T)}' is buffered.");
                }

                result = (TypedBaseClient<T>)this.clients[typeof(T)];
            }
            this.logger?.Log($"RestDataService.getClient(): exited with result '{result}'");
            return result;
        }
        #endregion getClient

        #region AddOrReplaceAuthentication
        public void AddOrReplaceAuthentication(string BearerToken)
        {
            this.authenticationString = BearerToken;
            lock (this.lockSync)
            {
                foreach (KeyValuePair<Type, BaseClient> kvp in this.clients)
                {
                    kvp.Value.AddAuthenticationHeader(this.authenticationString);
                }
            }
        }
        #endregion AddOrReplaceAuthentication

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            T result = default;
            this.logger?.Log($"RestDataService.GetAsync(): entered");
            string parameters = this.convertParameters(Parameters);
            TypedBaseClient<T> client = this.getClient<T>(this.EndpointRegister.GetEndpoint(Action));
            result = await client.GetAsync(this.EndpointRegister.GetEndpoint(Action).AppendPathSegment(parameters));
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

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            List<T> result = new List<T>();
            foreach (T value in await this.GetAllAsync<T>(Action))
            {
                if (FilterMethod(value))
                {
                    result.Add(value);
                }
            }
            return result;
        }
        #endregion GetAllAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject
        {
            bool result = false;
            this.logger?.Log($"RestDataService.CreateOrUpdateAsync(): entered");
            TypedBaseClient<T> client = this.getClient<T>(this.EndpointRegister.GetEndpoint(Action));
            result = await client.CreateOrUpdateAsync(Value);
            this.logger?.Log($"RestDataService.CreateOrUpdateAsync(): exited with result '{result}'");
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

            this.logger?.Log($"RestDataService.DeleteAsync(): entered");
            TypedBaseClient<T> client = this.getClient<T>(this.EndpointRegister.GetEndpoint(Action) + parameters);  //TODO: parameters here?!?
            result = await client.DeleteAsync(Parameters[0]);
            this.logger?.Log($"RestDataService.DeleteAsync(): exited with result '{result}'");
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
            bool result = false;

            //1. get all entries & 2. apply filter
            IEnumerable<T> allRelevantItems = await this.GetAllAsync<T>(Action, FilterMethod);

            //3 delete all where filter returns true
            if (allRelevantItems != null && allRelevantItems.Count() > 0)
            {
                foreach (T item in allRelevantItems)
                {
                    Value.Id = item.Id;  //TODO: check when not sleepy any more
                    var intermediateResult = await this.CreateOrUpdateAsync<T>(Action, Value);
                }
                result = true;
            }

            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            bool result = false;

            //1. get all entries & 2. apply filter
            IEnumerable<T> allRelevantItems = await this.GetAllAsync<T>(Action, FilterMethod);

            //3 delete all where filter returns true
            if (allRelevantItems != null && allRelevantItems.Count() > 0)
            {
                foreach (T item in allRelevantItems)
                {
                    var intermediateResult = await this.DeleteAsync<T>(Action, item.Id);
                }
                result = true;
            }

            return result;
        }
        #endregion DeleteAsync

        #region dispose
        protected override void dispose()
        {
            this.clients.Clear();
            this.clients = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
