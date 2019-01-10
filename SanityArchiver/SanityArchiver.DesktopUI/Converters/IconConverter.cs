using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using SanityArchiver.DesktopUI.Properties;
using Directory = SanityArchiver.Application.Models.Directories.Directory;
using File = SanityArchiver.Application.Models.Files.File;

namespace SanityArchiver.DesktopUI.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Directory directory:
                    if (DriveInfo.GetDrives().Any(drive => directory.Name == drive.Name))
                        return @"..\Icons\drive.png";
                    return @"..\Icons\folder.png";
                case File file:
                    switch (file.Extension)
                    {
                        case ".txt":
                            return @"..\Icons\txt.png";;
                        case ".ENC":
                            return @"..\Icons\enc.png";;
                        case ".zip":
                            return @"..\Icons\zip.png";;
                        default:
                            return @"..\Icons\file.png";;
                    }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}