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
            return new EqualFilter<Models.Task>(Models.Task.StateIDColumn, state.StateID);
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
            TaskViewModel task;
            TaskCollectionViewModel sourceTasks;
            int index;

            task = dropInfo.Data as TaskViewModel;
            if (task == null) return;

            sourceTasks = task.State.Tasks;

            if (sourceTasks == this)
            {
                index = this.IndexOf(task);
                if (index < dropInfo.InsertIndex)
                {
                    await this.AddAsync(dropInfo.InsertIndex, task, false);
                    await sourceTasks.RemoveAsync(task, false);
                }
                else
                {
                    await sourceTasks.RemoveAsync(task, false);
                    await this.AddAsync(dropInfo.InsertIndex, task, false);
                }
            }
            else
            {
                await sourceTasks.RemoveAsync(task, false);
                await this.AddAsync(dropInfo.InsertIndex, task, false);
            }

            task.StateID = this.state.StateID;
            task.Index = dropInfo.InsertIndex;


            for (int t = 0; t < this.Count; t++)
            {
                this[t].Index = t;
                await this.backlog.Database.UpdateAsync(this[t].Model);
            }
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
