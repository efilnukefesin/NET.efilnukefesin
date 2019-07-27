using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Server
{
    public class TypedPersistedController<T> : TypedBaseController<T> where T : IBaseObject
    {
        #region Properties

        private IDataService dataService;
        private string storeName;

        #endregion Properties

        #region Construction

        public TypedPersistedController(IDataService DataService, string StoreName)
        {
            this.dataService = DataService;
            this.storeName = StoreName;
        }

        #endregion Construction

        #region Methods

        #region addItems
        protected async override void addItems(IEnumerable<T> newItems)
        {
            var alreadyExistingItems = await this.dataService.GetAllAsync<T>(this.storeName);

            if (alreadyExistingItems == null)
            {
                await this.dataService.CreateOrUpdateAsync<T>(this.storeName, newItems);
                base.addItems(await this.dataService.GetAllAsync<T>(this.storeName));
            }
            else
            {
                this.items = new List<T>(alreadyExistingItems);
            }
        }
        #endregion addItems

        #region Delete
        public override ActionResult Delete(Guid Id)
        {
            var result = base.Delete(Id);
            this.dataService.DeleteAsync<T>(this.storeName, Id);
            return result;
        }
        #endregion Delete

        #region Put
        public override ActionResult Put(Guid Id, [FromBody] T updatedContent)
        {
            var result = base.Put(Id, updatedContent);
            this.dataService.CreateOrUpdateAsync<T>(this.storeName, updatedContent);
            return result;
        }
        #endregion Put

        #region Post
        public override ActionResult Post([FromBody] T newContent)
        {
            var result = base.Post(newContent);
            this.dataService.CreateOrUpdateAsync<T>(this.storeName, newContent);
            return result;
        }
        #endregion Post

        #endregion Methods

        #region Events

        #endregion Events
    }
}
