using SqlCEDatabaseUpgraderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardLib;

namespace MyKanban.Wizards
{
	public class CreateDatabaseWizard:Wizard<SqlCEDatabaseUpgrader>
	{

		public CreateDatabaseWizard()
		{
			this.Data = new SqlCEDatabaseUpgrader( new Models.MyKanban());

			Pages.Add(new StaticMessageWizardPage("Welcome", "Welcome to database creation Wizard !", "It seems it is the first time you are running this software. This process will guide you during creation of a new database."));
			Pages.Add(new WorkerDatabaseWizardPage("Create database", "Click on button bellow in order to create new database:", () => { return CreateDatabase(); } ));
			Pages.Add(new StaticMessageWizardPage("End", "You have sucessfully terminated database creation Wizard", "Click on finish button in order to quit."));
		}

		private async Task<bool> CreateDatabase()
		{
			await Data.CreateDatabaseAsync();
			return true;
			
		}


	}
}
