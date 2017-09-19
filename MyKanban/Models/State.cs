using DatabaseModelLib;
using ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKanban.Models
{
	public class State
	{

		public static readonly Column<State, int> StateIDColumn = new Column<State, int>() {  IsPrimaryKey = true, IsIdentity = true };
		public int? StateID
		{
			get { return StateIDColumn.GetValue(this); }
			set { StateIDColumn.SetValue(this, value); }
		}

		public static readonly Column<State, int> BacklogIDColumn = new Column<State, int>();
		public int? BacklogID
		{
			get { return BacklogIDColumn.GetValue(this); }
			set { BacklogIDColumn.SetValue(this, value); }
		}
		public static readonly Column<State, int> IndexColumn = new Column<State, int>();
		public int? Index
		{
			get { return IndexColumn.GetValue(this); }
			set { IndexColumn.SetValue(this, value); }
		}

		public static readonly Column<State, Text> NameColumn = new Column<State, Text>();
		public Text? Name
		{
			get { return NameColumn.GetValue(this); }
			set { NameColumn.SetValue(this, value); }
		}

        [Revision(1)]
        public static readonly Column<State, Text> GlyphColumn = new Column<State, Text>() { DefaultValue = "" };
        public Text? Glyph
        {
            get { return GlyphColumn.GetValue(this); }
            set { GlyphColumn.SetValue(this, value); }
        }
    }
}
