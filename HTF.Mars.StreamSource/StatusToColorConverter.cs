using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HTF.Mars.StreamSource
{
    public class StatusToColorConverter : IValueConverter
    {
        public Brush OfflineBrush { get; set; }
        public Brush OnlineBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cyrillicString = value as CyrillicString;
            if (cyrillicString != null)
            {
                if (cyrillicString.Latin == "ONLINE")
                {
                    return OnlineBrush;
                }
            }
            return OfflineBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
