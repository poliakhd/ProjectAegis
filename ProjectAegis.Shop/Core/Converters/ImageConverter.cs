namespace ProjectAegis.Shop.Core.Converters
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            try
            {
                var path = $@"{Directory.GetCurrentDirectory()}\{value as string}";
                
                if(string.IsNullOrEmpty(path) || !File.Exists(path))
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/page_green.png"));

                return new BitmapImage(new Uri(path));
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}