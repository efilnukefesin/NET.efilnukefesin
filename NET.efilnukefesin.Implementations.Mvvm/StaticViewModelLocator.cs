using NET.efilnukefesin.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm
{
    /// <summary>
    /// this class is used in cases the ViewModelLocator is needed to be static (e.g. XAML bindings)
    /// </summary>

    public static class StaticViewModelLocator
    {
        #region Properties
        private static IViewModelLocator locator = new ViewModelLocator();

        public static IViewModelLocator Current
        {
            get
            {
                return StaticViewModelLocator.locator;
            }
        }
        #endregion Properties

        #region Methods

        public static void Register(IViewModelLocator locatorToRegister)
        {
            if (locatorToRegister == null)
            {
                throw new ArgumentNullException("locatorToRegister");
            }
            StaticViewModelLocator.locator = locatorToRegister;
        }
        #endregion Methods
    }
}
