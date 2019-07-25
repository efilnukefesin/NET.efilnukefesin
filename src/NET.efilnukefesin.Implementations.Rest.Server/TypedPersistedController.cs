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

        #endregion Properties

        #region Construction

        public TypedPersistedController(IDataService DataService)
        {
            this.dataService = DataService;
        }

        #endregion Construction

        #region Methods

        public override ActionResult Delete(string Id)
        {
            //TODO: add persistance
            return base.Delete(Id);
        }

        public override ActionResult Put(string Id, [FromBody] T updatedContent)
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
