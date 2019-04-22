using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.efilnukefesin.Extensions.Wpf.Converters
{
    /// <summary>
    /// Converter for Rectangle Resizing, useable for generating a bigger or smaller rectangle than the source.
    /// </summary>
    public class RectangleResizeConverter : IValueConverter
    {
        #region Methods

        #region Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion Convert

        #region ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion ConvertBack

        #endregion Methods
    }
}
