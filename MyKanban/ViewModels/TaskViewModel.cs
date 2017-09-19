using ModelLib;
using MyKanban.Models;
using System;
using System.Windows;
using ViewModelLib;
using ViewModelLib.Attributes;
using DatabaseModelLib;
using DatabaseViewModelLib;

namespace MyKanban.ViewModels
{
	
    public class TaskViewModel : RowViewModel<Task>
	{

		public static readonly DependencyProperty StartStopCommandProperty = DependencyProperty.Register("StartStopCommand", typeof(ViewModelCommand), typeof(TaskViewModel));
		public ViewModelCommand StartStopCommand
		{
			get { return (ViewModelCommand)GetValue(StartStopCommandProperty); }
			private set { SetValue(StartStopCommandProperty, value); }
		}


		public int? TaskID
		{
			get { return Model.TaskID; }
			set { Model.TaskID = value; OnPropertyChanged(); }
		}

		/*public int? BacklogID
		{
			get { return Model.BacklogID; }
			set { Model.BacklogID = value; OnPropertyChanged(); }
		}*/


		[TextProperty(Header = "Title", IsMandatory = true, IsReadOnly = false)]
		public Text? Title
		{
			get { return Model.Title; }
			set { Model.Title = value; OnPropertyChanged(); }
		}
		[LargeTextProperty(Header = "Description", IsMandatory = false, IsReadOnly = false)]
		public Text? Description
		{
			get { return Model.Description; }
			set { Model.Description = value; OnPropertyChanged(); }
		}
		[ColorProperty(Header = "Color", IsMandatory = true, IsReadOnly = false)]
		public Text? Color
		{
			get { return Model.Color; }
			set { Model.Color = value; OnPropertyChanged(); }
		}


		public DateTime? StartDate
		{
			get { return Model.StartDate; }
			set { Model.StartDate = value; OnPropertyChanged(); }
		}

		public int? ElapsedTime
		{
			get { return Model.ElapsedTime; }
			set { Model.ElapsedTime = value; OnPropertyChanged(); }
		}
		
		public bool IsRunning
		{
			get { return Model.StartDate != null; }
		}
        public string Glyph
        {
            get
            {
                return IsRunning ? "" : "";
            }
        }
		/*public int? LaneID
		{
			get { return Model.LaneID; }
			set { Model.LaneID = value; OnPropertyChanged(); }
		}*/

		public int? StateID
		{
			get { return Model.StateID; }
			set { Model.StateID = value; OnPropertyChanged(); }
		}

		public int? Index
		{
			get { return Model.Index; }
			set { Model.Index = value; OnPropertyChanged(); }
		}
                		
			
		private BacklogViewModel backlog;
		public BacklogViewModel Backlog
		{
			get { return backlog; }
		}

        public StateViewModel State
        {
            get
            {
                foreach(StateViewModel state in backlog.States)
                {
                    if (state.StateID == StateID) return state;
                }
                return null;
            }
        }

		public TaskViewModel(IDatabaseViewModel Database, BacklogViewModel Backlog) :base(Database)
		{
			StartStopCommand = new ViewModelCommand(OnStartStopCommandCanExecute, OnStartStopCommandExecute);

			this.backlog = Backlog;
		}

		protected virtual bool OnStartStopCommandCanExecute(object Parameter)
		{
			return IsLoaded;
		}
		private async void OnStartStopCommandExecute(object Parameter)
		{
			if (IsRunning) await OnStop();
			else await OnStart();
		}

		private async System.Threading.Tasks.Task OnStart()
		{
			try
			{
				StartDate = DateTime.Now;
				await Backlog.Database.UpdateAsync(Model);
                OnPropertyChanged("IsRunning");
                OnPropertyChanged("Glyph");
            }
            catch (Exception ex)
			{
				Log(ex.Message);
			}
		}
		private async System.Threading.Tasks.Task OnStop()
		{
			try
			{
				ElapsedTime += (int)(DateTime.Now - StartDate.Value).TotalSeconds;
				StartDate = null;
				await Backlog.Database.UpdateAsync(Model);
				OnPropertyChanged("IsRunning");
                OnPropertyChanged("Glyph");
            }
            catch (Exception ex)
			{
				Log(ex.Message);
			}

		}

        protected override System.Threading.Tasks.Task<Models.Task> OnLoadModelAsync()
        {
            return System.Threading.Tasks.Task.FromResult(Model);
        }

    }

}
