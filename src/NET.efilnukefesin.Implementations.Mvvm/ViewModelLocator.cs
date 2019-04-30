using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm
{
    public class ViewModelLocator : IViewModelLocator
    {
        #region Properties

        private Dictionary<string, object> registeredInstances = new Dictionary<string, object>();

        #endregion Properties

        #region Construction

        public ViewModelLocator()
        {
            this.findAndAddViewModelInstances();
        }

        #endregion Construction

        #region Methods

        #region Register
        public void Register(string name, object o)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (!registeredInstances.ContainsKey(name))
            {
                this.registeredInstances.Add(name, o);
            }
            else
            {
                throw new Exception(String.Format("Instance with name '{0}' already registered", name));
            }
        }
        #endregion Register

        #region GetInstance
        public object GetInstance(string name)
        {
            if (registeredInstances.ContainsKey(name))
            {
                return registeredInstances[name];
            }
            return null;
        }
        #endregion GetInstance

        public object this[string name]
        {
            get
            {
                return GetInstance(name);
            }
        }

        #region findAndAddViewModelInstances
        private void findAndAddViewModelInstances()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly currentAssembly in assemblies)
            {
                foreach (Type currentType in currentAssembly.GetTypes())
                {
                    foreach (object customAttribute in currentType.GetCustomAttributes(true))
                    {
                        LocatorAttribute locatorAttribute = customAttribute as LocatorAttribute;
                        if (locatorAttribute != null)
                        {
                            if (!this.registeredInstances.ContainsKey(locatorAttribute.Name))
                            {
                                object instance = DiManager.GetInstance().Resolve(currentType);  //TODO: just add type, let resolving be done by using app
                                this.registeredInstances.Add(locatorAttribute.Name, instance);
                            }
                        }
                    }
                }
            }
        }
        #endregion findAndAddViewModelInstances

        #endregion Methods
    }
}
