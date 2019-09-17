using System;
using System.Drawing;

namespace MoreTerra.Structures
{
	public enum NPCType
	{
        None = 0,
        Merchant = 17,
        Nurse = 18,
        ArmsDealer = 19,
        Dryad = 20,
        Skeleton = 21,
        Guide = 22,
        OldMan = 37,
        Demolitionist = 38,
        Clothier = 54,
        GoblinTinkerer = 107,
		Wizard = 108,
		Mechanic = 124,
        SantaClaus = 142,
        Truffle = 160,
        Steampunker =178,
		DyeTrader = 207,
        PartyGirl = 208,
        Cyborg = 209,
        Painter = 227,
        WitchDoctor = 228,
        Pirate = 229,
        Stylist = 353,
        TravelingMerchant = 368,
        Angler = 369,
        TaxCollector = 441,
        Tavernkeep = 550,
        Unknown
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
