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

        public BaseWpfNavigationPresenter(string packPrefix, string typePrefix, ILogger logger) : base()
        {
            this.packPrefix = packPrefix ?? throw new ArgumentNullException(nameof(packPrefix));
            this.typePrefix = typePrefix ?? throw new ArgumentNullException(nameof(typePrefix));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion Construction

        #region Methods

        #region Back
        public void Back()
        {
            //If Modal Window is shown, close; if page view is shown,  go one back, forward to Presenter Implementation.
            if (this.currentWindow != null)
            {
                this.currentWindow.Close();
            }
            else if (this.currentPage != null)
            {
                this.presentationFrame.GoBack();
            }
        }
        #endregion Back

        #region Present
        public bool Present(string ViewUri, object DataContext)
        {
            bool result = false;
            try
            {
                this.currentDataContext = DataContext;
                Uri viewPackUri = new Uri(this.packPrefix + ViewUri);
                if (ViewUri.EndsWith("Window.xaml"))
                {
                    //show as modal window
                    string typeName = ViewUri.Replace(".xaml", "");
                    Type windowType = Type.GetType(this.typePrefix + typeName);
                    var window = TypeHelper.CreateInstance(windowType);
                    if (window is Window)
                    {
                        this.currentWindow = (window as Window);
                        this.currentPage = null;
                        (window as Window).Owner = Application.Current.MainWindow;
                        (window as Window).DataContext = this.currentDataContext;
                        (window as Window).ShowDialog();
                    }
                }
                else
                {
                    if (this.presentationFrame != null)
                    {
                        this.presentationFrame.Navigate(viewPackUri);

                        if ((Page)this.presentationFrame.Content != null)
                        {
                            this.currentWindow = null;
                            this.currentPage = (Page)this.presentationFrame.Content;
                            ((Page)this.presentationFrame.Content).DataContext = null;
                        }

                        this.bufferedViewUri = null;
                        this.bufferedDataContext = null;

                        result = true;
                    }
                    else
                    {
                        this.bufferedViewUri = ViewUri;
                        this.bufferedDataContext = DataContext;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion Present

        #region RegisterPresenter
        public void RegisterPresenter(object Presenter)
        {
            if (Presenter is Frame)
            {
                this.presentationFrame = (Frame)Presenter;
                this.presentationFrame.Navigated += this.presentationFrame_Navigated;
                if (this.bufferedViewUri != null && this.bufferedDataContext != null)
                {
                    this.Present(this.bufferedViewUri, this.bufferedDataContext);
                }
                this.IsPresenterRegistered = true;
            }
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
            if ((Page)e.Content != null)
            {
                this.currentWindow = null;
                this.currentPage = (Page)e.Content;
                ((Page)e.Content).DataContext = this.currentDataContext;
            }
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
