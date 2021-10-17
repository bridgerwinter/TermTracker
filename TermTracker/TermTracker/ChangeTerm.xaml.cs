using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeTerm : ContentPage
	{
		Term selectedTerm;
		public ChangeTerm(Term passedTerm)
		{
			InitializeComponent();
			selectedTerm = passedTerm;
			termDateStart.Date = passedTerm.termStart;
			termDateEnd.Date = passedTerm.termEnd;
			Term.Text = passedTerm.termVal.ToString();
			termTitle.Text = passedTerm.termTitle.ToString();
		}

		private void buttonChangeTerm_Clicked(object sender, EventArgs e)
		{
			bool isNumber = int.TryParse(Term.Text, out int termNumber);
			bool isError = false;
			
			if (!isNumber || termNumber == 0)
			{
				isError = true;
			}
			
			if (termDateStart.Date > termDateEnd.Date)
			{
				isError = true;
			}
			if (termTitle == null)
			{
				isError = true;
			}
			if (!isError)
			{

				selectedTerm.termStart = termDateStart.Date;
				selectedTerm.termEnd = termDateEnd.Date;
				selectedTerm.termTitle = termTitle.Text;
				selectedTerm.termVal = termNumber;
				
				using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
				{
					try
					{
						con.Update(selectedTerm);
						DisplayAlert("Term Updated", "Term Successfully Updated", "Ok");
						Navigation.PopAsync();
					}
					catch (Exception)
					{
						DisplayAlert("Duplicate Term", "Duplicate term please try again", "Ok");
					}
				}
			}
			else
			{
				DisplayAlert("Error", "Error with the date, term number, or term title. Please make sure all fields are filled out.", "Ok");
			}
		}
	}
}