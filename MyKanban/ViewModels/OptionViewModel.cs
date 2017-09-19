using DatabaseViewModelLib;
using ModelLib;
using MyKanban.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewLib;
using ViewModelLib;
using ViewModelLib.Attributes;

namespace MyKanban.ViewModels
{
	public class OptionViewModel:RowViewModel<Option>
	{
	

		public int? OptionID
		{
			get { return Model.OptionID; }
			set { Model.OptionID = value; OnPropertyChanged(); }
		}



		[TextProperty(Header = "Name", IsMandatory = true, IsReadOnly = false)]
		public Text? Name
		{
			get { return Model.Name; }
			set { Model.Name = value; OnPropertyChanged(); }
		}
		[TextProperty(Header = "Value", IsMandatory = true, IsReadOnly = false)]
		public Text? Value
		{
			get { return Model.Value; }
			set { Model.Value = value; OnPropertyChanged(); }
		}
		
	

		public OptionViewModel(IDatabaseViewModel Database):base(Database)
		{
		}


        protected override Task<Option> OnLoadModelAsync()
        {
            return System.Threading.Tasks.Task.FromResult(Model);
        }

    }
}
