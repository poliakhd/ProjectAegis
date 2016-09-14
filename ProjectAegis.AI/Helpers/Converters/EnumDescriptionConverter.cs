namespace ProjectAegis.AI.Helpers.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class EnumDescriptionConverter : IValueConverter
    {
        private string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(false);

            if (attributes.Length == 0)
                return value.ToString();

            return attributes.Cast<DescriptionAttribute>().FirstOrDefault()?.Description ?? "Unknown";
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetEnumDescription((Enum)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}