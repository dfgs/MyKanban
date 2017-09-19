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
using DatabaseModelLib.Filters;
using DatabaseModelLib;

namespace MyKanban.ViewModels
{
	public class StateCollectionViewModel : RowViewModelCollection<StateViewModel, State>, IDropTarget, IDragSource
    {
		private BacklogViewModel backlog;

		public StateCollectionViewModel(IDatabaseViewModel Database, BacklogViewModel Backlog) : base(Database)
		{
			this.backlog = Backlog;
		}

        protected override Window OnCreateEditWindow()
        {
            return new EditStateWindow();
        }

        protected override Task<State> OnCreateEmptyModelAsync()
		{
			return System.Threading.Tasks.Task.FromResult(new State() { Name="New state", BacklogID=backlog.BacklogID,Index=Count });
		}

		protected override Task<StateViewModel> OnCreateViewModelItem(Type ModelType)
		{
			return System.Threading.Tasks.Task.FromResult(new StateViewModel(Database,backlog));
		}

		protected override Filter<State> OnCreateFilter()
		{
			return new EqualFilter<State>(State.BacklogIDColumn, backlog.BacklogID);
		}

		protected override IColumn<State>[] OnCreateOrders()
		{
			return new IColumn<State>[] { State.IndexColumn };
		}

		protected override bool OnRemoveCommandCanExecute(object Parameter)
		{
			StateViewModel state;
	
			state = Parameter as StateViewModel;
            if (state == null) return false;
            else return state.Tasks.Count==0;
		}

        #region IDropTarget
        public void DragOver(IDropInfo dropInfo)
		{
			if (dropInfo.Data is StateViewModel)
			{
				dropInfo.Effects = DragDropEffects.Move;
				dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
			}
            
        }

		public async void Drop(IDropInfo dropInfo)
		{
			StateViewModel state;
			int index;

			state = dropInfo.Data as StateViewModel;
			if (state == null) return;
			index = IndexOf(state);


			if (index < dropInfo.InsertIndex)
			{

				await AddAsync(dropInfo.InsertIndex, state, false);
				await RemoveAsync(state, false);
			}
			else
			{

				await RemoveAsync(state, false);
				await AddAsync(dropInfo.InsertIndex, state, false);
			}

			for (int t = 0; t < Count; t++)
			{
				this[t].Index = t;
				await Database.UpdateAsync(this[t].Model);
			}

			//throw new NotImplementedException();
		}
        #endregion

        #region IDragSource
        public void StartDrag(IDragInfo dragInfo)
        {
            backlog.IsDragging = true;

            dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            dragInfo.Data = dragInfo.SourceItem;
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            return true;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            backlog.IsDragging = false;
        }

        public void DragCancelled()
        {
            backlog.IsDragging = false;
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            return true;
        }
        #endregion





    }
}
