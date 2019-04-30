using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.BaseClasses.Test
{
    /// <summary>
    /// base class for Unit tests
    /// </summary>
    /// <typeparam name="T">the type to be tested</typeparam>
    [TestClass]
    public class BaseSimpleTest
    {
        #region Properties

        protected Random random;

        #endregion Properties

        #region Methods

        #region Init
        [TestInitialize]
        public virtual void Init()
        {
            this.random = new Random();
        }
        #endregion Init

        #region Deinit
        [TestCleanup]
        public virtual void Deinit()
        {
        }
        #endregion Deinit

        #endregion Methods
    }
}
