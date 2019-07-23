using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Controllers
{
    public class ValuesController : TypedBaseController<ValueObject<string>>
    {
        public ValuesController()
        {
            List<ValueObject<string>> initialItems = new List<ValueObject<string>>() { new ValueObject<string>("Item1"), new ValueObject<string>("Item2"), new ValueObject<string>("Item3") };
            this.addItems(initialItems);
        }
    }
}
