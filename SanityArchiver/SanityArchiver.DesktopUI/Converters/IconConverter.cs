using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
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
                        return GetImageSource(Resources.drive);
                    return GetImageSource(Resources.folder);
                case File file:
                    switch (file.Extension)
                    {
                        case ".txt":
                            return GetImageSource(Resources.txt);
                        case ".ENC":
                            return GetImageSource(Resources.enc);
                        case ".zip":
                            return GetImageSource(Resources.zip_file);
                        case ".exe":
                            return GetImageSource(Resources.exe);
                        case ".dll":
                            return GetImageSource(Resources.dll);
                        case ".jpg":
                        case ".png":
                        case ".bmp":
                            return GetImageSource(Resources.image);
                        default:
                            return GetImageSource(Resources.file);
                    }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BitmapSource GetImageSource(Bitmap bitmap) =>
            Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
    }
}