using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TermTracker.Model;

namespace TermTracker
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{
				var info = con.GetTableInfo("Course");
				if (!info.Any())
				{
					Course course1 = new Course()
					{
						courseTitle = "Test Course 1",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Performance Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course2 = new Course()
					{
						courseTitle = "Test Course 2",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Performance Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course3 = new Course()
					{
						courseTitle = "Test Course 3",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Performance Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course4 = new Course()
					{
						courseTitle = "Test Course 4",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Performance Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course5 = new Course()
					{
						courseTitle = "Test Course 5",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Performance Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course6 = new Course()
					{
						courseTitle = "Test Course 6",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 1,
						performanceAssessment = true,
						objectiveAssessment = false,
						pAssessmentNotes = "Objective Assessment Notes",
						pAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						pAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course7 = new Course()
					{
						courseTitle = "Test Course 7",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = false,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course8 = new Course()
					{
						courseTitle = "Test Course 8",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = false,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course9 = new Course()
					{
						courseTitle = "Test Course 9",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = false,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course10 = new Course()
					{
						courseTitle = "Test Course 10",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = false,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course11 = new Course()
					{
						courseTitle = "Test Course 11",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = false,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
					};
					Course course12 = new Course()
					{
						courseTitle = "Test Course 12",
						optionalNotes = "These are optional notes",
						courseInfo = "Course Info goes here",
						startDate = DateTime.Parse("1/1/2022"),
						endDate = DateTime.Parse("12/31/2022"),
						status = 1,
						ci_Name = "Bridger Winter",
						ci_Phone = "509-552-3399",
						ci_Email = "bridgerwinter@gmail.com",
						term = 2,
						performanceAssessment = true,
						objectiveAssessment = true,
						oAssessmentNotes = "Objective Assessment Notes",
						oAssessmentStart = DateTime.Parse("12 / 31 / 2022"),
						oAssessmentEnd = DateTime.Parse("12/31/2022"),
						pAssessmentNotes = "Performance Assessment Notes"
					};
					Term term1 = new Term()
					{
						termVal = 1,
						termStart = DateTime.Parse("01/01/2000"),
						termEnd = DateTime.Parse("05/31/2000"),
						termTitle = "Fall Term"
					};
					Term term2 = new Term()
					{
						termVal = 2,
						termStart = DateTime.Parse("06/01/2000"),
						termEnd = DateTime.Parse("12/31/2000"),
						termTitle = "Spring Term"

					};

					con.CreateTable<Course>();
					con.CreateTable<Term>();

					con.Insert(course1);
					con.Insert(course2);
					con.Insert(course3);
					con.Insert(course4);
					con.Insert(course5);
					con.Insert(course6);
					con.Insert(course7);
					con.Insert(course8);
					con.Insert(course9);
					con.Insert(course10);
					con.Insert(course11);
					con.Insert(course12);
					con.Insert(term1);
					con.Insert(term2);
				}
			}
			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{ 
				con.CreateTable<Course>();
				con.CreateTable<Term>();

				var courses = con.Table<Course>().ToList();
				termView.ItemsSource = courses;

				var terms = con.Table<Term>().ToList();
				semesters.ItemsSource = terms;
				//startTerm1.Text = terms[0].termStart.ToString();
				//endTerm1.Text = terms[0].termEnd.ToString();
				//startTerm2.Text = terms[1].termStart.ToString();
				//endTerm2.Text = terms[1].termEnd.ToString();
			}
		}
		private void termView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedPost = termView.SelectedItem as Course;
			if (selectedPost != null)
			{
				Navigation.PushAsync(new modifyCourse(selectedPost));
			}
		}	

		private void addTerm_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AddTerm());
		}

		private void addCourse_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AddCourse());
		}

		private void semesters_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selected = semesters.SelectedItem as Term;
			if (selected != null)
			{
				Navigation.PushAsync(new ViewTerm(selected.termVal));
			}
		}
	}
}
