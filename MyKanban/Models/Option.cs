using DatabaseModelLib;
using ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKanban.Models
{
	public class Option
	{

		public static readonly Column<Option, int> OptionIDColumn = new Column<Option, int>() {  IsPrimaryKey = true, IsIdentity = true };
		public int? OptionID
		{
			get { return OptionIDColumn.GetValue(this); }
			set { OptionIDColumn.SetValue(this, value); }
		}

		
		public static readonly Column<Option, Text> NameColumn = new Column<Option, Text>();
		public Text? Name
		{
			get { return NameColumn.GetValue(this); }
			set { NameColumn.SetValue(this, value); }
		}


		public static readonly Column<Option, Text> ValueColumn = new Column<Option, Text>();
		public Text? Value
		{
			get { return ValueColumn.GetValue(this); }
			set { ValueColumn.SetValue(this, value); }
		}

	}
}
