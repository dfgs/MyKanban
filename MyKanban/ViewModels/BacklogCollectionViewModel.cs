using DatabaseViewModelLib;
using MyKanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLib;

namespace MyKanban.ViewModels
{
	public class BacklogCollectionViewModel : RowViewModelCollection<BacklogViewModel, Backlog>
	{
 
        public BacklogCollectionViewModel(IDatabaseViewModel Database) : base(Database)
		{
		}

        protected override Window OnCreateEditWindow()
        {
            return new EditBacklogWindow();
        }

        protected override Task<Backlog> OnCreateEmptyModelAsync()
		{
			return System.Threading.Tasks.Task.FromResult(new Backlog() { Name="New backlog" } );
		}
        protected override Task<BacklogViewModel> OnCreateViewModelItem(Type ModelType)
        {
			return System.Threading.Tasks.Task.FromResult(new BacklogViewModel(Database));
		}

		protected override bool OnRemoveCommandCanExecute(object Parameter)
		{
            BacklogViewModel item;

            if (Parameter == null) item = SelectedItem; else item = Parameter as BacklogViewModel;
			if (item == null) return false;
			return (item.States.Count == 0) ;
		}

        public override System.Threading.Tasks.Task OnAddCommandExecuted(BacklogViewModel ViewModel)
        {
            this.SelectedItem = ViewModel;
            return base.OnAddCommandExecuted(ViewModel);
        }
        
		
    }
}
