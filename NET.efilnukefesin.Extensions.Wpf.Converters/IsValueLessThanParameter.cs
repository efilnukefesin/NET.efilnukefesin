using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.efilnukefesin.Extensions.Wpf.Converters
{
    public class IsValueLessThanParameter : IValueConverter
    {
        #region Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double actualWidth = (Double)value;

            if (actualWidth < Int32.Parse((string)parameter))
            {
                return true;
            }

            return false;
        }
        #endregion Convert

        #region ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion ConvertBack
    }
}
