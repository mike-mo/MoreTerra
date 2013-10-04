using System;
using System.Drawing;
using System.Collections.Generic;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra.Structures
{
	public class TileData
	{
		private Boolean isImportant;
		private Color colour;
		private Boolean drawMarker;
		private MarkerType markerType;

		#region Constructors
		public TileData()
		{
			this.isImportant = false;
			this.colour = Color.Magenta;
			this.markerType = MarkerType.Unknown;
		}

		public TileData(bool isTileImportant, Color colour, MarkerType hasMarker = MarkerType.Unknown)
		{
			this.isImportant = isTileImportant;
			this.colour = colour;
			this.drawMarker = false;
			this.markerType = hasMarker;
		}
		#endregion

		#region GetSet Functions
		public Color Colour
		{
			get
			{
				return this.colour;
			}
		}

		public bool IsImportant
		{
			get
			{
				return this.isImportant;
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

		public static Byte Sign;
		public static Byte Chest;
		public static Byte Unknown;
		public static Byte Processed;
		public static Byte Cropped;
		public static Byte BackgroundOffset;
		public static Byte Water;
		public static Byte Lava;
		public static Byte Honey;
		public static Byte Wire;
		public static Byte WallOffset;

		public static void Initialize()
		{
			Boolean Important;
			MarkerType mt;
			Byte startPos;

			tileTypeDefs = new TileData[256];

			startPos = 0;
			foreach (KeyValuePair<Int32, TileInfo> kvp in Global.Instance.Info.Tiles)
			{
				if (kvp.Value.name == "Signs")
					TileProperties.Sign = (Byte)kvp.Key;
				else if (kvp.Value.name == "Containers")
					TileProperties.Chest = (Byte)kvp.Key;

				Important = (kvp.Value.autoGenType == String.Empty);

				if (kvp.Value.markerName != "")
				{
					if (Enum.TryParse<MarkerType>(Global.Instance.Info.Markers[kvp.Value.markerName].markerImage, out mt) == false)
						mt = MarkerType.Unknown;
				}
				else
				{
					mt = MarkerType.Unknown;
				}

				tileTypeDefs[kvp.Key] = new TileData(Important, kvp.Value.color, mt);
				startPos++;
			}
            TileProperties.Unknown = 210; // startPos;

			startPos = (Byte) (254 - Global.Instance.Info.Walls.Count);

			foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in Global.Instance.Info.SpecialObjects)
			{
				startPos -= (Byte) kvp.Value.Count;
			}

			TileProperties.Processed = startPos;
			tileTypeDefs[startPos++] = new TileData(false, Color.AliceBlue);
			TileProperties.Cropped = startPos;
			tileTypeDefs[startPos++] = new TileData(false, Color.AliceBlue);

			foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in Global.Instance.Info.SpecialObjects)
			{
				switch (kvp.Key)
				{
					case "Background":
						TileProperties.BackgroundOffset = startPos;

						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							tileTypeDefs[startPos++] = new TileData(false, soi.color);
						}
						break;
					case "Liquid":
						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							if (soi.name == "Lava")
								TileProperties.Lava = startPos;
							else if (soi.name == "Water")
								TileProperties.Water = startPos;
							else if (soi.name == "Honey")
								TileProperties.Honey = startPos;

							tileTypeDefs[startPos++] = new TileData(false, soi.color);
						}
						break;
					case "Wire":
						foreach (SpecialObjectInfo soi in kvp.Value)
						{
							if (soi.name == "Wire")
								TileProperties.Wire = startPos;

							tileTypeDefs[startPos++] = new TileData(false, soi.color);
						}
						break;
					default:
						break;
				}

			}

			// This is a startPos -1 because walls start at 1, not at zero.
			TileProperties.WallOffset = (Byte) (startPos - 1);
			foreach (KeyValuePair<Int32, WallInfo> kvp in Global.Instance.Info.Walls)
			{
				tileTypeDefs[startPos] = new TileData(false, kvp.Value.color);
				startPos++;
			}



			for (Int32 i = 0; i < 256; i++)
			{
				if (tileTypeDefs[i] == null)
					tileTypeDefs[i] = new TileData(false, Color.Magenta);
			}

		}
	}
}
