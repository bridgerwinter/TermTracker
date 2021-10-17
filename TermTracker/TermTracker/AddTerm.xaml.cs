using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Model
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTerm : ContentPage
	{
		public AddTerm()
		{
			InitializeComponent();
		}

		private void ButtonAddTerm_Clicked(object sender, EventArgs e)
		{
			bool isNumber = int.TryParse(Term.Text, out int termNumber);
			bool isError = false;
			if (!isNumber || termNumber == 0)
			{
				isError = true;
			}
			if(termDateStart.Date > termDateEnd.Date)
			{
				isError = true;
			}
			if (termTitle == null)
			{
				isError = true;
			}
			if(!isError)
			{
				Term term = new Term()
				{
					termVal = termNumber,
					termStart = termDateStart.Date,
					termEnd = termDateEnd.Date,
					termTitle = termTitle.Text
				};

				using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
				{
					try
					{
						con.Insert(term);
						DisplayAlert("Term Added", "Term Successfully Added", "Ok");
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