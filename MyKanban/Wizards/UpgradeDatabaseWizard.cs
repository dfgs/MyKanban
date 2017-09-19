using Microsoft.Win32;
using SqlCEDatabaseUpgraderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardLib;

namespace MyKanban.Wizards
{
	public class UpgradeDatabaseWizard:Wizard<SqlCEDatabaseUpgrader>
	{
		//private SqlCEDatabaseUpgrader upgrader;

		public UpgradeDatabaseWizard()
		{
			//this.upgrader = Upgrader;
			this.Data = new SqlCEDatabaseUpgrader(new Models.MyKanban());

			Pages.Add(new StaticMessageWizardPage("Welcome", "Welcome to database migration Wizard !", "Your database is not up to date. This process will guide you during upgrade of database."));
			Pages.Add(new WorkerDatabaseWizardPage("Backup database", "Click on button bellow in order to backup database:",  () => { return BackupDatabase(); } ));
			Pages.Add(new WorkerDatabaseWizardPage("Upgrade database", "Click on button bellow in order to upgrade database:", ()=> { return UpgradeDatabase(); }  ));
			Pages.Add(new StaticMessageWizardPage("End", "You have sucessfully terminated database upgrade Wizard", "Click on finish button in order to quit."));
		}

		private async Task<bool> UpgradeDatabase()
		{
			try
			{
				await Data.UpgradeAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
		private async Task<bool> BackupDatabase()
		{
			SaveFileDialog dialog;
			bool result;

			dialog = new SaveFileDialog();
			dialog.Title = "Backup database to";
			dialog.FileName = "MyKanban.bak";
			dialog.Filter = "All files|*.*";

			result = dialog.ShowDialog() ?? false;
			if (result)
			{
				await Data.BackupAsync(dialog.FileName);
			}
			return result;

		}

	}
}
