using DatabaseModelLib;
using ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKanban.Models
{
	public class Task
	{

		public static readonly Column<Task, int> TaskIDColumn = new Column<Task, int>() { IsPrimaryKey=true,IsIdentity=true };
		public int? TaskID
		{
			get { return TaskIDColumn.GetValue(this); }
			set { TaskIDColumn.SetValue(this, value); }
		}

		/*public static readonly Column<Task, int> BacklogIDColumn = new Column<Task, int>();
		public int? BacklogID
		{
			get { return BacklogIDColumn.GetValue(this); }
			set { BacklogIDColumn.SetValue(this, value); }
		}*/

		[Revision(2)]
		public static readonly Column<Task, DateTime> CreationDateColumn = new Column<Task, DateTime>() { DefaultValue = DateTime.Now };
		public DateTime? CreationDate
		{
			get { return CreationDateColumn.GetValue(this); }
			set { CreationDateColumn.SetValue(this, value); }
		}

		[Revision(3)]
		public static readonly Column<Task, DateTime> UpdateDateColumn = new Column<Task, DateTime>() { DefaultValue = DateTime.Now };
		public DateTime? UpdateDate
		{
			get { return UpdateDateColumn.GetValue(this); }
			set { UpdateDateColumn.SetValue(this, value); }
		}

		public static readonly Column<Task, int> StateIDColumn = new Column<Task, int>();
		public int? StateID
		{
			get { return StateIDColumn.GetValue(this); }
			set { StateIDColumn.SetValue(this, value); }
		}

		public static readonly Column<Task, int> IndexColumn = new Column<Task, int>();
		public int? Index
		{
			get { return IndexColumn.GetValue(this); }
			set { IndexColumn.SetValue(this, value); }
		}

		public static readonly Column<Task, Text> TitleColumn = new Column<Task, Text>();
		public Text? Title
		{
			get { return TitleColumn.GetValue(this); }
			set { TitleColumn.SetValue(this, value); }
		}

		//[Revision(2)]
		public static readonly Column<Task, Text> DescriptionColumn = new Column<Task, Text>() { IsNullable = true };
		public Text? Description
		{
			get { return DescriptionColumn.GetValue(this); }
			set { DescriptionColumn.SetValue(this, value); }
		}


		//[Revision(3)]
		public static readonly Column<Task, Text> ColorColumn = new Column<Task, Text>() { DefaultValue = (string)"OrangeRed" };
		public Text? Color
		{
			get { return ColorColumn.GetValue(this); }
			set { ColorColumn.SetValue(this, value); }
		}



		//[Revision(4)]
		public static readonly Column<Task, DateTime> StartDateColumn = new Column<Task, DateTime>() { IsNullable = true };
		public DateTime? StartDate
		{
			get { return StartDateColumn.GetValue(this); }
			set { StartDateColumn.SetValue(this, value); }
		}


		//[Revision(4)]
		public static readonly Column<Task, int> ElapsedTimeColumn = new Column<Task, int>() { DefaultValue = 0 };
		public int? ElapsedTime
		{
			get { return ElapsedTimeColumn.GetValue(this); }
			set { ElapsedTimeColumn.SetValue(this, value); }
		}

		public Task()
		{
			CreationDate = DateTime.Now;
			UpdateDate = DateTime.Now;
		}
	}
}
