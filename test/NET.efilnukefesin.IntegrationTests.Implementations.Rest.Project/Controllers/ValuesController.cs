﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Controllers
{
    public class ValuesController : TypedPersistedController<ValueObject<string>>
    {
        #region Properties

        #endregion Properties

        #region Construction

        public ValuesController(IDataService dataService)
            : base(dataService, "Values")
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

        #endregion Methods
    }
}
