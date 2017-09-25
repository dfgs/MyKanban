using DatabaseViewModelLib;
using GongSolutions.Wpf.DragDrop;
using ModelLib;
using MyKanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewLib;
using ViewModelLib;
using ViewModelLib.Attributes;

namespace MyKanban.ViewModels
{
	public class StateViewModel:RowViewModel<State>
    {
		

		public int? StateID
		{
			get { return Model.StateID; }
			set { Model.StateID = value; OnPropertyChanged(); }
		}

		public int? BacklogID
		{
			get { return Model.BacklogID; }
			set { Model.BacklogID = value; OnPropertyChanged(); }
		}

		public int? Index
		{
			get { return Model.Index; }
			set { Model.Index = value; OnPropertyChanged(); }
		}

		[TextProperty(Header = "Name", IsMandatory = true, IsReadOnly = false)]
		public Text? Name
		{
			get { return Model.Name; }
			set { Model.Name = value; OnPropertyChanged(); }
		}

        [TextProperty(Header = "Glyph", IsMandatory = true, IsReadOnly = false)]
        public Text? Glyph
        {
            get { return Model.Glyph; }
            set { Model.Glyph = value; OnPropertyChanged(); }
        }


		[IntProperty(Header = "MaxTaskAge", IsMandatory = false, IsReadOnly = false,MinValue = 0)]
		public int? MaxTaskAge
		{
			get { return Model.MaxTaskAge; }
			set { Model.MaxTaskAge = value; OnPropertyChanged(); }
		}

        private BacklogViewModel backlog;

        private TaskCollectionViewModel tasks;
        public TaskCollectionViewModel Tasks
        {
            get { return tasks; }
        }
		public BacklogViewModel Backlog
		{
			get { return backlog; }
		}

        public List<string> Glyphs
        {
            get { return Helper.Glyphs; }
        }

        /*public StateCollectionViewModel States
		{
			get { return backlog.States; }
		}*/


        public StateViewModel(IDatabaseViewModel Database, BacklogViewModel Backlog):base(Database)
		{
			this.backlog = Backlog;
            tasks = new TaskCollectionViewModel(backlog.Database, backlog, this);Children.Add(tasks);
		}


        protected override Task<State> OnLoadModelAsync()
        {
            return System.Threading.Tasks.Task.FromResult(Model);
        }

		

	}
}
