using System;
using System.Windows.Data;

namespace Sheepsteak.EchoesJS.UI.Assets.Converters
{
    public class DateTimeOffsetToRelativeTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((DateTimeOffset)value).ToNaturalRelativeTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
