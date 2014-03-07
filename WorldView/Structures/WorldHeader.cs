namespace MoreTerra.Structures
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
	using MoreTerra;

    public struct WorldHeader
    {
        private int releaseNumber;
        private string name;
        private int id;
        private Rect worldCoords;
        private Point maxTiles;
		private byte moonType;
		private int[] treeX;
		private int[] treeStyle;
		private int[] caveBackX;
		private int[] caveBackStyle;
		private int iceBackStyle;
		private int jungleBackStyle;
		private int hellBackStyle;
        private Point spawnPoint;
        private double surfaceLevel;
        private double rockLayer;
        private double temporaryTime;
        private bool isDayTime;
        private int moonPhase;
        private bool isBloodMoon;
        private bool isEclipse;
        private Point dungeonPoint;
		private bool crimson; // What is this?
        private bool isBoss1Dead;
        private bool isBoss2Dead;
        private bool isBoss3Dead;
		private bool isQueenBeeDead;
        private bool isMechBoss1Dead;
		private bool isMechBoss2Dead;
		private bool isMechBoss3Dead;
		private bool isMechBossAnyDead;
		private bool isPlantBossDead;
		private bool isGolemBossDead;
		private bool isGoblinSaved;
		private bool isWizardSaved;
		private bool isMechanicSaved;
		private bool isGoblinArmyDefeated;
		private bool isClownDefeated;
        private bool isFrostDefeated;
        private bool isPiratesDefeated;
        private bool isShadowOrbSmashed;
        private bool isMeteorSpawned;
        private byte shadowOrbsSmashed;
		private int altarsDestroyed;
		private bool hardMode;
        private int invasionDelay;
        private int invasionSize;
        private int invasionType;
        private double invasionPointX;
		private bool isRaining;
		private int rainTime;
		private Single maxRain;
		private int[] oreTiers;
		private byte[] styles;
		private int cloudsActive;
		private short numClouds;
		private Single windSpeed;

		private String merchantsName; //0x11
		private String nursesName; //0x12
		private String armsDealersName; //0x13
		private String dryadsName; //0x14
		private String guidesName; //0x16
		private String clothiersName; //0x36
		private String demolitionistsName; //0x26
		private String tinkerersName; //0x6B
		private String wizardsName; //0x6C
		private String mechanicsName; //0x7C
		private String trufflesName; //160
		private String steampunkersName; // 178
		private String dyetradersName; // 207
		private String partygirlsName; // 208
		private String cyborgsName; // 209
		private String paintersName; // 227
		private String witchdoctorsName; // 228
		private String piratesName; // 229
        private String stylistName;

        public int[] sectionPointers;

        [CategoryAttribute("General"), ReadOnlyAttribute(true)]
        public int ReleaseNumber
        {
            get
            {
                return this.releaseNumber;
            }
            set
            {
                this.releaseNumber = value;
            }
        }

        [CategoryAttribute("General"), ReadOnlyAttribute(true)]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [CategoryAttribute("General"), ReadOnlyAttribute(true)]
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public Rect WorldCoords
        {
            get
            {
                return this.worldCoords;
            }
            set
            {
                this.worldCoords = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public Point MaxTiles
        {
            get
            {
                return this.maxTiles;
            }
            set
            {
				// For some very odd reason the Y coord on the map is stored first.
				// Maps are always longer than tall so we flip it if it's the other way around.
				if (value.X < value.Y)
				{
					int t = value.X;
					value.X = value.Y;
					value.Y = t;
				}
                this.maxTiles = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public byte MoonType
		{
			get
			{
				return this.moonType;
			}
			set
			{
				this.moonType = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int[] TreeX
		{
			get
			{
				return this.treeX;
			}
			set
			{
				this.treeX = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int[] TreeStyle
		{
			get
			{
				return this.treeStyle;
			}
			set
			{
				this.treeStyle = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int[] CaveBackX
		{
			get
			{
				return this.caveBackX;
			}
			set
			{
				this.caveBackX = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int[] CaveBackStyle
		{
			get
			{
				return this.caveBackStyle;
			}
			set
			{
				this.caveBackStyle = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int IceBackStyle
		{
			get
			{
				return this.iceBackStyle;
			}
			set
			{
				this.iceBackStyle = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int JungleBackStyle
		{
			get
			{
				return this.jungleBackStyle;
			}
			set
			{
				this.jungleBackStyle = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int HellBackStyle
		{
			get
			{
				return this.hellBackStyle;
			}
			set
			{
				this.hellBackStyle = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public Point SpawnPoint
        {
            get
            {
                return this.spawnPoint;
            }
            set
            {
                this.spawnPoint = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public double SurfaceLevel
        {
            get
            {
                return this.surfaceLevel;
            }
            set
            {
                this.surfaceLevel = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public double RockLayer
        {
            get
            {
                return this.rockLayer;
            }
            set
            {
                this.rockLayer = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public double TemporaryTime
        {
            get
            {
                return this.temporaryTime;
            }
            set
            {
                this.temporaryTime = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public bool IsDayTime
        {
            get
            {
                return this.isDayTime;
            }
            set
            {
                this.isDayTime = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public int MoonPhase
        {
            get
            {
                return this.moonPhase;
            }
            set
            {
                this.moonPhase = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public bool IsBloodMoon
        {
            get
            {
                return this.isBloodMoon;
            }
            set
            {
                this.isBloodMoon = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public bool IsEclipse
        {
            get
            {
                return this.isEclipse;
            }
            set
            {
                this.isEclipse = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
        public Point DungeonPoint
        {
            get
            {
                return this.dungeonPoint;
            }
            set
            {
                this.dungeonPoint = value;
            }
        }

        [CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public bool Crimson
        {
            get
            {
				return this.crimson;
            }
            set
            {
				this.crimson = value;
            }
        }

        [CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
        public bool IsBoss1Dead
        {
            get
            {
                return this.isBoss1Dead;
            }
            set
            {
                this.isBoss1Dead = value;
            }
        }

        [CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
        public bool IsBoss2Dead
        {
            get
            {
                return this.isBoss2Dead;
            }
            set
            {
                this.isBoss2Dead = value;
            }
        }

        [CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
        public bool IsBoss3Dead
        {
            get
            {
                return this.isBoss3Dead;
            }
            set
            {
                this.isBoss3Dead = value;
            }
        }

        [CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsQueenBeeDead
		{
			get
			{
				return this.isQueenBeeDead;
			}
			set
			{
				this.isQueenBeeDead = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
        public bool IsMechBoss1Dead
        {
            get
            {
                return this.isMechBoss1Dead;
            }
            set
            {
                this.isMechBoss1Dead = value;
            }
        }

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsMechBoss2Dead
		{
			get
			{
				return this.isMechBoss2Dead;
			}
			set
			{
				this.isMechBoss2Dead = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsMechBoss3Dead
		{
			get
			{
				return this.isMechBoss3Dead;
			}
			set
			{
				this.isMechBoss3Dead = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsMechBossAnyDead
		{
			get
			{
				return this.isMechBossAnyDead;
			}
			set
			{
				this.isMechBossAnyDead = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsPlantBossDead
		{
			get
			{
				return this.isPlantBossDead;
			}
			set
			{
				this.isPlantBossDead = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public bool IsGolemBossDead
		{
			get
			{
				return this.isGolemBossDead;
			}
			set
			{
				this.isGolemBossDead = value;
			}
		}

		[CategoryAttribute("NPC Information"), ReadOnlyAttribute(true)]
		public Boolean IsGoblinSaved
		{
			get
			{
				return this.isGoblinSaved;
			}
			set
			{
				this.isGoblinSaved = value;
			}
		}

		[CategoryAttribute("NPC Information"), ReadOnlyAttribute(true)]
		public Boolean IsWizardSaved
		{
			get
			{
				return this.isWizardSaved;
			}
			set
			{
				this.isWizardSaved = value;
			}
		}

		[CategoryAttribute("NPC Information"), ReadOnlyAttribute(true)]
		public Boolean IsMechanicSaved
		{
			get
			{
				return this.isMechanicSaved;
			}
			set
			{
				this.isMechanicSaved = value;
			}
		}

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public Boolean IsClownDefeated
		{
			get
			{
				return this.isClownDefeated;
			}
			set
			{
				this.isClownDefeated = value;
			}
		}

        [CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public Boolean IsFrostDefeated
        {
            get
            {
				return this.isFrostDefeated;
            }
            set
            {
				this.isFrostDefeated = value;
            }
        }

		[CategoryAttribute("Boss Information"), ReadOnlyAttribute(true)]
		public Boolean IsPiratesDefeated
		{
			get
			{
				return this.isPiratesDefeated;
			}
			set
			{
				this.isPiratesDefeated = value;
			}
		}

		[CategoryAttribute("Meteor Information"), ReadOnlyAttribute(true)]
        public bool IsShadowOrbSmashed
        {
            get
            {
                return this.isShadowOrbSmashed;
            }
            set
            {
                this.isShadowOrbSmashed = value;
            }
        }

        [CategoryAttribute("Meteor Information"), ReadOnlyAttribute(true)]
        public bool IsMeteorSpawned
        {
            get
            {
                return this.isMeteorSpawned;
            }
            set
            {
                this.isMeteorSpawned = value;
            }
        }

        [CategoryAttribute("Meteor Information"), ReadOnlyAttribute(true)]
        public byte ShadowOrbsSmashed
        {
            get
            {
                return this.shadowOrbsSmashed;
            }
            set
            {
                this.shadowOrbsSmashed = value;
            }
        }

		[CategoryAttribute("Hard Mode Information"), ReadOnlyAttribute(true)]
		public Int32 AltarsDestroyed
		{
			get
			{
				return this.altarsDestroyed;
			}
			set
			{
				this.altarsDestroyed = value;
			}
		}

		[CategoryAttribute("Hard Mode Information"), ReadOnlyAttribute(true)]
		public Boolean HardMode
		{
			get
			{
				return this.hardMode;
			}
			set
			{
				this.hardMode = value;
			}
		}

		[CategoryAttribute("Invasion Information"), ReadOnlyAttribute(true)]
        public int InvasionDelay
        {
            get
            {
                return this.invasionDelay;
            }
            set
            {
                this.invasionDelay = value;
            }
        }

        [CategoryAttribute("Invasion Information"), ReadOnlyAttribute(true)]
        public int InvasionSize
        {
            get
            {
                return this.invasionSize;
            }
            set
            {
                this.invasionSize = value;
            }
        }

        [CategoryAttribute("Invasion Information"), ReadOnlyAttribute(true)]
        public int InvasionType
        {
            get
            {
                return this.invasionType;
            }
            set
            {
                this.invasionType = value;
            }
        }

        [CategoryAttribute("Invasion Information"), ReadOnlyAttribute(true)]
        public double InvasionPointX
        {
            get
            {
                return this.invasionPointX;
            }
            set
            {
                this.invasionPointX = value;
            }
        }

		[CategoryAttribute("Invasion Information"), ReadOnlyAttribute(true)]
		public Boolean IsGoblinArmyDefeated
		{
			get
			{
				return isGoblinArmyDefeated;
			}
			set
			{
				isGoblinArmyDefeated = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public bool IsRaining
		{
			get
			{
				return this.isRaining;
			}
			set
			{
				this.isRaining = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public int RainTime
		{
			get
			{
				return this.rainTime;
			}
			set
			{
				this.rainTime = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public Single MaxRain
		{
			get
			{
				return this.maxRain;
			}
			set
			{
				this.maxRain = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public int[] OreTiers
		{
			get
			{
				return this.oreTiers;
			}
			set
			{
				this.oreTiers = value;
			}
		}

		[CategoryAttribute("World Information"), ReadOnlyAttribute(true)]
		public byte[] Styles
		{
			get
			{
				return this.styles;
			}
			set
			{
				this.styles = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public int CloudsActive
		{
			get
			{
				return this.cloudsActive;
			}
			set
			{
				this.cloudsActive = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public short NumClouds
		{
			get
			{
				return this.numClouds;
			}
			set
			{
				this.numClouds = value;
			}
		}

		[CategoryAttribute("Weather Information"), ReadOnlyAttribute(true)]
		public Single WindSpeed
		{
			get
			{
				return this.windSpeed;
			}
			set
			{
				this.windSpeed = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String MerchantsName
		{
			get {
				return merchantsName;
			}
			set {
				merchantsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String NursesName
		{
			get {
				return nursesName;
			}
			set {
				nursesName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String ArmsDealersName
		{
			get {
				return armsDealersName;
			}
			set {
				armsDealersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String DryadsName
		{
			get {
				return dryadsName;
			}
			set {
				dryadsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String GuidesName
		{
			get {
				return guidesName;
			}
			set {
				guidesName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String ClothiersName
		{
			get {
				return clothiersName;
			}
			set {
				clothiersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String DemolitionistsName
		{
			get {
				return demolitionistsName;
			}
			set {
				demolitionistsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String TinkerersName
		{
			get {
				return tinkerersName;
			}
			set {
				tinkerersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String WizardsName
		{
			get {
				return wizardsName;
			}
			set {
				wizardsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String MechanicsName
		{
			get
			{
				return mechanicsName;
			}
			set
			{
				mechanicsName = value;
			}
		}

        [CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String TrufflesName
		{
			get
			{
				return trufflesName;
			}
			set
			{
				trufflesName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String SteamPunkersName
		{
			get
			{
				return steampunkersName;
			}
			set
			{
				steampunkersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String DyeTradersName
		{
			get
			{
				return dyetradersName;
			}
			set
			{
				dyetradersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String PartyGirlsName
		{
			get
			{
				return partygirlsName;
			}
			set
			{
				partygirlsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String CyborgsName
		{
			get
			{
				return cyborgsName;
			}
			set
			{
				cyborgsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String PaintersName
		{
			get
			{
				return paintersName;
			}
			set
			{
				paintersName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String WitchDoctorsName
		{
			get
			{
				return witchdoctorsName;
			}
			set
			{
				witchdoctorsName = value;
			}
		}

		[CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
		public String PiratesName
		{
			get
			{
				return piratesName;
			}
			set
			{
				piratesName = value;
			}
		}

        [CategoryAttribute("NPC Names"), ReadOnlyAttribute(true)]
        public String StylistName
        {
            get
            {
                return stylistName;
            }
            set
            {
                stylistName = value;
            }
        }


	}
}
