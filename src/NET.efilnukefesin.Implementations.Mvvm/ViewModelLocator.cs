using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm
{
    public class ViewModelLocator : BaseObject, IViewModelLocator
    {
        #region Properties

        private Dictionary<string, object> registeredInstances = new Dictionary<string, object>();
        private ILogger logger;
        public bool IsBusy { get; private set; } = false;

        #endregion Properties

        #region Construction

        public ViewModelLocator()
        {
            this.logger = new SerilogLogger();  //TODO: replace by Di
            this.findAndAddViewModelInstances();
        }

        #endregion Construction

        #region Indexer
        public object this[string name]
        {
            get
            {
                return GetInstance(name);
            }
        }
        #endregion Indexer

        #region Methods

        #region Register
        public void Register(string name, object o)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("ViewModelLocator.Register: No name supplied");
            }

            if (!registeredInstances.ContainsKey(name))
            {
                this.registeredInstances.Add(name, o);
            }
            else
            {
                throw new Exception($"ViewModelLocator.Register: Instance with name '{name}' already registered");
            }
        }
        #endregion Register

        #region GetInstance
        public object GetInstance(string name)
        {
            object result = default;
            if (this.registeredInstances.ContainsKey(name))
            {
                result = this.registeredInstances[name];
            }
            
            return result;
        }
        #endregion GetInstance

        #region findAndAddViewModelInstances
        private void findAndAddViewModelInstances()
        {
            this.IsBusy = true;
            this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): entered");
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly currentAssembly in assemblies)
            {
                if (currentAssembly.FullName.Contains("System.Runtime"))
                {
                    this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): skipped Assembly '{currentAssembly.FullName}'");
                }
                else
                {
                    foreach (Type currentType in currentAssembly.GetTypes())
                    {
                        try
                        {
                            foreach (object customAttribute in currentType.GetCustomAttributes(true))
                            {
                                try
                                {
                                    LocatorAttribute locatorAttribute = customAttribute as LocatorAttribute;
                                    if (locatorAttribute != null)
                                    {
                                        if (!this.registeredInstances.ContainsKey(locatorAttribute.Name))
                                        {
                                            this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): adding new instance '{locatorAttribute.Name}'");
                                            object instance = DiManager.GetInstance().Resolve(currentType);  //TODO: just add type, let resolving be done by using app
                                            this.registeredInstances.Add(locatorAttribute.Name, instance);
                                        }
                                        else
                                        {
                                            this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): skipping '{locatorAttribute.Name}' as already registered");
                                        }
                                    }
                                
                                }
                                catch (Exception ex)
                                {
                                    this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): Exception caught with Attributes: '{ex.Message}'", Contracts.Logger.Enums.LogLevel.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): Exception caught with Types: '{ex.Message}'", Contracts.Logger.Enums.LogLevel.Warning);
                        }
                    }
                }
            }
            this.logger?.Log($"ViewModelLocator.findAndAddViewModelInstances(): exited");
            this.IsBusy = false;
        }
        #endregion findAndAddViewModelInstances

        #region Initialize
        public void Initialize()
        {
            this.logger?.Log($"ViewModelLocator.Initialize(): entered");

            foreach (KeyValuePair<string, object> kvp in this.registeredInstances)
            {
                var instance = kvp.Value;
                if (TypeHelper.ImplementsInterface(instance.GetType(), typeof(IViewModel)))
                {
                    this.logger?.Log($"ViewModelLocator.Initialize(): ViewModel '{kvp.Key}' implements IViewModel, and is '{instance.GetType()}'");
                    IViewModel convertedInstance = (IViewModel)instance;
                    this.logger?.Log($"ViewModelLocator.Initialize(): calling Initialize()");
                    convertedInstance.Initialize();
                    this.logger?.Log($"ViewModelLocator.Initialize(): ended calling Initialize()");
                }
                else
                {
                    this.logger?.Log($"ViewModelLocator.Initialize(): ViewModel '{kvp.Key}' does not implement IViewModel, but is '{instance.GetType()}'", Contracts.Logger.Enums.LogLevel.Warning);
                }
            }

            this.logger?.Log($"ViewModelLocator.Initialize(): exited");
        }
        #endregion Initialize

        #region dispose
        protected override void dispose()
        {
            this.registeredInstances.Clear();
            this.registeredInstances = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
