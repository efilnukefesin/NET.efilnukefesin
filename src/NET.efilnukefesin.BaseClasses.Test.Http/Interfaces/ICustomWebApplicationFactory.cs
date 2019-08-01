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
        TestServer GetServer();
        HttpClient GetClient();
    }
}
