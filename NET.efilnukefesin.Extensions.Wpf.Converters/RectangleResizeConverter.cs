using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            Rect result = default(Rect);
            double param = 0f;

            if ((value is Rect) && (double.TryParse(parameter.ToString(), out param)))
            {
                Rect sourceRect = (Rect)value;
                result = new Rect(sourceRect.X - param, sourceRect.Y - param, sourceRect.Width + (2 * param), sourceRect.Height + (2 * param));
            }

            return result;
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
