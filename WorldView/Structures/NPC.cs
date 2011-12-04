using System;
using System.Drawing;

namespace MoreTerra.Structures
{
	public enum NPCType
	{
		Armsdealer = 0,
		Clothier,
		Demolitionist,
		Dryad,
		Guide,
		Mechanic = 5,
		Merchant,
		Nurse,
		Oldman,
		Tinkerer,
		Wizard = 10,
		Unknown = 11
	}
		
	public class NPC
	{
		private Int32 npcId;
		private Boolean activeNpc;
		private String npcName;
		private PointSingle npcPosition;

		private Boolean isNpcHomeless;
		private Point npcHomeTile;
		private NPCType npcType;

		#region Constructors
		public NPC()
		{
			npcId = -1;
			activeNpc = false;
			npcName = String.Empty;
			npcPosition = new PointSingle(0, 0);
			isNpcHomeless = false;
			npcHomeTile = new Point(0, 0);
			npcType = NPCType.Unknown;
		}

		public NPC(Int32 id, Boolean active, String name, PointSingle pos, Boolean homeless, Point home)
		{
			npcId = id;
			activeNpc = active;
			npcName = name;
			isNpcHomeless = homeless;
			npcHomeTile = home;
			npcPosition = pos;
			npcType = NPCType.Unknown;
		}
		#endregion

		#region GetSet Functions
		public Int32 Id
		{
			get
			{
				return npcId;
			}
			set
			{
				npcId = value;
			}
		}

		public Boolean Homeless
		{
			get
			{
				return isNpcHomeless;
			}
			set
			{
				isNpcHomeless = value;
			}
		}
			
		public PointSingle Position
		{
			get
			{
				return npcPosition;
			}
			set
			{
				npcPosition = value;
			}
		}

		public Point HomeTile
		{
			get
			{
				return npcHomeTile;
			}
			set
			{
				npcHomeTile = value;
			}
		}

		public Boolean Active
		{
			get
			{
				return activeNpc;
		}
			set
			{
				activeNpc = value;
			}
		}

		public String Name
		{
			get
			{
				return npcName;
			}
			set
			{
				npcName = value;
			}
		}

		public NPCType Type
		{
			get
			{
				return npcType;
			}
			set
			{
				npcType = value;
			}
		}
		#endregion

	}
}
