using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Controllers
{
    public class SpecialValuesController : TypedBaseController<ValueObject<string>>
    {
        #region Properties

        #endregion Properties

        #region Construction

        public SpecialValuesController()
        {
            this.addItems(this.generateTestItems());
        }

        #endregion Construction

        #region Methods

        #region generateTestItems
        private List<ValueObject<string>> generateTestItems()
        {
            List<ValueObject<string>> result = new List<ValueObject<string>>();
            ValueObject<string> item1 = new ValueObject<string>("item1");
            ValueObject<string> item2 = new ValueObject<string>("item2");
            ValueObject<string> item3 = new ValueObject<string>("item3");
            result.Add(item1);
            result.Add(item2);
            result.Add(item3);
            return result;
        }
        #endregion generateTestItems

        //TODO: add special end point
        #region Get
        [HttpGet("{Id}/{Message}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SimpleResult<ValueObject<bool>>> GetSpecial(string Id, string Message)  //TODO: replace by Guid
        {
            SimpleResult<ValueObject<bool>> result = default;

            //if (this.items.Any(x => x.Id.Equals(Id)))
            //{
            //    result = new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault());
            //    //result = new OkObjectResult(new SimpleResult<T>(this.items.Where(x => x.Id.Equals(Id)).FirstOrDefault()));
            //}
            //else
            //{
            //    result = new SimpleResult<T>(new ErrorInfo(1, "Not found", $"The item you were looking for is not known, please add first an item with Id '{Id}'"));
            //    //TODO: add notfound?
            //    //result = NotFound();
            //}

            return result;
        }
        #endregion Get

        #endregion Methods
    }
}
