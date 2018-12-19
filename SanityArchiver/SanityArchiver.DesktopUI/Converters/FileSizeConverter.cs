using System;
using System.Globalization;
using System.Windows.Data;

namespace SanityArchiver.DesktopUI.Converters
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (long)value;
            if (bytes == 0) return "0 Byte";
            
            string[] sizes = {"Bytes", "KB", "MB", "GB", "TB"};
            
            var i = (int) (Math.Floor(Math.Log(bytes) / Math.Log(1024)));
            return $"{Math.Round(bytes / Math.Pow(1024, i), 2)} {sizes[i]}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}