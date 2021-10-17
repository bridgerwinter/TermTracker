using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TermTracker
{
	public class Course
	{
		[PrimaryKey, AutoIncrement] public int Id { get; set; }
		[MaxLength(250)] public string courseTitle { get; set; }
		[MaxLength(250)] public string optionalNotes { get; set; }
		[MaxLength(250)] public string courseInfo { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
		public int status { get; set; } //0,1,2,3

		[MaxLength(250)] public string ci_Name { get; set; }
		[MaxLength(250)] public string ci_Phone { get; set; }
		[MaxLength(250)] public string ci_Email { get; set; }
		public int term { get; set; }
		public bool performanceAssessment { get; set; }
		public bool objectiveAssessment { get; set; }

		[MaxLength(250)] public string pAssessmentNotes { get; set; }
		[MaxLength(250)] public string oAssessmentNotes { get; set; }
		public DateTime pAssessmentStart { get; set; }
		public DateTime pAssessmentEnd { get; set; }
		public DateTime oAssessmentStart { get; set; }
		public DateTime oAssessmentEnd { get; set; }

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

	}
}
