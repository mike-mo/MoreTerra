using System;
using System.Drawing;
using System.Collections.Generic;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra.Structures
{
	public class TileData
	{
		private Boolean isImportant;
		private Color color;
        private Color officialColor;
        private Boolean transparent;
		private Boolean drawMarker;
		private MarkerType markerType;

		#region Constructors
		public TileData()
		{
			this.isImportant = false;
			this.color = Color.Magenta;
            this.officialColor = Color.Magenta;
            this.transparent = false;
			this.markerType = MarkerType.Unknown;
		}

		public TileData(bool isTileImportant, Color color, Color officialColor, MarkerType hasMarker = MarkerType.Unknown)
		{
			this.isImportant = isTileImportant;
			this.color = color;
            this.officialColor = officialColor;
            this.transparent = false;
			this.drawMarker = false;
			this.markerType = hasMarker;
		}
		#endregion

		#region GetSet Functions
        public Color Color
		{
			get
			{
                return this.color;
            }
        }

        public Color OfficialColor
        {
            get
            {
                return this.officialColor;
			}
		}

		public bool IsImportant
		{
			get
			{
				return this.isImportant;
			}
			set
			{
				this.isImportant = value;
			}
		}

		public bool DrawMarker
		{
			get
			{
				return this.drawMarker;
			}
			set
			{
				drawMarker = value;
			}
		}

		public MarkerType MarkerType
		{
			get
			{
				return this.markerType;
			}
			set {
				this.markerType = value;
			}
		}
		#endregion
	}

	public class TileProperties
	{
		public static TileData[] tileTypeDefs;

		public static Int16 Sign;
		public static Int16 Chest;
        public static Int16 Amethyst;
        public static Int16 Diamond;
        public static Int16 Emerald;
        public static Int16 Ruby;
        public static Int16 Sapphire;
        public static Int16 Topaz;
        public static Int16 ExposedGems;
        public static Int16 SmallDetritus;
        public static Int16 LargeDetritus;
        public static Int16 LargeDetritus2;
        public static Int16 CopperCache;
        public static Int16 SilverCache;
        public static Int16 GoldCache;
        public static Int16 Unknown;
		public static Int16 Processed;
		public static Int16 Cropped;
		public static Int16 BackgroundOffset;
		public static Int16 Water;
		public static Int16 Lava;
		public static Int16 Honey;
		public static Int16 RedWire;
		public static Int16 BlueWire;
		public static Int16 GreenWire;
		public static Int16 WallOffset;

		public const int TYPES = 512;

		public static void Initialize()
		{
			MarkerType mt;
			Int16 startPos;

			tileTypeDefs = new TileData[TileProperties.TYPES];

			startPos = 0;
			foreach (KeyValuePair<Int32, TileInfo> kvp in Global.Instance.Info.Tiles)
			{
				if (kvp.Value.name == "Signs")
                    TileProperties.Sign = (Int16)kvp.Key;
				else if (kvp.Value.name == "Containers")
                    TileProperties.Chest = (Int16)kvp.Key;
                else if (kvp.Value.name == "Amethyst")
                    TileProperties.Amethyst = (Int16)kvp.Key;
                else if (kvp.Value.name == "Diamond")
                    TileProperties.Diamond = (Int16)kvp.Key;
                else if (kvp.Value.name == "Emerald")
                    TileProperties.Emerald = (Int16)kvp.Key;
                else if (kvp.Value.name == "Ruby")
                    TileProperties.Ruby = (Int16)kvp.Key;
                else if (kvp.Value.name == "Sapphire")
                    TileProperties.Sapphire = (Int16)kvp.Key;
                else if (kvp.Value.name == "Topaz")
                    TileProperties.Topaz = (Int16)kvp.Key;
                else if (kvp.Value.name == "Exposed Gems")
                    TileProperties.ExposedGems = (Int16)kvp.Key;
                else if (kvp.Value.name == "Small Detritus")
                    TileProperties.SmallDetritus = (Int16)kvp.Key;
                else if (kvp.Value.name == "Large Detritus")
                    TileProperties.LargeDetritus = (Int16)kvp.Key;
                else if (kvp.Value.name == "Large Detritus2")
                    TileProperties.LargeDetritus2 = (Int16)kvp.Key;
                else if (kvp.Value.name == "Copper Cache")
                    TileProperties.CopperCache = (Int16)kvp.Key;
                else if (kvp.Value.name == "Silver Cache")
                    TileProperties.SilverCache = (Int16)kvp.Key;
                else if (kvp.Value.name == "Gold Cache")
                    TileProperties.GoldCache = (Int16)kvp.Key;

				if (kvp.Value.markerName != "")
				{
                    MarkerInfo mi = Global.Instance.Info.GetMarkerByName(kvp.Value.markerName);

                    if (mi == null)
						mt = MarkerType.Unknown;
                    else
                    {
                        if (Enum.TryParse<MarkerType>(mi.markerImage, out mt) == false)
                            mt = MarkerType.Unknown;
                    }
				}
				else
				{
					mt = MarkerType.Unknown;
				}

				tileTypeDefs[kvp.Key] = new TileData(kvp.Value.important, kvp.Value.color, kvp.Value.officialColor, mt);
				startPos++;
			}
			TileProperties.Unknown = startPos;

			startPos = (Int16) ((TileProperties.TYPES - 2) - Global.Instance.Info.Walls.Count);

			foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in Global.Instance.Info.SpecialObjects)
			{
				startPos -= (Int16) kvp.Value.Count;
			}

			TileProperties.Processed = startPos;
			tileTypeDefs[startPos++] = new TileData(false, Color.AliceBlue, Color.AliceBlue);
			TileProperties.Cropped = startPos;
			tileTypeDefs[startPos++] = new TileData(false, Color.AliceBlue, Color.AliceBlue);

			foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in Global.Instance.Info.SpecialObjects)
			{
				switch (kvp.Key)
				{
					case "Background":
						TileProperties.BackgroundOffset = startPos;

						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							tileTypeDefs[startPos++] = new TileData(false, soi.color, soi.officialColor);
						}
						break;
					case "Liquid":
						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							if (soi.name == "Honey")
								TileProperties.Honey = startPos;
							else if (soi.name == "Lava")
								TileProperties.Lava = startPos;
							else if (soi.name == "Water")
								TileProperties.Water = startPos;

							tileTypeDefs[startPos++] = new TileData(false, soi.color, soi.officialColor);
						}
						break;
					case "Wire":
						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							if (soi.name == "Red Wire")
								TileProperties.RedWire = startPos;
							else if (soi.name == "Green Wire")
								TileProperties.GreenWire = startPos;
							else if (soi.name == "Blue Wire")
								TileProperties.BlueWire = startPos;

							tileTypeDefs[startPos++] = new TileData(false, soi.color, soi.officialColor);
						}
						break;
					default:
						break;
				}

			}

			// This is a startPos -1 because walls start at 1, not at zero.
			TileProperties.WallOffset = (Int16) (startPos - 1);
			foreach (KeyValuePair<Int32, WallInfo> kvp in Global.Instance.Info.Walls)
			{
				tileTypeDefs[startPos] = new TileData(false, kvp.Value.color, kvp.Value.officialColor);
				startPos++;
			}


			for (Int32 i = 0; i < TileProperties.TYPES; i++)
			{
				if (tileTypeDefs[i] == null)
					tileTypeDefs[i] = new TileData(false, Color.Magenta, Color.Magenta);
			}

		}
	}
}
