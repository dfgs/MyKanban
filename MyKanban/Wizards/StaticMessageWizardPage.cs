using SqlCEDatabaseUpgraderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardLib;


namespace MyKanban.Wizards
{
	public class StaticMessageWizardPage : WizardPage<SqlCEDatabaseUpgrader>
	{
		private string header;
		public override string Header
		{
			get { return header; }
		}
		private string title;
		public string Title
		{
			get { return title; }
		}
		private string body;
		public string Body
		{
			get { return body; }
		}


		public StaticMessageWizardPage(string Header,string Title,string Body)
		{
			this.header = Header;this.title = Title;this.body = Body;
		}
		public override bool OnCanGoNext()
		{
			return true;
		}

		public override bool OnCanGoPrevious()
		{
			return true;
		}

	}
}
