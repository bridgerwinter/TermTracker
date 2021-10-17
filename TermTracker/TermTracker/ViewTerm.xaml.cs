using Plugin.LocalNotifications;
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
	public partial class ViewTerm : ContentPage
	{
		int gSelectedTerm;
		Term selectedTerm;
		//List<Course> filteredCourses = new List<Course>();
		public ViewTerm(int selectedTerm)
		{
			InitializeComponent();
			gSelectedTerm = selectedTerm;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{
				con.CreateTable<Course>();
				//con.Insert(course);
				var courses = con.Table<Course>().ToList(); //FindAll(term==gSelectedTerm);
				courses = courses.FindAll(e => e.term == gSelectedTerm);
				var terms = con.Table<Term>().ToList();
				terms = terms.FindAll(e => e.termVal == gSelectedTerm);
				selectedTerm = terms[0];
				//termView.ItemsSource = courses;
				generalView.ItemsSource = courses;
			}
		}
		private void buttonEditTerm_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChangeTerm(selectedTerm));

		}

		private void buttonDeleteTerm_Clicked_1(object sender, EventArgs e)
		{
			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{
				con.CreateTable<Course>();
				int rows = con.Delete(selectedTerm);
				if (rows > 0)
				{
					DisplayAlert("Success", "Succesfully updated", "Ok");
					Navigation.PopAsync();
				}
				else
				{
					DisplayAlert("Failure", "Failure to update", "Ok");
					Navigation.PopAsync();
				}
			}
		}

		private void generalView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedPost = generalView.SelectedItem as Course;
			if (selectedPost != null)
			{
				Navigation.PushAsync(new modifyCourse(selectedPost));
			}
		}

		private void alertTermStart_Clicked(object sender, EventArgs e)
		{
			TimeSpan ts1 = selectedTerm.termStart.Date - DateTime.Now;
			double seconds = ts1.TotalSeconds;
			CrossLocalNotifications.Current.Show("Term Starts Soon", "There's a term starting soon.", 01, DateTime.Now.AddSeconds(seconds));
			DisplayAlert("Alert Created", "Alert successfully created for term start.", "Ok");
		}

		private void alertTermEnd_Clicked(object sender, EventArgs e)
		{
			TimeSpan ts1 = selectedTerm.termEnd.Date - DateTime.Now;
			double seconds = ts1.TotalSeconds;
			CrossLocalNotifications.Current.Show("Term Ends Soon", "There's a term ending soon.", 02, DateTime.Now.AddSeconds(seconds));
			DisplayAlert("Alert Created", "Alert successfully created for term end.", "Ok");

		}
	}
}