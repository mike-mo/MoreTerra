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
        private Point spawnPoint;
        private double surfaceLevel;
        private double rockLayer;
        private double temporaryTime;
        private bool isDayTime;
        private int moonPhase;
        private int moonType;
        private bool isBloodMoon;
        private Point dungeonPoint;
        private bool isBoss1Dead;
        private bool isBoss2Dead;
        private bool isBoss3Dead;
        private bool isMechBoss1Dead;
		private bool isGoblinSaved;
		private bool isWizardSaved;
		private bool isMechanicSaved;
		private bool isGoblinArmyDefeated;
        private bool isFrostDefeated;
        private bool isPiratesDefeated;
		private bool isClownDefeated;
        private bool isShadowOrbSmashed;
        private bool isMeteorSpawned;
        private byte shadowOrbsSmashed;
		private int altarsDestroyed;
		private bool hardMode;
        private int invasionDelay;
        private int invasionSize;
        private int invasionType;
        private double invasionPointX;

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
        public int MoonType
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
			get {
				return mechanicsName;
			}
			set {
				mechanicsName = value;
			}
		}


	}
}
