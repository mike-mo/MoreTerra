using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Utilities
{
	class pTimeSpan
	{
		public DateTime Start;
		public DateTime End;
		public String Note;

		public pTimeSpan(DateTime newDateTime, String newNote)
		{
			Start = newDateTime;
			End = new DateTime(0);

			if (String.IsNullOrEmpty(newNote))
				Note = newNote;
			else
				Note = "";
		}

		public TimeSpan Elapsed()
		{
			if (End.CompareTo(Start) < 0)
				return new TimeSpan(0);

			return End - Start;
		}
	}

}
