using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm
{
    public class NavigationService : INavigationService
    {
        #region Properties

        private Dictionary<string, string> viewsAndViewModels;

        private INavigationPresenter navigationPresenter;

        #endregion Properties

        #region Construction

        public NavigationService(INavigationPresenter NavigationPresenter)
        {
            this.navigationPresenter = NavigationPresenter;
            this.viewsAndViewModels = new Dictionary<string, string>();
            this.findViewsAndViewModels();
        }

        #endregion Construction

        #region Methods

        #region findViewsAndViewModels
        private void findViewsAndViewModels()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly currentAssembly in assemblies)
            {
                foreach (Type currentType in currentAssembly.GetTypes())
                {
                    ViewModelAttribute viewModelAttribute = null;
                    ViewAttribute viewAttribute = null;
                    foreach (object customAttribute in currentType.GetCustomAttributes(true))
                    {
                        if (customAttribute is ViewModelAttribute)
                        {
                            viewModelAttribute = customAttribute as ViewModelAttribute;
                        }
                        else if (customAttribute is ViewAttribute)
                        {
                            viewAttribute = customAttribute as ViewAttribute;
                        }
                        if (viewModelAttribute != null && viewAttribute != null)
                        {
                            if (!this.viewsAndViewModels.ContainsValue(viewModelAttribute.ViewModelName))
                            {
                                //object instance = DiManager.GetInstance().Resolve(currentType);
                                //this.viewsAndViewModels.Add(viewModelAttribute.ViewModelName, instance);
                                //this.viewsAndViewModels.Add(currentType, this.viewModelLocator.GetInstance(viewModelAttribute.ViewModelName));
                                this.viewsAndViewModels.Add(viewAttribute.ViewUri, viewModelAttribute.ViewModelName);
                            }
                        }
                    }
                }
            }
        }
        #endregion findViewsAndViewModels

        #region Navigate
        public bool Navigate(string ViewModelName)
        {
            string viewName = this.viewsAndViewModels.Where(x => x.Value.Equals(ViewModelName)).FirstOrDefault().Key;
            return this.navigationPresenter.Present(viewName, StaticViewModelLocator.Current.GetInstance(ViewModelName));
        }
        #endregion Navigate

        #region CanNavigate
        public bool CanNavigate(string ViewModelName)
        {
            return this.viewsAndViewModels.Any(x => x.Value.Equals(ViewModelName));
        }
        #endregion CanNavigate

        #endregion Methods
    }
}
