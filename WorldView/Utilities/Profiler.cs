using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Utilities
{
	public class Profiler
	{
		private Dictionary<Int32, String> openIDs;
		private Dictionary<String, Dictionary<Int32, pTimeSpan>> profileList;
		private Int32 currentID;

		public Profiler()
		{
			openIDs = new Dictionary<Int32, String>();
			profileList = new Dictionary<String, Dictionary<Int32, pTimeSpan>>();
			currentID = 0;
		}

		public void Clear()
		{
			openIDs.Clear();
			profileList.Clear();
			currentID = 0;
		}

		public Int32 Start(String pName, String useNote = null)
		{
			pTimeSpan pts = new pTimeSpan(DateTime.Now, useNote);

			openIDs.Add(currentID, pName);

			if (profileList.ContainsKey(pName) == false)
				profileList[pName] = new Dictionary<Int32, pTimeSpan>();

			profileList[pName].Add(currentID, pts);

			return ++currentID;
		}

		public void Stop(Int32 id)
		{
			String pName;
			Dictionary<Int32, pTimeSpan> pList;

			if (openIDs.ContainsKey(id) == false)
				//              Throw exception.
				return;

			pName = openIDs[id];

			if (profileList.ContainsKey(pName) == false)
				//				Throw exception.
				return;

			pList = profileList[pName];

			if (pList.ContainsKey(id) == false)
				//				Throw exception.
				return;

			pList[id].End = DateTime.Now;

			openIDs.Remove(id);
		}

		public void Stop(String pName)
		{
			Int32 lastID = -1;

			// We really should walk the tree backwards.
			foreach (KeyValuePair<Int32, String> kvp in openIDs)
			{
				if (kvp.Value == pName)
					lastID = kvp.Key;
			}

			if (lastID == -1)
				// Throw Exception.
				return;


			Stop(lastID);
		}

		public TimeSpan Total(String pName)
		{
			TimeSpan t = new TimeSpan(0);
			Dictionary<Int32, pTimeSpan> pList;

			if (profileList.ContainsKey(pName) == false)
				// Throw exception.
				return t;

			pList = profileList[pName];

			foreach (KeyValuePair<Int32, pTimeSpan> kvp in pList)
			{
				t += kvp.Value.Elapsed();
			}

			return t;
		}

		public override string ToString()
		{
			String returnVal = "";

			foreach (KeyValuePair<String, Dictionary<Int32, pTimeSpan>> kvp in profileList)
			{
				returnVal = returnVal + kvp.Key + " : " + Total(kvp.Key).ToString() + Environment.NewLine;
			}

			return returnVal;
		}
	}
}
