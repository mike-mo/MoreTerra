using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MoreTerra.Structures
{
	public class NPC
	{
		public Int32 npcId;
		public Boolean activeNpc;
		public String npcName;

		// this is a hack.  The actual position is stored as a Single of all things.
		// not sure why we need to know the NPC position down to 8 decimal places.
		// We're not writing NPC's back to the file so the loss of precision shouldn't
		// be any issue.  Just documenting in case things change.
		public Point npcPosition;

		public Boolean isNpcHomeless;
		public Point npcHomeTile;


		public NPC()
		{
			npcId = -1;
			activeNpc = false;
			npcName = String.Empty;
			npcPosition = new Point(0, 0);
			isNpcHomeless = false;
			npcHomeTile = new Point(0, 0);
		}

		public NPC(Int32 id, Boolean active, String name, Point pos, Boolean homeless, Point home)
		{
			npcId = id;
			activeNpc = active;
			npcName = name;
			isNpcHomeless = homeless;
			npcHomeTile = home;

			
			npcPosition = new Point((Int32) (pos.X/16), (Int32) (pos.Y/16));

		}
	}
}
