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
            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: started with Action '{Action}' and Object of Type '{Value.GetType()}'");
            bool result = false;

            string mappedAction = this.EndpointRegister.GetEndpoint(Action);
            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: mappedAction is '{mappedAction}'");
                if (this.items.ContainsKey(mappedAction))
                {
                    this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: items are containing the key, so let's use the existing list");
                    //create or update
                    if (this.items[mappedAction].Any(x => x.Id.Equals(Value.Id)))
                    {
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: Id match successful, replacement started for '{Value.Id}'");
                        //update
                        this.items[mappedAction].RemoveAll(x => x.Id.Equals(Value.Id));
                        this.items[mappedAction].Add(Value);
                        result = true;
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: replacement finished");
                    }
                    else
                    {
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: Id match not successful, adding started for '{Value.Id}'");
                        //create
                        this.items[mappedAction].Add(Value);
                        result = true;
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: adding finished");
                    }
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: items are not containing the key, adding new list plus value");
                    //add new
                    this.items.Add(mappedAction, new List<IBaseObject>());
                    this.items[mappedAction].Add(Value);
                    result = true;
                }
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: ended with result '{result}'");
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, IEnumerable<T> Values) where T : IBaseObject
        {
            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (with enum): started with Action '{Action}' and Object of Type '{Values.GetType()}'");
            bool result = true;

            foreach (T value in Values)
            {
                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (with enum): calling CreateOrUpdateAsync with value '{value}'");
                result &= await this.CreateOrUpdateAsync<T>(Action, value);
            }

            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: ended with result '{result}'");
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"InMemoryDataService.DeleteAsync: started with Action '{Action}' and Parameter[0] '{Parameters[0]?.ToString()}'");
            bool result = false;

            string mappedAction = this.EndpointRegister.GetEndpoint(Action);
            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.DeleteAsync: mappedAction is '{mappedAction}'");
                if (this.items.ContainsKey(mappedAction))
                {
                    this.logger?.Log($"InMemoryDataService.DeleteAsync: items are containing the key, so let's start matching");
                    var idToRemove = Parameters[0];
                    if (this.items[mappedAction].Any(x => x.Id.Equals(idToRemove)))
                    {
                        this.logger?.Log($"InMemoryDataService.DeleteAsync: found item with Id '{idToRemove}'");
                        int deletedItems = this.items[mappedAction].RemoveAll(x => x.Id.Equals(idToRemove));
                        result = true;
                        this.logger?.Log($"InMemoryDataService.DeleteAsync: deleted item with Id '{idToRemove}', {deletedItems} time(s)");
                    }
                    else
                    {
                        this.logger?.Log($"InMemoryDataService.DeleteAsync: item with Id '{idToRemove}' not found, deleting nothing", Contracts.Logger.Enums.LogLevel.Warning);
                    }
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.DeleteAsync: items are not containing the key, no need to continue", Contracts.Logger.Enums.LogLevel.Warning);
                }
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.DeleteAsync: mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.DeleteAsync: ended with result '{result}'");
            return result;
        }
        #endregion DeleteAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"InMemoryDataService.GetAllAsync: started with Action '{Action}'");
            IEnumerable<T> result = default;

            string mappedAction = this.EndpointRegister.GetEndpoint(Action);

            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.GetAllAsync: mappedAction is '{mappedAction}'");
                if (this.items.ContainsKey(mappedAction))
                {
                    this.logger?.Log($"InMemoryDataService.GetAllAsync: items containing the mapped action");
                    result = new List<T>();
                    foreach (IBaseObject src in this.items[mappedAction])
                    {
                        this.logger?.Log($"InMemoryDataService.GetAllAsync: adding '{src}'");
                        result = result.Add((T)src);
                    }
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.GetAllAsync: items not containing the mapped action, returning null", Contracts.Logger.Enums.LogLevel.Warning);
                }
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.GetAllAsync: mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.GetAllAsync: ended with result '{result}'");
            return result;
        }
        #endregion GetAllAsync

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            IEnumerable<T> result = new List<T>();
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

        #region GetAsync
        public async Task<T> GetAsync<T>(string Action, params object[] Parameters) where T : IBaseObject
        {
            this.logger?.Log($"InMemoryDataService.GetAsync: started with Action '{Action}' and Parameter[0] '{Parameters[0]?.ToString()}'");
            T result = default;

            string mappedAction = this.EndpointRegister.GetEndpoint(Action);

            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.GetAsync: mappedAction is '{mappedAction}'");
                if (this.items.ContainsKey(this.EndpointRegister.GetEndpoint(Action)))
                {
                    this.logger?.Log($"InMemoryDataService.GetAllAsync: items containing the mapped action");
                    result = (T)this.items[this.EndpointRegister.GetEndpoint(Action)].Where(x => x.Id.Equals(Parameters[0])).FirstOrDefault();
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.GetAllAsync: items not containing the mapped action", Contracts.Logger.Enums.LogLevel.Warning);
                }
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.GetAsync: mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.GetAsync: ended with result '{result}'");
            return result;
        }
        #endregion GetAsync

        #region CreateOrUpdateAsync
        public async Task<bool> CreateOrUpdateAsync<T>(string Action, T Value, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): started with Action '{Action}' and Value '{Value}'");
            bool result = false;
            string mappedAction = this.EndpointRegister.GetEndpoint(Action);

            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): mappedAction is '{mappedAction}'");
                List<Guid> IdsToRemove = new List<Guid>();
                foreach (IBaseObject item in this.items[mappedAction])
                {
                    if (FilterMethod.Invoke((T)item))
                    {
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): found a match with Id '{item.Id}'");
                        IdsToRemove.Add(item.Id);
                    }
                }

                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): IdsToRemove has {IdsToRemove.Count} entries");
                if (IdsToRemove.Count > 0)
                {
                    this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): starting removal");
                    foreach (Guid Id in IdsToRemove)
                    {
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): removing '{Id}'");
                        this.items[mappedAction].RemoveAll(x => x.Id.Equals(Id));
                    }
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): nothing to remove");
                }

                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): adding new Value");
                this.items[mappedAction].Add(Value);
                result = true;
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync: ended with result '{result}'");
            return result;
        }
        #endregion CreateOrUpdateAsync

        #region DeleteAsync
        public async Task<bool> DeleteAsync<T>(string Action, Func<T, bool> FilterMethod) where T : IBaseObject
        {
            bool result = false;
            this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): started with Action '{Action}'");

            string mappedAction = this.EndpointRegister.GetEndpoint(Action);
            if (mappedAction != null)
            {
                this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): mappedAction is '{mappedAction}'");
                if (this.items.ContainsKey(mappedAction))
                {
                    //TODO: double check doubled code with above method
                    this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): items containing key, so looking for it is valuable");
                    List<Guid> IdsToRemove = new List<Guid>();
                    foreach (IBaseObject item in this.items[mappedAction])
                    {
                        if (FilterMethod.Invoke((T)item))
                        {
                            this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): found a match with Id '{item.Id}'");
                            IdsToRemove.Add(item.Id);
                        }
                    }

                    this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): IdsToRemove has {IdsToRemove.Count} entries");
                    if (IdsToRemove.Count > 0)
                    {
                        this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): starting removal");
                        foreach (Guid Id in IdsToRemove)
                        {
                            this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): removing '{Id}'");
                            this.items[mappedAction].RemoveAll(x => x.Id.Equals(Id));
                        }
                        result = true;
                    }
                    else
                    {
                        this.logger?.Log($"InMemoryDataService.CreateOrUpdateAsync (delegate): nothing to remove");
                    }
                }
                else
                {
                    this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): items are not containing the mapped action, so no need to search", Contracts.Logger.Enums.LogLevel.Warning);
                }
            }
            else
            {
                this.logger?.Log($"InMemoryDataService.DeleteAsync (delegate): mappedAction is null, doing nothing", Contracts.Logger.Enums.LogLevel.Error);
            }

            this.logger?.Log($"InMemoryDataService.DeleteAsync: ended with result '{result}'");
            return result;
        }
        #endregion DeleteAsync

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
