using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Server
{
    public class TypedBaseController<T> : BaseController where T : IBaseObject
    {
        #region Properties

        protected List<T> items;

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

        #region addItems
        protected void addItems(IEnumerable<T> newItems)
        {
            this.items.AddRange(newItems);
        }
        #endregion addItems

        #region GetAll
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult<SimpleResult<IEnumerable<T>>> GetAll()
        {
            SimpleResult<IEnumerable<T>> result = default;

            if (this.items.Count > 0)
            {
                result = new SimpleResult<IEnumerable<T>>(this.items);
                //result = new OkObjectResult(new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault()));
            }
            else
            {
                result = new SimpleResult<IEnumerable<T>>(new ErrorInfo(2, "No items", $"The list you are trying to get has no items."));
                //TODO: add notfound?
                //result = NotFound();
            }

            return result;
        }
        #endregion GetAll

        #region Get
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult<SimpleResult<T>> Get(string Id)  //TODO: replace by Guid
        {
            SimpleResult<T> result = default;

            if (this.items.Any(x => x.Id.Equals(Id)))
            {
                result = new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault());
                //result = new OkObjectResult(new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault()));
            }
            else
            {
                result = new SimpleResult<T>(new ErrorInfo(1, "Not found", $"The item you were looking for is not known, please add first an item with Id '{Id}'"));
                //TODO: add notfound?
                //result = NotFound();
            }

            return result;
        }
        #endregion Get

        #region Head
        [HttpHead("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult Head(string Id)  //TODO: replace by Guid
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

        #region Delete
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult Delete(string Id)  //TODO: replace by Guid
        {
            ActionResult result = default;

            if (this.items.Any(x => x.Id.Equals(Id)))
            {
                int numberOfRemovedItems = this.items.RemoveAll(x => x.Id.Equals(Id));
                if (numberOfRemovedItems > 0)
                {
                    result = Ok();
                }
            }
            else
            {
                result = NotFound();
            }

            return result;
        }
        #endregion Delete

        #region Put
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult Put(string Id, [FromBody]T updatedContent)  //TODO: replace by Guid
        {
            ActionResult result = default;

            if (this.items.Any(x => x.Id.Equals(Id)))
            {
                //TODO: replace content BUT NOT id!
                var unUpdatedItem = this.items.FirstOrDefault(x => x.Id.Equals(Id));
                var oldId = unUpdatedItem.Id;
                updatedContent.Id = oldId;
                this.items.RemoveAll(x => x.Id.Equals(Id));
                this.items.Add(updatedContent);
                result = AcceptedAtAction(nameof(this.Get), new { id = updatedContent.Id }, updatedContent);
            }
            else
            {
                result = NotFound();
            }

            return result;
        }
        #endregion Put

        #region Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual ActionResult Post([FromBody]T newContent)  //TODO: replace by Guid
        {
            ActionResult result = default;

            if (!this.items.Any(x => x.Id.Equals(newContent.Id)))
            {
                this.items.Add(newContent);
                result = CreatedAtAction(nameof(this.Get), new { id = newContent.Id }, newContent);
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }
        #endregion Post

        #endregion Methods
    }
}
