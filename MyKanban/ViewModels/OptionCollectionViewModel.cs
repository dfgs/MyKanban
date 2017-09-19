using MyKanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;
using ModelLib;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using DatabaseViewModelLib;

namespace MyKanban.ViewModels
{

	public class OptionCollectionViewModel : RowViewModelCollection<OptionViewModel, Option>
	{

		public OptionCollectionViewModel(IDatabaseViewModel Database) : base(Database)
		{
		}

		protected override System.Threading.Tasks.Task<Option> OnCreateEmptyModelAsync()
		{
			return System.Threading.Tasks.Task.FromResult(new Option() { Name="New option",Value="TBD" } );
		}

		protected override Task<OptionViewModel> OnCreateViewModelItem(Type ModelType)
		{
			return System.Threading.Tasks.Task.FromResult(new OptionViewModel(Database));
		}

	



	}
}
