namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.ComponentModel;

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
        private bool isBloodMoon;
        private Point dungeonPoint;
        private bool isBoss1Dead;
        private bool isBoss2Dead;
        private bool isBoss3Dead;
        private bool isShadowOrbSmashed;
        private bool isMeteorSpawned;
        private byte shadowOrbsSmashed;
        private int invasionDelay;
        private int invasionSize;
        private int invasionType;
        private double invasionPointX;

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
    }
}
