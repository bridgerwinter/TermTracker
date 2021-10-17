using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SQLite;
using Plugin.LocalNotifications;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TermTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCourse : ContentPage
	{
		public AddCourse()
		{
			InitializeComponent();
		}

		private void addButton_Clicked(object sender, EventArgs e)
		{
			string[] errorList = {};
			bool isError = false;
			if (Start_Date.Date > End_Date.Date)
			{
				string error = "Start Date is greater than End Date.";
				isError = true;
				errorList =  errorList = errorList.Append(error).ToArray();
			}
			if (Course_Title.Text == null)
			{
				string error = "Course Title is null, please select a value.";
				isError = true;
				errorList = errorList = errorList.Append(error).ToArray();

			}
			if (!IsValidEmail(CI_Email.Text))
			{
				string error = "Email address is not in a valid format";
				isError = true;
				errorList = errorList.Append(error).ToArray();
			}
			if (Course_Info.Text == null)
			{
				string error = "Course Info is null, please select a value.";
				isError = true;
				errorList = errorList = errorList.Append(error).ToArray();

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
					errorList.Append(error2);

				}
				if (oNotes.Text == null)
				{
					string error2 = "Objective Assessment Notes is null, please select a value.";
					isError = true;
					errorList.Append(error2);

				}
			}


			if (!isError)
			{
				Course course = new Course()
				{
					courseTitle = Course_Title.Text,
					optionalNotes = Optional_Notes.Text,
					courseInfo = Course_Info.Text,
					startDate = Start_Date.Date,
					endDate = End_Date.Date,
					status = Status.SelectedIndex + 1,
					performanceAssessment = pAssessmentBox.IsChecked,
					pAssessmentStart = pStart.Date,
					pAssessmentEnd = pEnd.Date,
					pAssessmentNotes = pNotes.Text,
					objectiveAssessment = oAssessmentBox.IsChecked,
					oAssessmentStart = oStart.Date,
					oAssessmentEnd = oEnd.Date,
					oAssessmentNotes = oNotes.Text,
					ci_Name = CI_Name.Text,
					ci_Phone = CI_Phone.Text,
					ci_Email = CI_Email.Text,
					term = int.Parse(Term.Text)
				};
				using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
				{
					con.CreateTable<Course>();
					con.Insert(course);
					Navigation.PopAsync();
					//var courses = con.Table<Course>().ToList();
					//termView.ItemsSource = courses;
				}
			}
			else
			{
				string result = String.Join(Environment.NewLine, errorList);
				DisplayAlert("Errors Found", result, "Ok");
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

	}
}