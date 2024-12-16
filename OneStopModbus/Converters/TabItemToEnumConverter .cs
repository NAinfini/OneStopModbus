using DevExpress.Xpf.Core;
using OneStopModbus.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OneStopModbus.Converters
{
    public class TabItemToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConnectionHardware enumValue)
            {
                return enumValue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DXTabItem tabItem && tabItem.Tag is ConnectionHardware enumValue)
            {
                return enumValue;
            }
            return Binding.DoNothing;
        }
    }
}