using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.BaseClasses.Test.Http.Classes
{
    public class CustomWebApplicationFactory<StartupType> : WebApplicationFactory<StartupType> where StartupType : class
    {
        #region Properties

        private string solutionRelativePath;
        private string applicationBasePath;
        private string solutionName;
        private string environment;

        #endregion Properties

        #region Construction

        public CustomWebApplicationFactory(string solutionRelativePath = null, string applicationBasePath = null, string solutionName = null, string Environment = "Development")
        {
            this.solutionRelativePath = solutionRelativePath;
            this.applicationBasePath = applicationBasePath;
            this.solutionName = solutionName;
            this.environment = Environment;
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
            if (!string.IsNullOrEmpty(this.solutionRelativePath) && !string.IsNullOrEmpty(this.applicationBasePath) && !string.IsNullOrEmpty(this.solutionName))
            {
                builder.UseSolutionRelativeContentRoot(this.solutionRelativePath, this.applicationBasePath, this.solutionName);
            }
            else if (!string.IsNullOrEmpty(this.solutionRelativePath) && !string.IsNullOrEmpty(this.solutionName))
            {
                builder.UseSolutionRelativeContentRoot(this.solutionRelativePath, this.solutionName);
            }
            else if (!string.IsNullOrEmpty(this.solutionRelativePath))
            {
                builder.UseSolutionRelativeContentRoot(this.solutionRelativePath);
            }
            builder.UseEnvironment(this.environment);
            base.ConfigureWebHost(builder);
        }
        #endregion ConfigureWebHost

        #region CreateWebHostBuilder
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<StartupType>();
        }
        #endregion CreateWebHostBuilder

        #endregion Methods

    }
}
