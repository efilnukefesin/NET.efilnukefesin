﻿using NET.efilnukefesin.Contracts.Services.DataService;
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

            string parameters = this.convertParameters(Parameters);

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

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value)
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
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters)
        {
            bool result = false;

            string parameters = this.convertParameters(Parameters);

            HttpResponseMessage response = await this.httpClient.DeleteAsync(this.EndpointRegister.GetEndpoint(Action) + parameters);
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