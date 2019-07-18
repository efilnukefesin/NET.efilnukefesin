using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Server
{
    public abstract class TypedBaseController<T> : BaseController where T : IBaseObject
    {
        #region Properties

        private List<T> items;

        #endregion Properties

        #region Construction

        public TypedBaseController()
            : base()
        {
            this.items = new List<T>();
        }

        #endregion Construction

        #region Methods

        //TODO: implement CRUD methods
        //GET -> return list -> SDK.GetAllAsync
        //GET/Id -> return item -> SDK.GetAsync
        // DELETE/Id -> delete item -> SDK.DeleteAsync
        // HEAD/Id -> does items exist (200) or not (400) -> SDK.existsAsync
        // POST -> create item -> SDK.createAsync
        // PUT/Id -> update item -> SDK.updateAsync
        // merge put and post and head in  -> SDK.CreateOrUpdateAsync
        // https://stackoverflow.com/questions/9265286/including-id-in-uri-for-put-requests
        // https://www.restapitutorial.com/lessons/httpmethods.html
        //TODO: make BaseSDK

        #endregion Methods
    }
}
