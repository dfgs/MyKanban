using SqlCEDatabaseUpgraderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLib;
using WizardLib;



namespace MyKanban.Wizards
{
	public class WorkerDatabaseWizardPage : WizardPage<SqlCEDatabaseUpgrader>
	{
		//private MyKanban.Models.MyKanban db;

		public static readonly DependencyProperty WorkerCommandProperty = DependencyProperty.Register("WorkerCommand", typeof(ViewModelCommand), typeof(WorkerDatabaseWizardPage));
		public ViewModelCommand WorkerCommand
		{
			get { return (ViewModelCommand)GetValue(WorkerCommandProperty); }
			private set { SetValue(WorkerCommandProperty, value); }
		}

		private Func<Task<bool>> action;

		private string header;
		public override string Header
		{
			get { return header; }
		}

		private string title;
		public string Title
		{
			get { return title; }
		}

		public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(RunStatus), typeof(WorkerDatabaseWizardPage));
		public RunStatus Status
		{
			get { return (RunStatus)GetValue(StatusProperty); }
			private set { SetValue(StatusProperty, value); }
		}

		public WorkerDatabaseWizardPage(string Header,string Title,Func<Task<bool>> Action)
		{
			
			this.title = Title;
			this.action = Action;
			this.header = Header;
			WorkerCommand = new ViewModelCommand(OnWorkCommandCanExecute, OnWorkCommandExecute);

			//db = new Models.MyKanban();
			this.Status = RunStatus.Waiting;
		}

		protected virtual bool OnWorkCommandCanExecute(object Parameter)
		{
			return (Status!=RunStatus.Done) && (Status != RunStatus.Running);
		}

		private async void OnWorkCommandExecute(object Parameter)
		{
			bool result;

			try
			{
				Status = RunStatus.Running;
				result = await action();
				if (result) Status = RunStatus.Done;
				else
				{
					Status = RunStatus.Error;
					ErrorMessage = "Operation canceled";
				}
			}
			catch (Exception ex)
			{
				Status = RunStatus.Error;
				ErrorMessage = ex.Message;
			}
		}


		public override bool OnCanGoNext()
		{
			return Status==RunStatus.Done;
		}

		public override bool OnCanGoPrevious()
		{
			return Status!=RunStatus.Running;
		}
		

	}
}
