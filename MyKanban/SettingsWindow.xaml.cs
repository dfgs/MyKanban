using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using ViewLib;

namespace MyKanban
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }


        private void OKCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IValidate validateBacklog;
            IValidate validateState;

            validateBacklog = editBacklogControl.DataContext as IValidate;
            validateState = editStateControl.DataContext as IValidate;

            e.CanExecute = (validateBacklog?.Validate() ?? true) && (validateState?.Validate() ?? true);
            e.Handled = true;
        }

        private void OKCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IValidate validateBacklog;
            IValidate validateState;

            validateBacklog = editBacklogControl.DataContext as IValidate;
            validateState = editStateControl.DataContext as IValidate;

            validateState.Commit();
            validateBacklog.Commit();

            DialogResult = true;
        }

        private void CancelCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; e.Handled = true;
        }

        private void CancelCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ApplyCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IValidate validateBacklog;
            IValidate validateState;

            validateBacklog = editBacklogControl.DataContext as IValidate;
            validateState = editStateControl.DataContext as IValidate;

            e.CanExecute = (validateBacklog?.Validate() ?? true) && (validateState?.Validate() ?? true);
            e.Handled = true;
        }

        private void ApplyCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IValidate validateBacklog;
            IValidate validateState;

            validateBacklog = editBacklogControl.DataContext as IValidate;
            validateState = editStateControl.DataContext as IValidate;

            validateState.Commit();
            validateBacklog.Commit();
        }


    }
}
