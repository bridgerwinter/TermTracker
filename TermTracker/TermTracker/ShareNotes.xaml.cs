using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TermTracker.Model;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TermTracker.Model
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShareNotes : ContentPage
	{
		Course selectedCourse;
		public ShareNotes(Course passedCourse)
		{
			InitializeComponent();
			selectedCourse = passedCourse;
		}

		private void sendNotesButton_Clicked(object sender, EventArgs e)
		{
			if (IsValidEmail(toAddress.Text))
			{
				string body = "";

				if (checkboxCourseInfo.IsChecked)
				{
					string courseInfo =
				"Course Title: " + selectedCourse.courseTitle + Environment.NewLine +
				"Course Info: " + selectedCourse.courseInfo + Environment.NewLine +
				"Optional Notes: " + selectedCourse.optionalNotes + Environment.NewLine +
				"Course Start Date: " + selectedCourse.startDate.ToString() + Environment.NewLine +
				"Course End Date: " + selectedCourse.endDate.ToString() + Environment.NewLine +
				"Term: " + (selectedCourse.term).ToString() + Environment.NewLine + Environment.NewLine;

					body = body + courseInfo;
				}

				if (checkboxAssessmentInfo.IsChecked)
				{
					string assessmentInfo = "";

					if (selectedCourse.objectiveAssessment)
					{
						assessmentInfo = assessmentInfo +
							"Objective Assessment: True" + Environment.NewLine +
							"Objective Assessment Start Date: " + selectedCourse.oAssessmentStart.ToString() + Environment.NewLine +
							"Objective Assessment End Date: " + selectedCourse.oAssessmentEnd.ToString() + Environment.NewLine +
							"Objective Assessment Notes: " + selectedCourse.oAssessmentNotes + Environment.NewLine + Environment.NewLine;
					}
					if (selectedCourse.performanceAssessment)
					{
						assessmentInfo = assessmentInfo +
							"Performance Assessment : True" + Environment.NewLine +
							"Performance Assessment Start Date: " + selectedCourse.pAssessmentStart.ToString() + Environment.NewLine +
							"Performance Assessment End Date: " + selectedCourse.pAssessmentEnd.ToString() + Environment.NewLine +
							"Performance Assessment Notes: " + selectedCourse.pAssessmentNotes.ToString() + Environment.NewLine + Environment.NewLine;
					}
					body = body + assessmentInfo;
				}
				if (checkboxCourseInstructor.IsChecked)
				{
					string courseInstructorInfo =
						"Course Instructor Name: " + selectedCourse.ci_Name + Environment.NewLine +
						"Course Instructor Phone: " + selectedCourse.ci_Phone + Environment.NewLine +
						"Course Instructor Email: " + selectedCourse.ci_Email + Environment.NewLine + Environment.NewLine;

					body = body + courseInstructorInfo;

				}
				List<string> result = toAddress.Text.Split(',').ToList();
				//https://stackoverflow.com/questions/22629951/suppressing-warning-cs4014-because-this-call-is-not-awaited-execution-of-the
				_ = Task.Run(() => SendEmail("Course Information", body, result).ConfigureAwait(false));
			}
			else
			{
				DisplayAlert("Invalid Email", "Email address is not in a valid format, please fix.", "Ok");
			}
		}
		public async Task SendEmail(string subject, string body, List<string> recipients)
		{
			try
			{
				var message = new EmailMessage
				{
					Subject = subject,
					Body = body,
					To = recipients,
					//Cc = ccRecipients,
					//Bcc = bccRecipients
				};
				await Email.ComposeAsync(message);
			}
			catch (FeatureNotSupportedException fbsEx)
			{
				// Email is not supported on this device
			}
			catch (Exception ex)
			{
				// Some other exception occurred
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