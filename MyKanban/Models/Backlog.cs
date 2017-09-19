using DatabaseModelLib;
using ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace MyKanban.Models
{
	public class Backlog
	{

		public static readonly Column<Backlog, int> BacklogIDColumn = new Column<Backlog, int>() { IsPrimaryKey = true, IsIdentity = true };
		public int? BacklogID
		{
			get { return BacklogIDColumn.GetValue(this); }
			set { BacklogIDColumn.SetValue(this, value); }
		}


		public static readonly Column<Backlog, Text> NameColumn = new Column<Backlog, Text>();
		public Text? Name
		{
			get { return NameColumn.GetValue(this); }
			set { NameColumn.SetValue(this, value); }
		}

        public static readonly Column<Backlog, Text> GlyphColumn = new Column<Backlog, Text>() { DefaultValue = "" };
        public Text? Glyph
        {
            get { return GlyphColumn.GetValue(this); }
            set { GlyphColumn.SetValue(this, value); }
        }

    }
}
