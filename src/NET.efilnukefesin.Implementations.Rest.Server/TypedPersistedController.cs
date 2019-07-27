using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
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
            await this.dataService.CreateOrUpdateAsync<T>(this.storeName, newItems);
            base.addItems(await this.dataService.GetAllAsync<T>(this.storeName));
        }
        #endregion addItems

        public override ActionResult Delete(Guid Id)
        {
            //TODO: add persistance
            return base.Delete(Id);
        }

        public override ActionResult Put(Guid Id, [FromBody] T updatedContent)
        {
            //TODO: add persistance
            return base.Put(Id, updatedContent);
        }

        public override ActionResult Post([FromBody] T newContent)
        {
            //TODO: add persistance
            return base.Post(newContent);
        }

        #endregion Methods

        #region Events

        #endregion Events
    }
}
