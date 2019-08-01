using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http.Interfaces
{
    public interface ICustomWebApplicationFactory
    {
        #region Properties

        #endregion Properties

        #region Methods

        TestServer GetServer();
        HttpClient GetClient();
        Type GetStartupType();

        #endregion Methods
    }
}
