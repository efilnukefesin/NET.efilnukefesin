﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest.Classes
{
    internal class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup>
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            return base.CreateServer(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup>();
        }
    }
}