using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET.efilnukefesin.Extensions.Wpf.UserControls
{
    /// <summary>
    /// Interaktionslogik für PopupPageControl.xaml
    /// </summary>
    public partial class PopupPageControl : UserControl
    {
        #region Construction
        public PopupPageControl()
        {
            InitializeComponent();
        }
        #endregion Construction

        #region Events

        #region fContent_Navigated
        private void fContent_Navigated(object sender, NavigationEventArgs e)
        {
            //fContent.RemoveBackEntry();  // don't do any history - mem leak
        }
        #endregion fContent_Navigated

        #endregion Events

        #region Methods

        #region UpdateUI
        private void UpdateUI()
        {
            this.fContent.Source = this.FrameSource;
            this.polyMarker.Margin = new Thickness(0, 0, this.RightMarkerMargin, 0);
        }
        #endregion UpdateUI

        #endregion Methods

        #region DependencyProperties

        #region BlurVisual Property
        public static readonly DependencyProperty BlurVisualProperty = DependencyProperty.Register("BlurVisual", typeof(Visual), typeof(PopupPageControl), new PropertyMetadata(null, BlurVisual_ValueChanged));

        static void BlurVisual_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PopupPageControl self = obj as PopupPageControl;
            if (self.BlurVisualChanged != null) self.BlurVisualChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The visual for blurring"), Category("Own Properties"), DisplayName("BlurVisual")]
        public Visual BlurVisual
        {
            get { return (Visual)GetValue(BlurVisualProperty); }
            set { SetValue(BlurVisualProperty, value); }
        }

        public event EventHandler BlurVisualChanged;
        #endregion BlurVisual Property

        #region BlurRadius Property
        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register("BlurRadius", typeof(double), typeof(PopupPageControl), new PropertyMetadata(0.0, BlurRadius_ValueChanged));

        static void BlurRadius_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PopupPageControl self = obj as PopupPageControl;
            if (self.BlurRadiusChanged != null) self.BlurRadiusChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The radius of the blur effect"), Category("Own Properties"), DisplayName("BlurRadius")]
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public event EventHandler BlurRadiusChanged;
        #endregion BlurRadius Property

        #region FrameSource Property
        public static readonly DependencyProperty FrameSourceProperty = DependencyProperty.Register("FrameSource", typeof(Uri), typeof(PopupPageControl), new PropertyMetadata(default(Uri), FrameSource_ValueChanged));

        static void FrameSource_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PopupPageControl self = obj as PopupPageControl;
            if (self.FrameSourceChanged != null) self.FrameSourceChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The Uri or Page to be shown in the Popup"), Category("Own Properties"), DisplayName("FrameSource")]
        public Uri FrameSource
        {
            get { return (Uri)GetValue(FrameSourceProperty); }
            set { SetValue(FrameSourceProperty, value); }
        }

        public event EventHandler FrameSourceChanged;
        #endregion FrameSource Property

        #region RightMarkerMargin Property
        public static readonly DependencyProperty RightMarkerMarginProperty = DependencyProperty.Register("RightMarkerMargin", typeof(int), typeof(PopupPageControl), new PropertyMetadata(0, RightMarkerMargin_ValueChanged));

        static void RightMarkerMargin_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PopupPageControl self = obj as PopupPageControl;
            if (self.RightMarkerMarginChanged != null) self.RightMarkerMarginChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The distance in pixels from the right where the marker begins"), Category("Own Properties"), DisplayName("RightMarkerMargin")]
        public int RightMarkerMargin
        {
            get { return (int)GetValue(RightMarkerMarginProperty); }
            set { SetValue(RightMarkerMarginProperty, value); }
        }

        public event EventHandler RightMarkerMarginChanged;
        #endregion RightMarkerMargin Property

        #endregion DependencyProperties
    }
}
