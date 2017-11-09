using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModelLib;
using System.Threading.Tasks;
using ModelLib;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using DatabaseModelLib.Filters;
using DatabaseViewModelLib;
using MyKanban.Models;
using SqlCEDatabaseModelLib;
using System.Data.SqlServerCe;
using DatabaseModelLib;

namespace MyKanban.ViewModels
{
    public class TaskCollectionViewModel : RowViewModelCollection<TaskViewModel, Models.Task>,IDropTarget,IDragSource
    {
        private BacklogViewModel backlog;
        private StateViewModel state;

        public TaskCollectionViewModel(IDatabaseViewModel Database, BacklogViewModel Backlog,StateViewModel State) : base(Database)
        {
            this.backlog = Backlog;
            this.state = State;
        }

        protected override Window OnCreateEditWindow()
        {
            return new EditTaskWindow();
        }

		
		protected override async Task<bool> OnEditInModelAsync(TaskViewModel ViewModel)
		{
			ViewModel.UpdateDate = DateTime.Now;
			return await base.OnEditInModelAsync(ViewModel);
		}

		protected override Task<Models.Task> OnCreateEmptyModelAsync()
        {
            return System.Threading.Tasks.Task.FromResult(new Models.Task() {StateID=state.StateID,Index=Count });

        }
		protected override Task<TaskViewModel> OnCreateViewModelItem(Type ModelType)
		{
            return System.Threading.Tasks.Task.FromResult(new TaskViewModel(Database,backlog));
        }


		protected override Filter<Models.Task> OnCreateFilter()
        {
			DateTime minUpdateDate;

			if (state.MaxTaskAge==null) minUpdateDate = DateTime.Now.AddDays(-36500);
			else minUpdateDate = DateTime.Now.AddDays(-state.MaxTaskAge.Value);

			return new AndFilter<Models.Task>( 
				new AndFilter<Models.Task>(
					new EqualFilter<Models.Task>(Models.Task.StateIDColumn, state.StateID),
					new GreaterOrEqualFilter<Models.Task>(Models.Task.UpdateDateColumn, minUpdateDate)
				)
			);
        }

		protected override IColumn<Models.Task>[] OnCreateOrders()
		{
			return new IColumn<Models.Task>[] { Models.Task.IndexColumn };
		}

	

		#region IDropTarget
		public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is TaskViewModel) 
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
        }

        public async void Drop(IDropInfo dropInfo)
        {
			TaskCollectionViewModel sourceTasks;
			TaskViewModel movedTask;
			int index;
			int insertIndex;

			
			movedTask = dropInfo.Data as TaskViewModel;
            if (movedTask == null) return;

			sourceTasks = movedTask.State.Tasks;

			if ((sourceTasks==this) && (dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem)) insertIndex = dropInfo.InsertIndex - 1;
			else insertIndex = dropInfo.InsertIndex;

			if (movedTask.StateID != this.state.StateID)
			{
				movedTask.StateID = this.state.StateID;
				movedTask.UpdateDate = DateTime.Now;		// update date when stateid changed
			}
			movedTask.Index = insertIndex;
			await Database.UpdateAsync(movedTask.Model);

			index = 0;
			foreach (TaskViewModel task in this)
			{
				if (index == insertIndex) index++;
				if (task == movedTask) continue;

				task.Index = index;
				//task.UpdateDate = DateTime.Now;
				index++;

				
				await Database.UpdateAsync(task.Model);
			}


			if (sourceTasks!=this) await sourceTasks.LoadAsync();
			await LoadAsync();

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
            backlog.IsDragging =false;
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            return true;
        }
        #endregion

    }
}
