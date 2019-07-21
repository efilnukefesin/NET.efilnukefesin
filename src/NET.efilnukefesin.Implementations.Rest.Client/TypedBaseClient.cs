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
    public abstract class TypedBaseClient<T> : BaseClient where T : IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedBaseClient(Uri ResourceUri, ILogger Logger, HttpMessageHandler OverrideMessageHandler = null)
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

        //TODO: CreateOrUpdate = Exists, if yes Update, if not Create

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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
