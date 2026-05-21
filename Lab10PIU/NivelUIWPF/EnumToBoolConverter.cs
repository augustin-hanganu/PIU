using System;
using System.Globalization;
using System.Windows.Data;

namespace NivelUIWPF
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string enumString)
                return value?.ToString() == enumString;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string enumString)
                return Enum.Parse(targetType, enumString);
            return Binding.DoNothing;
        }
    }
}