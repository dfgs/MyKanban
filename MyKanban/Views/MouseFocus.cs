using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyKanban.Views
{
	public class MouseFocus:DependencyObject
	{
		public static readonly DependencyProperty IsCellFocusedProperty = DependencyProperty.RegisterAttached("IsCellFocused", typeof(bool), typeof(MouseFocus), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

		public static bool GetIsCellFocused(DependencyObject Element)
		{
			return (bool)Element.GetValue(IsCellFocusedProperty);
		}
		public static void SetIsCellFocused(DependencyObject Element,bool Value)
		{
			Element.SetValue(IsCellFocusedProperty, Value);
		}


		public MouseFocus()
		{

		}
	}
}
