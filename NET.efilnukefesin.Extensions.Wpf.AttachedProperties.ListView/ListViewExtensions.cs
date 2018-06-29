using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace NET.efilnukefesin.Extensions.Wpf.AttachedProperties.ListView
{
    public class ListViewExtensions
    {
        #region Properties

        private static readonly DependencyProperty SelectedValueBinderProperty = DependencyProperty.RegisterAttached("SelectedValueBinder", typeof(ListViewSelectedItemsBinder), typeof(ListViewExtensions));

        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.RegisterAttached("SelectedValues", typeof(IList), typeof(ListViewExtensions), new FrameworkPropertyMetadata(null, onSelectedValuesChanged));

        #endregion Properties

        #region Methods

        #region getSelectedValueBinder
        private static ListViewSelectedItemsBinder getSelectedValueBinder(DependencyObject obj)
        {
            return (ListViewSelectedItemsBinder)obj.GetValue(SelectedValueBinderProperty);
        }
        #endregion getSelectedValueBinder

        #region setSelectedValueBinder
        private static void setSelectedValueBinder(DependencyObject obj, ListViewSelectedItemsBinder items)
        {
            obj.SetValue(SelectedValueBinderProperty, items);
        }
        #endregion setSelectedValueBinder

        #region onSelectedValuesChanged
        private static void onSelectedValuesChanged(DependencyObject o, DependencyPropertyChangedEventArgs value)
        {
            var oldBinder = ListViewExtensions.getSelectedValueBinder(o);
            if (oldBinder != null)
            {
                oldBinder.UnBind();
            }

            ListViewExtensions.setSelectedValueBinder(o, new ListViewSelectedItemsBinder((System.Windows.Controls.ListView)o, (IList)value.NewValue));
            ListViewExtensions.getSelectedValueBinder(o).Bind();
        }
        #endregion onSelectedValuesChanged

        #region SetSelectedValues
        public static void SetSelectedValues(Selector elementName, IEnumerable value)
        {
            elementName.SetValue(SelectedValuesProperty, value);
        }
        #endregion SetSelectedValues

        #region GetSelectedValues
        public static IEnumerable GetSelectedValues(Selector elementName)
        {
            return (IEnumerable)elementName.GetValue(SelectedValuesProperty);
        }
        #endregion GetSelectedValues

        #endregion Methods
    }
}
