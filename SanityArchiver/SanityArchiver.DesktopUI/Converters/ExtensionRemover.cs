using System;
using System.Globalization;
using System.Windows.Data;

namespace SanityArchiver.DesktopUI.Converters
{
    public class ExtensionRemover : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string name)) return "";
            
            var length = name.LastIndexOf('.');
            return (length == -1) ? name : name.Substring(0, length);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}