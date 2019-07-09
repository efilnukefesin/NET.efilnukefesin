using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NET.efilnukefesin.Implementations.Mvvm.WPF
{
    public abstract class BaseWpfNavigationPresenter : BaseObject, INavigationPresenter
    {
        #region Properties

        private Frame presentationFrame;
        private object currentDataContext = null;

        private string bufferedViewUri = null;
        private object bufferedDataContext = null;

        private string packPrefix;
        private string typePrefix;

        private Window currentWindow = null;
        private Page currentPage = null;

        public bool IsPresenterRegistered { get; private set; } = false;

        private ILogger logger;

        #endregion Properties

        #region Construction

        public BaseWpfNavigationPresenter(string packPrefix, string typePrefix, ILogger logger = null) : base()
        {
            this.packPrefix = packPrefix ?? throw new ArgumentNullException(nameof(packPrefix));
            this.typePrefix = typePrefix ?? throw new ArgumentNullException(nameof(typePrefix));
            this.logger = logger;
            this.logger?.Log($"BaseWpfNavigationPresenter.ctor(): called with params packPrefix: '{packPrefix}' and typePrefix: '{typePrefix}'");
        }

        #endregion Construction

        #region Methods

        #region Back
        public void Back()
        {
            this.logger?.Log($"BaseWpfNavigationPresenter.Back(): Entered");
            //If Modal Window is shown, close; if page view is shown,  go one back, forward to Presenter Implementation.
            if (this.currentWindow != null)
            {
                this.logger?.Log($"BaseWpfNavigationPresenter.Back(): currentWindow is not null, attempting to close it.");
                this.currentWindow.Close();
            }
            else if (this.currentPage != null)
            {
                this.logger?.Log($"BaseWpfNavigationPresenter.Back(): currentPage is not null, currentWindow is, so attempting to go back here.");
                this.presentationFrame.GoBack();
            }
            this.logger?.Log($"BaseWpfNavigationPresenter.Back(): Exited");
        }
        #endregion Back

        #region Present
        public bool Present(string ViewUri, object DataContext)
        {
            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): Entered with params ViewUri: '{ViewUri}' and DataContext[type]: '{DataContext.GetType()}'");
            bool result = false;
            try
            {
                this.currentDataContext = DataContext;
                Uri viewPackUri = new Uri(this.packPrefix + ViewUri);
                this.logger?.Log($"BaseWpfNavigationPresenter.Present(): viewPackUri built: '{viewPackUri}'");
                if (ViewUri.EndsWith("Window.xaml"))
                {
                    this.logger?.Log($"BaseWpfNavigationPresenter.Present(): ViewUri ends with 'Window.xaml', hence we are trying to show a pop up window");
                    //show as modal window
                    string typeName = ViewUri.Replace(".xaml", "");
                    this.logger?.Log($"BaseWpfNavigationPresenter.Present(): typeName: '{typeName}', fully: '{this.typePrefix + typeName}'");
                    Type windowType = Type.GetType(this.typePrefix + typeName);
                    if (windowType != null)
                    {
                        this.logger?.Log($"BaseWpfNavigationPresenter.Present(): successfully determined window type: '{windowType}'");
                        var window = TypeHelper.CreateInstance(windowType);
                        if (window != null)
                        {
                            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): successfully created an instance of: '{windowType}'");
                            if (window is Window)
                            {
                                this.logger?.Log($"BaseWpfNavigationPresenter.Present(): '{windowType}' is a Window");
                                this.currentWindow = (window as Window);
                                this.currentPage = null;
                                (window as Window).Owner = Application.Current.MainWindow;
                                (window as Window).DataContext = this.currentDataContext;
                                (window as Window).ShowDialog();
                            }
                            else
                            {
                                this.logger?.Log($"BaseWpfNavigationPresenter.Present(): '{windowType}' instance is no Window.", Contracts.Logger.Enums.LogLevel.Error);
                            }
                        }
                        else
                        {
                            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): could not create an instance from windowType '{windowType}'", Contracts.Logger.Enums.LogLevel.Error);
                        }
                    }
                    else
                    {
                        this.logger?.Log($"BaseWpfNavigationPresenter.Present(): unsuccessfully determined window type first time", Contracts.Logger.Enums.LogLevel.Error);
                        //TODO: determine type from all loaded assemblies
                        windowType = AssemblyHelper.GetType(typeName);
                        if (windowType != null)
                        {
                            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): successfully determined window type second time: '{windowType.GetType()}'");
                            //TODO: do the same as above
                        }
                        else
                        {
                            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): unsuccessfully determined window type last time", Contracts.Logger.Enums.LogLevel.Error);
                        }
                    }
                }
                else
                {
                    this.logger?.Log($"BaseWpfNavigationPresenter.Present(): ViewUri does not end with 'Window.xaml', hence we are trying navigate in the frame");
                    if (this.presentationFrame != null)
                    {
                        this.logger?.Log($"BaseWpfNavigationPresenter.Present(): presentationFrame is not null, trying to navigate there");
                        this.presentationFrame.Navigate(viewPackUri);

                        if ((Page)this.presentationFrame.Content != null)
                        {
                            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): presentationFrame.Content is not null, trying to unset the DataContext");
                            this.currentWindow = null;
                            this.currentPage = (Page)this.presentationFrame.Content;
                            ((Page)this.presentationFrame.Content).DataContext = null;
                        }

                        this.logger?.Log($"BaseWpfNavigationPresenter.Present(): setting bufferedViewUri and bufferedDataContext to null");
                        this.bufferedViewUri = null;
                        this.bufferedDataContext = null;

                        result = true;
                    }
                    else
                    {
                        this.logger?.Log($"BaseWpfNavigationPresenter.Present(): presentationFrame is null, buffering ViewUri and DataContext");
                        this.bufferedViewUri = ViewUri;
                        this.bufferedDataContext = DataContext;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger?.Log($"BaseWpfNavigationPresenter.Present(): Exception thrown: '{ex.Message}'", Contracts.Logger.Enums.LogLevel.Error);
            }
            this.logger?.Log($"BaseWpfNavigationPresenter.Present(): exited with result: '{result}'");
            return result;
        }
        #endregion Present

        #region RegisterPresenter
        public void RegisterPresenter(object Presenter)
        {
            this.logger?.Log($"BaseWpfNavigationPresenter.RegisterPresenter(): Entered");
            if (Presenter is Frame)
            {
                this.logger?.Log($"BaseWpfNavigationPresenter.RegisterPresenter(): Presenter is a Frame, adding events");
                this.presentationFrame = (Frame)Presenter;
                this.presentationFrame.Navigated += this.presentationFrame_Navigated;
                if (this.bufferedViewUri != null && this.bufferedDataContext != null)
                {
                    this.logger?.Log($"BaseWpfNavigationPresenter.RegisterPresenter(): Buffered View Uri and DataContext are not null, Presenting that");
                    this.Present(this.bufferedViewUri, this.bufferedDataContext);
                }
                this.IsPresenterRegistered = true;
            }
            this.logger?.Log($"BaseWpfNavigationPresenter.RegisterPresenter(): Exited, IsPresenterRegistered: '{this.IsPresenterRegistered}'");
        }
        #endregion RegisterPresenter

        #region presentationFrame_Navigated: set the datacontext after navigating as the content is now different
        /// <summary>
        /// set the datacontext after navigating as the content is now different
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presentationFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.logger?.Log($"BaseWpfNavigationPresenter.presentationFrame_Navigated(): Entered");
            if ((Page)e.Content != null)
            {
                this.logger?.Log($"BaseWpfNavigationPresenter.presentationFrame_Navigated(): e is a Page and it's content is not null");
                this.currentWindow = null;
                this.currentPage = (Page)e.Content;
                this.logger?.Log($"BaseWpfNavigationPresenter.presentationFrame_Navigated(): setting the DataContext of the Content to the currentDataContext: '{this.currentDataContext.GetType()}'");
                ((Page)e.Content).DataContext = this.currentDataContext;
            }
            this.logger?.Log($"BaseWpfNavigationPresenter.presentationFrame_Navigated(): Exited");
        }
        #endregion presentationFrame_Navigated

        #region dispose
        protected override void dispose()
        {
            this.presentationFrame = null;
            this.currentDataContext = null;
            this.bufferedDataContext = null;
            this.bufferedViewUri = null;
        }
        #endregion dispose

        #endregion Methods    
    }
}
