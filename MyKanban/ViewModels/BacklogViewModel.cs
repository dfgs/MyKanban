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
using System.Windows.Media;
using ViewModelLib;
using ViewModelLib.Attributes;


namespace MyKanban.ViewModels
{
    public class BacklogViewModel : RowViewModel<Backlog>, IDropTarget
    {



        public int? BacklogID
        {
            get { return Model.BacklogID; }
            set { Model.BacklogID = value; OnPropertyChanged(); }
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
            set { Model.Glyph = value;OnPropertyChanged(); }
        }

        /*private IDatabaseViewModel database;
        public IDatabaseViewModel Database
        {
            get { return database; }
        }*/



        private StateCollectionViewModel states;
        public StateCollectionViewModel States
        {
            get { return states; }
        }

        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.Register("IsDragging", typeof(bool), typeof(BacklogViewModel));
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public List<string> Glyphs
        {
            get { return Helper.Glyphs; }
        }
       

        public BacklogViewModel(IDatabaseViewModel Database):base(Database)
        {
			states = new StateCollectionViewModel(Database, this); Children.Add(states);
        }

        protected override Task<Backlog> OnLoadModelAsync()
        {
            return System.Threading.Tasks.Task.FromResult(Model);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            StateViewModel state;

            if (dropInfo.Data is TaskViewModel)
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
            else if (dropInfo.Data is StateViewModel)
            {
                state = (StateViewModel)dropInfo.Data;
                if (state.Tasks.Count == 0)
                {
                    dropInfo.Effects = DragDropEffects.Move;
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                }
            }
        }

        public async void Drop(IDropInfo dropInfo)
        {
            TaskViewModel task;
            StateViewModel state;


            if (dropInfo.Data is TaskViewModel)
            {
                task = (TaskViewModel)dropInfo.Data;
                await task.State.Tasks.RemoveAsync(task);
            }
            else if (dropInfo.Data is StateViewModel)
            {
                state = (StateViewModel)dropInfo.Data;
                await states.RemoveAsync(state);
            }


        }

		public override bool IsModelEqualTo(Backlog Other)
		{
			return BacklogID == Other.BacklogID;
		}

	}

}
