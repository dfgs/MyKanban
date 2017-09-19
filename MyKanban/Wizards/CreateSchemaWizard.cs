using SqlCEDatabaseUpgraderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardLib;

namespace MyKanban.Wizards
{
	public class CreateSchemaWizard:Wizard<SqlCEDatabaseUpgrader>
	{

		public CreateSchemaWizard()
		{
			this.Data = new SqlCEDatabaseUpgrader( new Models.MyKanban());

			Pages.Add(new StaticMessageWizardPage("Welcome", "Welcome to schema creation Wizard !", "It seems it is the first time you are running this software. This process will guide you during creation of a new schema."));
			Pages.Add(new WorkerDatabaseWizardPage("Create schema", "Click on button bellow in order to create schema:", () => { return CreateSchema(); } ));
			Pages.Add(new StaticMessageWizardPage("End", "You have sucessfully terminated schema creation Wizard", "Click on finish button in order to quit."));
		}

		private async Task<bool> CreateSchema()
		{
			try
			{
				await Data.CreateSchemaAsync();
				return true;
			}
			catch
			{
				return false;
			}

			
		}


	}
}
