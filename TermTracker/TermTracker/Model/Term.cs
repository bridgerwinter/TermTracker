using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TermTracker.Model
{
	public class Term
	{
		//[PrimaryKey, AutoIncrement] public int termId { get; set; }
		[PrimaryKey] public int termVal { get; set; }
		public string termTitle { get; set; }
		public DateTime termStart { get; set; }
		public DateTime termEnd { get; set; } 
	}
}
