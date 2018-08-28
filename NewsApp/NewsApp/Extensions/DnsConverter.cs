using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Extensions
{
    class DnsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var IsValidDate = DateTime.TryParse((string)value, out DateTime date);
            if (IsValidDate)
            {
                var day = date.Date.DayOfWeek;
                var month = date.ToString("MMMM");
                var year = date.Year;
                return $"{month} {day}, {year}";
            }
            else
                return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
