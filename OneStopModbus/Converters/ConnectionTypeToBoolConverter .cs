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
    public class ConnectionTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConnectionType connectionType)
            {
                return connectionType == ConnectionType.Slave; // True if Slave, False otherwise
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? ConnectionType.Slave : ConnectionType.Master; // Slave if True, Master otherwise
            }
            return ConnectionType.Master;
        }
    }
}