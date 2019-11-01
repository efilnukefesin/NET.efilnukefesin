using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
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
        
        private static IViewModelLocator locator
        {
            get 
            {
                IViewModelLocator result = new ViewModelLocator();
                result.Initialize();
                return result;
            }
        }
        private static ILogger logger = new SerilogLogger();  //TODO: replace by Di

        #region Current
        public static IViewModelLocator Current
        {
            get
            {
                return StaticViewModelLocator.locator;
            }
        }
        #endregion Current

        #endregion Properties

        #region Methods

        #region Register
        public static void Register(IViewModelLocator locatorToRegister)
        {
            StaticViewModelLocator.logger.Log($"StaticViewModelLocator.Register(): entered with parameter type '{locatorToRegister.GetType()}'");
            if (locatorToRegister == null)
            {
                StaticViewModelLocator.logger.Log($"StaticViewModelLocator.Register(): locatorToRegister is null", Contracts.Logger.Enums.LogLevel.Error);
                throw new ArgumentNullException("locatorToRegister");
            }
            StaticViewModelLocator.locator = locatorToRegister;
            StaticViewModelLocator.logger.Log($"StaticViewModelLocator.Register(): exited");
        }
        #endregion Register

        #endregion Methods
    }
}
