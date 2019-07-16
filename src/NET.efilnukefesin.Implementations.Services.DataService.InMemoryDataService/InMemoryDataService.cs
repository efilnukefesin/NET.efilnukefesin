using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Extensions;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Implementations.Services.DataService.InMemoryDataService
{
    public class InMemoryDataService : BaseObject, IDataService
    {
        #region Properties

        public IEndpointRegister EndpointRegister { get; set; }
        private ILogger logger;

        private Dictionary<string, List<IBaseObject>> items;

        #endregion Properties

        #region Construction

        public InMemoryDataService(IEndpointRegister EndpointRegister, ILogger Logger = null)
        {
            this.EndpointRegister = EndpointRegister;
            this.logger = Logger;
            this.items = new Dictionary<string, List<IBaseObject>>();
        }

        #endregion Construction

        #region Methods

        #region AddOrReplaceAuthentication
        public void AddOrReplaceAuthentication(string Authenticationstring)
        {
            //do nothing
        }
        #endregion AddOrReplaceAuthentication

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value) where T : IBaseObject
        {
            bool result = false;

            if (this.items.ContainsKey(this.EndpointRegister.GetEndpoint(Action)))
            {
                //create or update
                if (this.items[this.EndpointRegister.GetEndpoint(Action)].Any(x => x.Id.Equals(Value.Id)))
                {
                    //update
                    this.items[this.EndpointRegister.GetEndpoint(Action)].RemoveAll(x => x.Id.Equals(Value.Id));
                    this.items[this.EndpointRegister.GetEndpoint(Action)].Add(Value);
                    result = true;
                }
                else
                {
                    //create
                    this.items[this.EndpointRegister.GetEndpoint(Action)].Add(Value);
                    result = true;
                }
            }
            else
            {
                //add new
                this.items.Add(this.EndpointRegister.GetEndpoint(Action), new List<IBaseObject>());
                this.items[this.EndpointRegister.GetEndpoint(Action)].Add(Value);
                result = true;
            }

            return result;
        }
        #endregion CreateOrUpdateAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject
        {
            bool result = true;

            foreach (T value in Values)
            {
                result &= await this.CreateOrUpdateAsync<T>(this.EndpointRegister.GetEndpoint(Action), value);
            }

            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            bool result = false;

            if (this.items.ContainsKey(this.EndpointRegister.GetEndpoint(Action)))
            {
                string idToRemove = Parameters[0].ToString();
                if (this.items[this.EndpointRegister.GetEndpoint(Action)].Any(x => x.Id.Equals(idToRemove)))
                {
                    this.items[this.EndpointRegister.GetEndpoint(Action)].RemoveAll(x => x.Id.Equals(idToRemove));
                    result = true;
                }
            }

            return result;
        }
        #endregion DeleteAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            IEnumerable<T> result = default;

            if (this.items.ContainsKey(this.EndpointRegister.GetEndpoint(Action)))
            {
                result = new List<T>();
                foreach (IBaseObject src in this.items[this.EndpointRegister.GetEndpoint(Action)])
                {
                    result = result.Add((T)src);
                }
            }

            return result;
        }
        #endregion GetAllAsync

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            T result = default;

            if (this.items.ContainsKey(this.EndpointRegister.GetEndpoint(Action)))
            {
                result = (T)this.items[this.EndpointRegister.GetEndpoint(Action)].Where(x => x.Id.Equals(Parameters[0].ToString())).FirstOrDefault();
            }
            
            return result;
        }
        #endregion GetAsync

        #region dispose
        protected override void dispose()
        {
            this.items.Clear();
            this.items = null;
            this.logger = null;
            this.EndpointRegister = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
