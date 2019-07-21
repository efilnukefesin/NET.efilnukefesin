using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TypedBaseController(IEnumerable<T> initialItems)
            : base()
        {
            this.items = new List<T>(initialItems);
        }

        #endregion Construction

        #region Methods

        #region GetAll
        [HttpGet]
        public ActionResult<SimpleResult<IEnumerable<T>>> GetAll()
        {
            SimpleResult<IEnumerable<T>> result = default;

            if (this.items.Count > 0)
            {
                result = new SimpleResult<IEnumerable<T>>(this.items);
            }
            else
            {
                result = new SimpleResult<IEnumerable<T>>(new ErrorInfo(2, "No items", $"The list you are trying to get has no items."));
            }

            return result;
        }
        #endregion GetAll

        #region Get
        [HttpGet("{Id}")]
        public ActionResult<SimpleResult<T>> Get(string Id)  //TODO: replace by Guid
        {
            SimpleResult<T> result = default;

            if (this.items.Any(x => x.Id.Equals(Id)))
            {
                result = new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault());
            }
            else
            {
                result = new SimpleResult<T>(new ErrorInfo(1, "Not found", $"The item you were looking for is not known, please add first an item with Id '{Id}'"));
            }

            return result;
        }
        #endregion Get

        #region Head
        [HttpHead("{Id}")]
        public ActionResult Head(string Id)  //TODO: replace by Guid
        {
            ActionResult result = default;

            if (this.items.Any(x => x.Id.Equals(Id)))
            {
                result = Ok();
            }
            else
            {
                result = NotFound();
            }

            return result;
        }
        #endregion Head

        // Controller tests: https://docs.microsoft.com/de-de/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api

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
