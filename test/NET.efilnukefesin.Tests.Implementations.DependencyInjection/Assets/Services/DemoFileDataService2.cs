using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoFileDataService2 : BaseObject, IDataService
    {
        #region Properties

        public string BasePath { get; set; }
        public ILogger Logger { get; set; }

        #endregion Properties

        #region Construction

        public DemoFileDataService2(string basePath, ILogger logger)
        {
            BasePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
            this.Logger = logger;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Logger = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
