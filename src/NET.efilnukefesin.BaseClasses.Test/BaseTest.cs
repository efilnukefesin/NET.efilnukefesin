using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NET.efilnukefesin.BaseClasses.Test
{
    /// <summary>
    /// base class for Unit tests
    /// </summary>
    /// <typeparam name="T">the type to be tested</typeparam>
    [TestClass]
    public class BaseTest<T>
    {
        #region Properties

        protected T target = default(T);
        protected Random random;

        #endregion Properties

        #region Methods

        #region Init
        [TestInitialize]
        public virtual void Init()
        {
            this.target = default(T);
            this.random = new Random();
        }
        #endregion Init

        #region Deinit
        [TestCleanup]
        public virtual void Deinit()
        {
            this.target = default(T);
        }
        #endregion Deinit

        #endregion Methods
    }
}
