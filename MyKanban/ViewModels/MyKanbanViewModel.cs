using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;
using System.Windows;
using MyKanban.Models;
using DatabaseViewModelLib;

namespace MyKanban.ViewModels
{
	public class MyKanbanViewModel : DatabaseViewModel<Models.MyKanban>
	{
		private BacklogCollectionViewModel backlogs;
		public BacklogCollectionViewModel Backlogs
		{
			get { return backlogs; }
		}

		private OptionCollectionViewModel options;
		public OptionCollectionViewModel Options
		{
			get { return options; }
		}


		public MyKanbanViewModel()
		{
			backlogs = new BacklogCollectionViewModel(this); Children.Add(backlogs);
			options = new OptionCollectionViewModel(this); Children.Add(options);

		}
		protected override Task<Models.MyKanban> OnLoadModelAsync()
		{
			return System.Threading.Tasks.Task.FromResult(new Models.MyKanban());
		}

		

	}
}
