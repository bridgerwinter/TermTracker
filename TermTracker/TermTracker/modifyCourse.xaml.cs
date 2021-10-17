using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Model;
using TermTracker;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TermTracker.Model
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class modifyCourse : ContentPage
	{
		Course selectedCourse;
		public modifyCourse(Course selectedCourse)
		{
			InitializeComponent();
			this.selectedCourse = selectedCourse;
			Course_Title.Text = selectedCourse.courseTitle;
			Optional_Notes.Text = selectedCourse.optionalNotes;
			Course_Info.Text = selectedCourse.courseInfo;
			Start_Date.Date = selectedCourse.startDate;
			End_Date.Date = selectedCourse.endDate;
			Status.SelectedIndex = selectedCourse.status - 1;

			pAssessmentBox.IsChecked = selectedCourse.performanceAssessment;
			pStart.Date = selectedCourse.pAssessmentStart;
			pEnd.Date = selectedCourse.pAssessmentEnd;
			pNotes.Text = selectedCourse.pAssessmentNotes;

			oAssessmentBox.IsChecked = selectedCourse.objectiveAssessment;
			oStart.Date = selectedCourse.oAssessmentStart;
			oEnd.Date = selectedCourse.oAssessmentEnd;
			oNotes.Text = selectedCourse.oAssessmentNotes;

			CI_Name.Text = selectedCourse.ci_Name;
			CI_Phone.Text = selectedCourse.ci_Phone;
			CI_Email.Text = selectedCourse.ci_Email;
			Term.Text = selectedCourse.term.ToString();
		}

		private void updateButton_Clicked(object sender, EventArgs e)
		{
			string[] errorList = { };
			bool isError = false;
			if (Start_Date.Date > End_Date.Date)
			{
				string error = "Start Date is greater than End Date.";
				isError = true;
				errorList = errorList.Append(error).ToArray();
			}
			if (!IsValidEmail(CI_Email.Text))
			{
				string error = "Email address is not in a valid format";
				isError = true;
				errorList = errorList.Append(error).ToArray();
			}
			if (Course_Title.Text == null)
			{
				string error = "Course Title is null, please select a value.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (Course_Info.Text == null)
			{
				string error = "Course Info is null, please select a value.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (Status.SelectedIndex == -1) //look into this
			{
				string error = "Status is null, please select a value";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (CI_Name.Text == null)
			{
				string error = "Course Instructor name is null, please select a value.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (CI_Phone.Text == null)
			{
				string error = "Course Instructor phone number is null, please select a value.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (CI_Email.Text == null)
			{
				string error = "Course Instructor email is null, please select a value.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (!int.TryParse(Term.Text, out int i))
			{
				string error = "Term is not a valid number.";
				isError = true;
				errorList = errorList.Append(error).ToArray();

			}
			if (pAssessmentBox.IsChecked)
			{
				if (pStart.Date > pEnd.Date)
				{
					string error = "Performance Assessment Start Date is greater than its End Date.";
					isError = true;
					errorList = errorList.Append(error).ToArray();

				}
				if (pNotes.Text == null)
				{
					string error = "Performance Assessment Notes is null, please select a value.";
					isError = true;
					errorList = errorList.Append(error).ToArray();

				}
			}
			if (oAssessmentBox.IsChecked)
			{
				if (oStart.Date > oEnd.Date)
				{
					string error2 = "Objective Assessment Start Date is greater than its End Date.";
					isError = true;
					errorList = errorList.Append(error2).ToArray();

				}
				if (oNotes.Text == null)
				{
					string error2 = "Objective Assessment Notes is null, please select a value.";
					isError = true;
					errorList = errorList.Append(error2).ToArray();

				}
			}
			if (!isError)
			{
				selectedCourse.courseTitle = Course_Title.Text;
				selectedCourse.optionalNotes = Optional_Notes.Text;
				selectedCourse.courseInfo = Course_Info.Text;
				selectedCourse.startDate = Start_Date.Date;
				selectedCourse.endDate = End_Date.Date;
				selectedCourse.status = Status.SelectedIndex + 1;
				selectedCourse.performanceAssessment = pAssessmentBox.IsChecked;
				selectedCourse.pAssessmentStart = pStart.Date;
				selectedCourse.pAssessmentEnd = pEnd.Date;
				selectedCourse.pAssessmentNotes = pNotes.Text;
				selectedCourse.objectiveAssessment = oAssessmentBox.IsChecked;
				selectedCourse.oAssessmentStart = oStart.Date;
				selectedCourse.oAssessmentEnd = oEnd.Date;
				selectedCourse.oAssessmentNotes = oNotes.Text;
				selectedCourse.ci_Name = CI_Name.Text;
				selectedCourse.ci_Phone = CI_Phone.Text;
				selectedCourse.ci_Email = CI_Email.Text;
				selectedCourse.term = int.Parse(Term.Text);

				using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
				{
					con.CreateTable<Course>();
					int rows = con.Update(selectedCourse);
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
			else
			{
				string result = String.Join(Environment.NewLine, errorList);
				DisplayAlert("Errors Found", result, "Ok");
			}

		}

		private void deleteButton_Clicked(object sender, EventArgs e)
		{
			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{
				con.CreateTable<Course>();
				int rows = con.Delete(selectedCourse);
				if (rows > 0)
				{
					DisplayAlert("Success", "Succesfully deleted", "Ok");
					Navigation.PopAsync();
				}
				else
				{
					DisplayAlert("Failure", "Failure to Delete", "Ok");
					Navigation.PopAsync();
				}
			}
		}

		private void pAssessmentBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			if (pAssessmentBox.IsChecked)
			{
				pStartLabel.IsVisible = true;
				pStart.IsVisible = true;
				pEndLabel.IsVisible = true;
				pEnd.IsVisible = true;
				pNotes.IsVisible = true;
			}
			else
			{
				pStartLabel.IsVisible = false;
				pStart.IsVisible = false;
				pEndLabel.IsVisible = false;
				pEnd.IsVisible = false;
				pNotes.IsVisible = false;
			}
		}

		private void oAssessmentBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			if (oAssessmentBox.IsChecked)
			{
				oStartLabel.IsVisible = true;
				oStart.IsVisible = true;
				oEndLabel.IsVisible = true;
				oEnd.IsVisible = true;
				oNotes.IsVisible = true;
			}
			else
			{
				oStartLabel.IsVisible = false;
				oStart.IsVisible = false;
				oEndLabel.IsVisible = false;
				oEnd.IsVisible = false;
				oNotes.IsVisible = false;
			}
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ShareNotes(selectedCourse));
		}

		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				// Normalize the domain
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				string DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();

					// Pull out and process domain name (throws ArgumentException on invalid)
					string domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException e)
			{
				return false;
			}
			catch (ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		private void Start_Date_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void End_Date_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void setAlerts_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SetAlerts(selectedCourse));
		}
	}
}