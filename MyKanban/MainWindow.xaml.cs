using SqlCEDatabaseUpgraderLib;
using MyKanban.ViewModels;
using MyKanban.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Threading;

namespace MyKanban
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow 
    {
		static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

		private MyKanbanViewModel vm;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private bool closeFromMenu;

		public MainWindow()
		{
			if (!mutex.WaitOne(TimeSpan.Zero, true))
			{
				closeFromMenu = true;
				Close();
			}

			InitializeComponent();

			closeFromMenu = false;
			notifyIcon= new System.Windows.Forms.NotifyIcon();
			notifyIcon.Icon = global::MyKanban.Properties.Resources.appbar_column_three;
			notifyIcon.Visible = true;
			notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { new System.Windows.Forms.MenuItem("Close",new EventHandler(closeMenuItemClicked)) });
			notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            
		}


		private void closeMenuItemClicked(object sender,EventArgs e)
		{
			closeFromMenu = true;
			Close();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			this.WindowState = WindowState.Maximized;
			this.Visibility = Visibility.Visible;
			this.Activate();
		}

		protected override void OnStateChanged(EventArgs e)
		{
			base.OnStateChanged(e);
			if (WindowState==WindowState.Minimized)
			{
				this.Visibility = Visibility.Hidden;
			}
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			#if DEBUG
			return;
			#endif
			if (!closeFromMenu)
			{
				e.Cancel = true;
				this.WindowState = WindowState.Minimized;
			}
			else base.OnClosing(e);
		}
		protected override void OnClosed(EventArgs e)
		{
			notifyIcon.Visible = false;
			notifyIcon.Dispose();

			base.OnClosed(e);
		}

		private void ConnectCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=DataContext == null;
			e.Handled = true;
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			bool result;
			Window wizardWindow;
			SqlCEDatabaseUpgrader upgrader;

			MyKanban.Models.MyKanban db = new Models.MyKanban();
			upgrader = new SqlCEDatabaseUpgrader(db);

			//await db.DropAsync();

			#region create new database
			try
			{
				result = await upgrader.DatabaseExistsAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this,"Failed to check database presence: " + ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
				return;
			}

			if (!result)
			{
				wizardWindow = new DatabaseWizardWindow();
				wizardWindow.Owner = this;
				wizardWindow.DataContext = new CreateDatabaseWizard();
				wizardWindow.ShowDialog();
			}
			#endregion

			#region create schema
			try
			{
				result = await upgrader.SchemaExistsAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Failed to check schema presence: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!result)
			{
				wizardWindow = new DatabaseWizardWindow();
				wizardWindow.Owner = this;
				wizardWindow.DataContext = new CreateSchemaWizard();
				wizardWindow.ShowDialog();
			}
			#endregion

			#region upgrade database
			try
			{
				result = await upgrader.NeedsUpgradeAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Failed to check database version: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (result)
			{
				wizardWindow = new DatabaseWizardWindow();
				wizardWindow.Owner = this;
				wizardWindow.DataContext = new UpgradeDatabaseWizard();
				if (!wizardWindow.ShowDialog()??false)
				{
					MessageBox.Show(this, "Failed to upgrade database version" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}
			#endregion


			try
			{
				vm = new MyKanbanViewModel();
				DataContext = vm;
				await vm.LoadAsync();
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, "Failed to load database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				DataContext = null;
			}

		}

        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window;

            window = new AboutWindow() { Owner = this };
            window.ShowDialog();
        }



    }
}
