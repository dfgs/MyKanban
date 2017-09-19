using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MyKanban.Wizards
{
	/// <summary>
	/// Interaction logic for CreateDatabaseWizardWindow.xaml
	/// </summary>
	public partial class DatabaseWizardWindow
	{
		public DatabaseWizardWindow()
		{
			InitializeComponent();
		}

		private void WizardControl_WizardFinished(object sender, EventArgs e)
		{
			DialogResult = true;
		}
	}
}
