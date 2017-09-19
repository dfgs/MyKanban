using System;
using System.Globalization;
using System.Windows.Data;

namespace MyKanban.Views
{
	public class ElapsedTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			TimeSpan ts;
			
			ts=TimeSpan.FromSeconds((int)value);

			return ts.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
