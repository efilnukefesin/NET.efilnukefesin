using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

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

            if ((value is Rectangle) && (double.TryParse(parameter.ToString(), out param)))
            {
                Rectangle sourceRect = (Rectangle)value;
                result = new Rect(0 - param, 0 - param, sourceRect.Width + (2 * param), sourceRect.Height + (2 * param));
            }
            else if ((value is Rectangle) && (parameter.ToString().Contains("/")))
            {
                string[] coords = parameter.ToString().Split('/');

                Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                Rectangle sourceRect = (Rectangle)value;
                result = new Rect(0 - point.X, 0 - point.Y, sourceRect.Width + (2 * point.X), sourceRect.Height + (2 * point.Y));
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
