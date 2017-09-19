using DatabaseModelLib;
using ModelLib;
using SqlCEDatabaseModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKanban.Models
{
	public class MyKanban : SqlCEDatabase
	{
		

		public static readonly Table<Backlog> Backlogs = new Table<Models.Backlog>();
		public static readonly Table<State> States = new Table<State>();
		public static readonly Table<Task> Tasks = new Table<Task>();
		//[Revision(1)]
		public static readonly Table<Option> Options = new Table<Option>();


		public static readonly Relation<Backlog, State, int> BacklogToState = new Relation<Models.Backlog, Models.State, int>(Backlog.BacklogIDColumn, State.BacklogIDColumn);
		//public static readonly Relation<Backlog, Task, int> BacklogToTask = new Relation<Models.Backlog, Models.Task, int>(Backlog.BacklogIDColumn, Task.BacklogIDColumn);

		public static readonly Relation<State, Task, int> StateToTask = new Relation<Models.State, Models.Task, int>(State.StateIDColumn, Task.StateIDColumn, DeleteReferentialAction.None);


		public MyKanban() : base("MyKanban.sdf") 
		{
		}

		
		

		
		
	}
}
