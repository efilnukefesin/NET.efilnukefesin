using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using NET.efilnukefesin.Implementations.Mvvm.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm
{
    public class NavigationService : BaseObject, INavigationService
    {
        #region Properties

        private Dictionary<string, string> viewsAndViewModels;

        private INavigationPresenter navigationPresenter;
        private List<NavigationInfo> history;
        private ILogger logger;

        private string lastViewModel = string.Empty;

        #endregion Properties

        #region Construction

        public NavigationService(INavigationPresenter NavigationPresenter, ILogger Logger = null)
        {
            this.logger = Logger;
            this.navigationPresenter = NavigationPresenter;
            this.navigationPresenter.BackFinished += this.navigationPresenter_BackFinished;
            this.viewsAndViewModels = new Dictionary<string, string>();
            this.history = new List<NavigationInfo>();
            this.findViewsAndViewModels();
        }

        #endregion Construction

        #region Methods

        #region navigationPresenter_BackFinished
        private void navigationPresenter_BackFinished(object sender, EventArgs e)
        {
            this.logger?.Log($"NavigationService.navigationPresenter_BackFinished(): entered");
            this.lastViewModel = string.Empty;  //override -> 'invalidate'
            this.logger?.Log($"NavigationService.navigationPresenter_BackFinished(): exited");
        }
        #endregion navigationPresenter_BackFinished

        #region Back
        public void Back()
        {
            this.logger?.Log($"NavigationService.Back(): entered");
            this.navigationPresenter.Back();
            if (this.history.Count - 2 > 0)
            {
                string oldLastViewModel = this.lastViewModel;
                string newLastViewModel = this.history[this.history.Count - 2].ViewmodelName;
                newLastViewModel = string.Empty;  //override -> 'invalidate'
                this.lastViewModel = newLastViewModel;
                this.logger?.Log($"NavigationService.Back(): changed lastViewmodel from '{oldLastViewModel}' to '{newLastViewModel}'");
            }
            else
            {
                this.logger?.Log($"NavigationService.Back(): this.history.Count is '{this.history.Count}' too low");
            }
            this.logger?.Log($"NavigationService.Back(): exited"); 
        }
        #endregion Back

        #region findViewsAndViewModels
        private void findViewsAndViewModels()
        {
            this.logger.Log($"NavigationService.findViewsAndViewModels: entered");
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly currentAssembly in assemblies)
            {
                //this.logger.Log($"NavigationService.findViewsAndViewModels: iterating through Assemblies, currently: '{currentAssembly.FullName}'");
                try
                {
                    foreach (Type currentType in currentAssembly.GetTypes())
                    {
                        //this.logger.Log($"NavigationService.findViewsAndViewModels: iterating through Types in Assembly '{currentAssembly.FullName}', currently: '{currentType.FullName}'");
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
                                this.logger.Log($"NavigationService.findViewsAndViewModels: successfully found a ViewModelAttribute AND a ViewAttribute in  Assembly '{currentAssembly.FullName}', Type: '{currentType.FullName}'");
                                if (!this.viewsAndViewModels.ContainsValue(viewModelAttribute.ViewModelName))
                                {
                                    this.viewsAndViewModels.Add(viewAttribute.ViewUri, viewModelAttribute.ViewModelName);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Log($"NavigationService.findViewsAndViewModels: iterating through Types in Assembly '{currentAssembly.FullName}', Exception: '{ex}, {ex.Message}'", Contracts.Logger.Enums.LogLevel.Error);
                }
            }
            this.logger.Log($"NavigationService.findViewsAndViewModels: exited");
        }
        #endregion findViewsAndViewModels

        #region Navigate
        public bool Navigate(string ViewModelName)
        {
            this.logger.Log($"NavigationService.Navigate: entered for ViewModel '{ViewModelName}'");
            bool result = false;
            if (!this.lastViewModel.Equals(ViewModelName))
            {
                this.OnNavigationStarted(new EventArgs());
                string viewName = this.viewsAndViewModels.Where(x => x.Value.Equals(ViewModelName)).FirstOrDefault().Key;
                this.logger.Log($"NavigationService.Navigate: ViewModel '{ViewModelName}' belongs to View '{viewName}'");
                this.history.Add(new NavigationInfo(ViewModelName, viewName, result));
                this.logger.Log($"NavigationService.Navigate: setting this.lastViewModel from '{this.lastViewModel}' to '{ViewModelName}'");
                this.lastViewModel = ViewModelName;
                //TODO: add handling if Navigate is called before "RegisterPresenter"
                var viewModelInstance = StaticViewModelLocator.Current.GetInstance(ViewModelName);
                if (viewModelInstance != null)
                {
                    this.logger.Log($"NavigationService.Navigate: successfully got instance of ViewModel '{ViewModelName}': {viewModelInstance.GetType()}");
                }
                else
                {
                    this.logger.Log($"NavigationService.Navigate: could not get instance of ViewModel '{ViewModelName}'", Contracts.Logger.Enums.LogLevel.Warning);
                }
                if (this.navigationPresenter != null && viewModelInstance != null)
                {
                    this.logger.Log($"NavigationService.Navigate: this.navigationPresenter != null && viewModelInstance != null");
                    result = this.navigationPresenter.Present(viewName, viewModelInstance);  //waiting, if view is a window
                    if (result)
                    {
                        this.OnNavigationSuccessful(new EventArgs());
                    }
                    else
                    {
                        this.OnNavigationFailed(new EventArgs());
                    }
                }
                else
                {
                    this.logger.Log($"NavigationService.Navigate: NOT this.navigationPresenter != null && viewModelInstance != null");
                    if (this.navigationPresenter == null)
                    {
                        this.logger.Log($"NavigationService.Navigate: this.navigationPresenter == null", Contracts.Logger.Enums.LogLevel.Error);
                    }
                    if (viewModelInstance == null)
                    {
                        this.logger.Log($"NavigationService.Navigate: this.navigationPresenter == null", Contracts.Logger.Enums.LogLevel.Error);
                    }
                }
            }
            else
            {
                this.logger.Log($"NavigationService.Navigate: did nothing, already at ViewModel: '{ViewModelName}', this.lastViewModel: '{this.lastViewModel}'");
            }
            
            this.logger.Log($"NavigationService.Navigate: exited, result: '{result}'");
            return result;
        }
        #endregion Navigate

        #region CanNavigate
        public bool CanNavigate(string ViewModelName)
        {
            bool result = false;
            this.logger.Log($"NavigationService.CanNavigate: called for ViewModel '{ViewModelName}'");
            result = this.viewsAndViewModels.Any(x => x.Value.Equals(ViewModelName));
            this.logger.Log($"NavigationService.CanNavigate: exited with result '{result}' for ViewModel '{ViewModelName}'");
            return result;
        }
        #endregion CanNavigate

        #region dispose
        protected override void dispose()
        {
            this.viewsAndViewModels.Clear();
            this.viewsAndViewModels = null;
            this.navigationPresenter = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #region OnNavigationStarted
        protected virtual void OnNavigationStarted(EventArgs e)
        {
            this.NavigationStarted?.Invoke(this, e);
        }
        #endregion OnNavigationStarted

        #region OnNavigationSuccessful
        protected virtual void OnNavigationSuccessful(EventArgs e)
        {
            this.NavigationSuccessful?.Invoke(this, e);
        }
        #endregion OnNavigationSuccessful

        #region OnNavigationFailed
        protected virtual void OnNavigationFailed(EventArgs e)
        {
            this.NavigationFailed?.Invoke(this, e);
        }
        #endregion OnNavigationFailed

        public event EventHandler NavigationStarted;
        public event EventHandler NavigationSuccessful;
        public event EventHandler NavigationFailed;

        #endregion Events
    }
}
