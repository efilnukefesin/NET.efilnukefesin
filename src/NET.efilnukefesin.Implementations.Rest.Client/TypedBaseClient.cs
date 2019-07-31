using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Rest.Client
{
    public class TypedBaseClient<T> : BaseClient where T : IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedBaseClient(Uri ResourceUri, ILogger Logger = null, HttpMessageHandler OverrideMessageHandler = null)
            : base(ResourceUri, Logger, OverrideMessageHandler)
        {

        }

        #endregion Construction

        #region Methods

        #region GetAsync
        public async Task<T> GetAsync(params object[] Parameters)
        {
            T result = default;
            
            string logParameters = ObjectHelper.ConvertArrayToString(Parameters);
            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAsync({logParameters}): entered");
            string uriParameters = this.convertParameters(Parameters);

            HttpResponseMessage response = await this.httpClient.GetAsync(uriParameters);
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                this.requestInfo.LastContent = json;
                SimpleResult<T> requestResult = JsonConvert.DeserializeObject<SimpleResult<T>>(json);
                this.requestInfo.LastResult = requestResult;
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
                else
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAsync({logParameters}): error in request result '{requestResult.Error.Id}: {requestResult.Error.Message}, {requestResult.Error.Ex}'");
                }
            }
            else
            {
                this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAsync({logParameters}): error in request response '{response.StatusCode}: {response.ReasonPhrase}'");
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAsync({logParameters}): exited with result '{result}'");
            return result;
        }
        #endregion GetAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> result = default;

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAllAsync(): entered");
            HttpResponseMessage response = await this.httpClient.GetAsync(string.Empty);
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                this.requestInfo.LastContent = json;
                SimpleResult<IEnumerable<T>> requestResult = JsonConvert.DeserializeObject<SimpleResult<IEnumerable<T>>>(json);
                this.requestInfo.LastResult = requestResult;
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
                else
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAllAsync(): error in request result '{requestResult.Error.Id}: {requestResult.Error.Message}, {requestResult.Error.Ex}'");
                }
            }
            else
            {
                this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAllAsync(): error in request response '{response.StatusCode}: {response.ReasonPhrase}'");
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.GetAllAsync(): exited with result '{result}'");
            return result;
        }
        #endregion GetAllAsync

        #region ExistsAsync
        public async Task<bool> ExistsAsync(params object[] Parameters)
        {
            bool result = false;

            string logParameters = ObjectHelper.ConvertArrayToString(Parameters);
            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.ExistsAsync({logParameters}): entered");
            string uriParameters = this.convertParameters(Parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, uriParameters);
            HttpResponseMessage response = await this.httpClient.SendAsync(request);
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.ExistsAsync({logParameters}): error in request response '{response.StatusCode}: {response.ReasonPhrase}'", Contracts.Logger.Enums.LogLevel.Error);
                }
                else
                {
                    result = false;  //not found - this is ok with head
                }
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.ExistsAsync({logParameters}): exited with result '{result}'");
            return result;
        }
        #endregion ExistsAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync(params object[] Parameters)
        {
            bool result = false;

            string logParameters = ObjectHelper.ConvertArrayToString(Parameters);
            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.DeleteAsync({logParameters}): entered");
            string uriParameters = this.convertParameters(Parameters);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uriParameters);
            HttpResponseMessage response = await this.httpClient.SendAsync(request);
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.DeleteAsync({logParameters}): error in request response '{response.StatusCode}: {response.ReasonPhrase}'", Contracts.Logger.Enums.LogLevel.Error);
                }
                else
                {
                    result = false;  //not found - this is ok with delete
                }
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.DeleteAsync({logParameters}): exited with result '{result}'");
            return result;
        }
        #endregion DeleteAsync

        #region CreateAsync
        public async Task<bool> CreateAsync(T Value)
        {
            bool result = false;

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.CreateAsync(): entered");

            HttpResponseMessage response = await this.httpClient.PostAsync(string.Empty, new StringContent(JsonConvert.SerializeObject(Value)));
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.CreateAsync(): error in request response '{response.StatusCode}: {response.ReasonPhrase}'", Contracts.Logger.Enums.LogLevel.Error);
                }
                else
                {
                    result = false;  //not found - this is ok with head
                }
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.CreateAsync(): exited with result '{result}'");
            return result;
        }
        #endregion CreateAsync

        #region UpdateAsync
        public async Task<bool> UpdateAsync(T Value, params object[] Parameters)
        {
            bool result = false;

            string logParameters = ObjectHelper.ConvertArrayToString(Parameters);
            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.UpdateAsync({logParameters}): entered");
            string uriParameters = this.convertParameters(Parameters);

            HttpResponseMessage response = await this.httpClient.PutAsync(uriParameters, new StringContent(JsonConvert.SerializeObject(Value)));
            this.requestInfo.LastResponse = response;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    this.logger?.Log($"TypedBaseClient<{typeof(T)}>.UpdateAsync({logParameters}): error in request response '{response.StatusCode}: {response.ReasonPhrase}'", Contracts.Logger.Enums.LogLevel.Error);
                }
                else
                {
                    result = false;  //not found - this is ok with head
                }
            }

            this.logger?.Log($"TypedBaseClient<{typeof(T)}>.UpdateAsync({logParameters}): exited with result '{result}'");
            return result;
        }
        #endregion UpdateAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync(T Value)
        {
            bool result = false;
            if (await this.ExistsAsync(Value.Id))
            {
                result = await this.UpdateAsync(Value, Value.Id);
            }
            else
            {
                result = await this.CreateAsync(Value);
            }
            return result;
        }
        #endregion CreateOrUpdateAsync

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

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
