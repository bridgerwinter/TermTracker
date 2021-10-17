using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetAlerts : ContentPage
	{
		Course selectedCourse;
		public SetAlerts(Course passedCourse)
		{
			InitializeComponent();
			selectedCourse = passedCourse;
			if (selectedCourse.performanceAssessment == true)
			{
				labelPerformanceAssessment.IsVisible = true;
				checkboxPerformanceAssessment.IsVisible = true;
			}
			if (selectedCourse.objectiveAssessment == true)
			{
				labelObjectiveAssessment.IsVisible = true;
				checkboxObjectiveAssessment.IsVisible = true;
			}
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			int totalAlerts = 0;
			if (checkboxStartCourseAlert.IsChecked)
			{
				TimeSpan ts1 = selectedCourse.startDate.Date - DateTime.Now;
				double seconds = ts1.TotalSeconds;
				CrossLocalNotifications.Current.Show("Course Starts Soon", "There's a Course Coming up", 03, DateTime.Now.AddSeconds(seconds));
				totalAlerts = totalAlerts + 1;
			}
			if (checkboxEndCourseAlert.IsChecked)
			{
				TimeSpan ts1 = selectedCourse.endDate.Date - DateTime.Now;
				double seconds = ts1.TotalSeconds;
				CrossLocalNotifications.Current.Show("Course Ends Soon", "There's a course ending", 04, DateTime.Now.AddSeconds(seconds));
				totalAlerts = totalAlerts + 1;

			}
			if (checkboxObjectiveAssessment.IsChecked)
			{
				TimeSpan ts1 = selectedCourse.oAssessmentStart.Date - DateTime.Now;
				double seconds = ts1.TotalSeconds;
				CrossLocalNotifications.Current.Show("Objective Exam Starts Soon", "There's an objective assessment coming up", 05, DateTime.Now.AddSeconds(seconds));
				totalAlerts = totalAlerts + 1;

			}
			if (checkboxPerformanceAssessment.IsChecked)
			{
				TimeSpan ts1 = selectedCourse.pAssessmentStart.Date - DateTime.Now;
				double seconds = ts1.TotalSeconds;
				CrossLocalNotifications.Current.Show("Performance Assessment Starts Soon", "There's a performance assessment coming up", 06, DateTime.Now.AddSeconds(seconds));
				totalAlerts = totalAlerts + 1;

			}
			string body = totalAlerts.ToString() + " total alerts set.";
			DisplayAlert("Alerts Set", body, "Ok");
			Navigation.PopAsync();
		}
	}
}