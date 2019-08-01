using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NET.efilnukefesin.BaseClasses.Test.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NET.efilnukefesin.BaseClasses.Test.Http.Classes
{
    public class CustomWebApplicationFactory<StartupType> : WebApplicationFactory<StartupType>, ICustomWebApplicationFactory where StartupType : class
    {
        #region Properties

        HttpTestConfiguration httpTestConfiguration;

        #endregion Properties

        #region Construction

        public CustomWebApplicationFactory(HttpTestConfiguration HttpTestConfiguration)
        {
            this.httpTestConfiguration = HttpTestConfiguration;
        }

        #endregion Construction

        #region Methods

        #region CreateServer
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            return base.CreateServer(builder);
        }
        #endregion CreateServer

        #region ConfigureWebHost
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (!string.IsNullOrEmpty(this.httpTestConfiguration.SolutionRelativePath) && !string.IsNullOrEmpty(this.httpTestConfiguration.ApplicationBasePath) && !string.IsNullOrEmpty(this.httpTestConfiguration.SolutionName))
            {
                builder.UseSolutionRelativeContentRoot(this.httpTestConfiguration.SolutionRelativePath, this.httpTestConfiguration.ApplicationBasePath, this.httpTestConfiguration.SolutionName);
            }
            else if (!string.IsNullOrEmpty(this.httpTestConfiguration.SolutionRelativePath) && !string.IsNullOrEmpty(this.httpTestConfiguration.SolutionName))
            {
                builder.UseSolutionRelativeContentRoot(this.httpTestConfiguration.SolutionRelativePath, this.httpTestConfiguration.SolutionName);
            }
            else if (!string.IsNullOrEmpty(this.httpTestConfiguration.SolutionRelativePath))
            {
                builder.UseSolutionRelativeContentRoot(this.httpTestConfiguration.SolutionRelativePath);
            }
            builder.UseEnvironment(this.httpTestConfiguration.Environment);
            base.ConfigureWebHost(builder);
        }
        #endregion ConfigureWebHost

        #region CreateWebHostBuilder
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<StartupType>();
        }
        #endregion CreateWebHostBuilder

        #region GetServer
        public TestServer GetServer()
        {
            return this.Server;
        }
        #endregion GetServer

        #region GetClient
        public HttpClient GetClient()
        {
            return this.CreateClient();
        }
        #endregion GetClient

        #region GetStartupType
        public Type GetStartupType()
        {
            return typeof(StartupType);
        }
        #endregion GetStartupType

        #endregion Methods
    }
}
