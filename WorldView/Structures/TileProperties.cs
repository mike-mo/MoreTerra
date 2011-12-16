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
			TileProperties.Unknown = startPos;

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

			#region oldTypeDefs
			/*			tileTypeDefs[TileType.Dirt] = new TileData(TileType.Dirt, false, Constants.Colors.DIRT);
			tileTypeDefs[TileType.Stone] = new TileData(TileType.Stone, false, Constants.Colors.STONE);
			tileTypeDefs[TileType.Grass] = new TileData(TileType.Grass, false, Constants.Colors.GRASS);
			tileTypeDefs[TileType.Plants] = new TileData(TileType.Plants, true, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.Torch] = new TileData(TileType.Torch, false, Constants.Colors.LIGHT_SOURCE, MarkerType.Torch);
			tileTypeDefs[TileType.Trees] = new TileData(TileType.Trees, true, Constants.Colors.WOOD);
			tileTypeDefs[TileType.Iron] = new TileData(TileType.Iron, false, Constants.Colors.IRON, MarkerType.Iron);
			tileTypeDefs[TileType.Copper] = new TileData(TileType.Copper, false, Constants.Colors.COPPER, MarkerType.Copper);
			tileTypeDefs[TileType.Gold] = new TileData(TileType.Gold, false, Constants.Colors.GOLD, MarkerType.Gold);
			tileTypeDefs[TileType.Silver] = new TileData(TileType.Silver, false, Constants.Colors.SILVER, MarkerType.Silver);

			tileTypeDefs[TileType.Door] = new TileData(TileType.Door, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.DoorOpen] = new TileData(TileType.DoorOpen, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Heart] = new TileData(TileType.Heart, true, Constants.Colors.IMPORTANT, MarkerType.Heart);
			tileTypeDefs[TileType.Bottles] = new TileData(TileType.Bottles, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Table] = new TileData(TileType.Table, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Chair] = new TileData(TileType.Chair, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Anvil] = new TileData(TileType.Anvil, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Furnace] = new TileData(TileType.Furnace, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.CraftingTable] = new TileData(TileType.CraftingTable, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.WoodenPlatform] = new TileData(TileType.WoodenPlatform, false, Constants.Colors.WOOD);

			// #21 Chest has a marker but we do not use it to determine markers.
			tileTypeDefs[TileType.PlantsDecorative] = new TileData(TileType.PlantsDecorative, true, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.Chest] = new TileData(TileType.Chest, true, Constants.Colors.IMPORTANT);
			tileTypeDefs[TileType.Demonite] = new TileData(TileType.Demonite, false, Constants.Colors.DEMONITE, MarkerType.Demonite);
			tileTypeDefs[TileType.CorruptionGrass] = new TileData(TileType.CorruptionGrass, false, Constants.Colors.CORRUPTION_GRASS);
			tileTypeDefs[TileType.CorruptionPlants] = new TileData(TileType.CorruptionPlants, true, Constants.Colors.CORRUPTION_GRASS);
			tileTypeDefs[TileType.Ebonstone] = new TileData(TileType.Ebonstone, false, Constants.Colors.EBONSTONE);
			tileTypeDefs[TileType.Altar] = new TileData(TileType.Altar, true, Constants.Colors.IMPORTANT, MarkerType.Altar);
			tileTypeDefs[TileType.Sunflower] = new TileData(TileType.Sunflower, true, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.Pot] = new TileData(TileType.Pot, true, Constants.Colors.IMPORTANT);
			tileTypeDefs[TileType.PiggyBank] = new TileData(TileType.PiggyBank, true, Constants.Colors.DECORATIVE);

			tileTypeDefs[TileType.BlockWood] = new TileData(TileType.BlockWood, false, Constants.Colors.WOOD_BLOCK);
			tileTypeDefs[TileType.ShadowOrb] = new TileData(TileType.ShadowOrb, true, Constants.Colors.IMPORTANT, MarkerType.ShadowOrb);
			tileTypeDefs[TileType.CorruptionVines] = new TileData(TileType.CorruptionVines, false, Constants.Colors.CORRUPTION_VINES);
			tileTypeDefs[TileType.Candle] = new TileData(TileType.Candle, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.ChandlerCopper] = new TileData(TileType.ChandlerCopper, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.ChandlerSilver] = new TileData(TileType.ChandlerSilver, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.ChandlerGold] = new TileData(TileType.ChandlerGold, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.Meteorite] = new TileData(TileType.Meteorite, false, Constants.Colors.METEORITE, MarkerType.Meteorite);
			tileTypeDefs[TileType.BlockStone] = new TileData(TileType.BlockStone, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.BlockRedStone] = new TileData(TileType.BlockRedStone, false, Constants.Colors.BLOCK);

			tileTypeDefs[TileType.Clay] = new TileData(TileType.Clay, false, Constants.Colors.CLAY);
			tileTypeDefs[TileType.BlockBlueStone] = new TileData(TileType.BlockBlueStone, false, Constants.Colors.DUNGEON_BLUE);
			tileTypeDefs[TileType.LightGlobe] = new TileData(TileType.LightGlobe, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.BlockGreenStone] = new TileData(TileType.BlockGreenStone, false, Constants.Colors.DUNGEON_GREEN);
			tileTypeDefs[TileType.BlockPinkStone] = new TileData(TileType.BlockPinkStone, false, Constants.Colors.DUNGEON_PINK);
			tileTypeDefs[TileType.BlockGold] = new TileData(TileType.BlockGold, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.BlockSilver] = new TileData(TileType.BlockSilver, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.BlockCopper] = new TileData(TileType.BlockCopper, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.Spikes] = new TileData(TileType.Spikes, false, Constants.Colors.SPIKES);
			tileTypeDefs[TileType.CandleBlue] = new TileData(TileType.CandleBlue, false, Constants.Colors.LIGHT_SOURCE);

			// #55 Sign has a marker but we do not use it to determine markers.
			tileTypeDefs[TileType.Books] = new TileData(TileType.Books, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Web] = new TileData(TileType.Web, false, Constants.Colors.WEB);
			tileTypeDefs[TileType.Vines] = new TileData(TileType.Vines, false, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.Sand] = new TileData(TileType.Sand, false, Constants.Colors.SAND);
			tileTypeDefs[TileType.Glass] = new TileData(TileType.Glass, false, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Sign] = new TileData(TileType.Sign, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Obsidian] = new TileData(TileType.Obsidian, false, Constants.Colors.OBSIDIAN, MarkerType.Obsidian);
			tileTypeDefs[TileType.Ash] = new TileData(TileType.Ash, false, Constants.Colors.ASH);
			tileTypeDefs[TileType.Hellstone] = new TileData(TileType.Hellstone, false, Constants.Colors.HELLSTONE, MarkerType.Hellstone);
			tileTypeDefs[TileType.Mud] = new TileData(TileType.Mud, false, Constants.Colors.MUD);

			tileTypeDefs[TileType.UndergroundJungleGrass] = new TileData(TileType.UndergroundJungleGrass, false, Constants.Colors.UNDERGROUNDJUNGLE_GRASS);
			tileTypeDefs[TileType.UndergroundJunglePlants] = new TileData(TileType.UndergroundJunglePlants, true, Constants.Colors.UNDERGROUNDJUNGLE_PLANTS);
			tileTypeDefs[TileType.UndergroundJungleVines] = new TileData(TileType.UndergroundJungleVines, false, Constants.Colors.UNDERGROUNDJUNGLE_VINES);
			tileTypeDefs[TileType.Sapphire] = new TileData(TileType.Sapphire, false, Constants.Colors.GEMS, MarkerType.Sapphire);
			tileTypeDefs[TileType.Ruby] = new TileData(TileType.Ruby, false, Constants.Colors.GEMS, MarkerType.Ruby);
			tileTypeDefs[TileType.Emerald] = new TileData(TileType.Emerald, false, Constants.Colors.GEMS, MarkerType.Emerald);
			tileTypeDefs[TileType.Topaz] = new TileData(TileType.Topaz, false, Constants.Colors.GEMS, MarkerType.Topaz);
			tileTypeDefs[TileType.Amethyst] = new TileData(TileType.Amethyst, false, Constants.Colors.GEMS, MarkerType.Amethyst);
			tileTypeDefs[TileType.Diamond] = new TileData(TileType.Diamond, false, Constants.Colors.GEMS, MarkerType.Diamond);
			tileTypeDefs[TileType.UndergroundJungleThorns] = new TileData(TileType.UndergroundJungleThorns, false, Constants.Colors.UNDERGROUNDJUNGLE_THORNS);

			tileTypeDefs[TileType.UndergroundMushroomGrass] = new TileData(TileType.UndergroundMushroomGrass, false, Constants.Colors.UNDERGROUNDMUSHROOM_GRASS);
			tileTypeDefs[TileType.UndergroundMushroomPlants] = new TileData(TileType.UndergroundMushroomPlants, true, Constants.Colors.UNDERGROUNDMUSHROOM_PLANTS);
			tileTypeDefs[TileType.UndergroundMushroomTrees] = new TileData(TileType.UndergroundMushroomTrees, true, Constants.Colors.UNDERGROUNDMUSHROOM_TREES);
			tileTypeDefs[TileType.Plants2] = new TileData(TileType.Plants2, true, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.Plants3] = new TileData(TileType.Plants3, true, Constants.Colors.PLANTS);
			tileTypeDefs[TileType.BlockObsidian] = new TileData(TileType.BlockObsidian, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.BlockHellstone] = new TileData(TileType.BlockHellstone, false, Constants.Colors.BLOCK);
			tileTypeDefs[TileType.Hellforge] = new TileData(TileType.Hellforge, true, Constants.Colors.IMPORTANT, MarkerType.Hellforge);
			tileTypeDefs[TileType.DecorativePot] = new TileData(TileType.DecorativePot, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Bed] = new TileData(TileType.Bed, true, Constants.Colors.DECORATIVE);

			tileTypeDefs[TileType.Cactus] = new TileData(TileType.Cactus, false, Constants.Colors.CACTUS);
			tileTypeDefs[TileType.Coral] = new TileData(TileType.Coral, true, Constants.Colors.CORAL);
			tileTypeDefs[TileType.HerbImmature] = new TileData(TileType.HerbImmature, true, Constants.Colors.HERB);
			tileTypeDefs[TileType.HerbMature] = new TileData(TileType.HerbMature, true, Constants.Colors.HERB);
			tileTypeDefs[TileType.HerbBlooming] = new TileData(TileType.HerbBlooming, true, Constants.Colors.HERB);
			tileTypeDefs[TileType.Tombstone] = new TileData(TileType.Tombstone, true, Constants.Colors.TOMBSTONE);
			tileTypeDefs[TileType.Loom] = new TileData(TileType.Loom, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Piano] = new TileData(TileType.Piano, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Dresser] = new TileData(TileType.Dresser, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Bench] = new TileData(TileType.Bench, true, Constants.Colors.DECORATIVE);

			tileTypeDefs[TileType.Bathtub] = new TileData(TileType.Bathtub, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Banner] = new TileData(TileType.Banner, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Lamppost] = new TileData(TileType.Lamppost, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.Tikitorch] = new TileData(TileType.Tikitorch, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.Keg] = new TileData(TileType.Keg, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.ChineseLamp] = new TileData(TileType.ChineseLamp, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.CookingPot] = new TileData(TileType.CookingPot, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Safe] = new TileData(TileType.Safe, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.SkullCandle] = new TileData(TileType.SkullCandle, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.Trashcan] = new TileData(TileType.Trashcan, true, Constants.Colors.DECORATIVE);

			tileTypeDefs[TileType.Candleabra] = new TileData(TileType.Candleabra, true, Constants.Colors.LIGHT_SOURCE);
			tileTypeDefs[TileType.Bookcase] = new TileData(TileType.Bookcase, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Throne] = new TileData(TileType.Throne, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Plate] = new TileData(TileType.Plate, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Clock] = new TileData(TileType.Clock, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Statue] = new TileData(TileType.Statue, true, Constants.Colors.DECORATIVE);
			tileTypeDefs[TileType.Sawmill] = new TileData(TileType.Sawmill, true, Constants.Colors.DECORATIVE);
*/
			/*
			tileTypeDefs[TileType.Processed] = new TileData(false, Color.AliceBlue);

			tileTypeDefs[TileType.Water] = new TileData(false, Constants.Colors.WATER);
			tileTypeDefs[TileType.Lava] = new TileData(false, Constants.Colors.LAVA);

			tileTypeDefs[TileType.BackgroundSky] = new TileData(false, Constants.Colors.SKY);
			tileTypeDefs[TileType.BackgroundDirt] = new TileData(false, Constants.Colors.WALL_BACKGROUND);
			tileTypeDefs[TileType.BackgroundCave] = new TileData(false, Constants.Colors.WALL_BACKGROUND);
			tileTypeDefs[TileType.BackgroundHell] = new TileData(false, Constants.Colors.WALL_BACKGROUND);
			*/
			/*
			tileTypeDefs[TileType.WallStone] = new TileData(TileType.WallStone, false, Constants.Colors.WALL_STONE);
			tileTypeDefs[TileType.WallDirt] = new TileData(TileType.WallDirt, false, Constants.Colors.WALL_DIRT);
			tileTypeDefs[TileType.WallEbonstone] = new TileData(TileType.WallEbonstone, false, Constants.Colors.WALL_EBONSTONE);
			tileTypeDefs[TileType.WallWood] = new TileData(TileType.WallWood, false, Constants.Colors.WALL_WOOD);
			tileTypeDefs[TileType.WallGreyBrick] = new TileData(TileType.WallGreyBrick, false, Constants.Colors.WALL_BRICK);
			tileTypeDefs[TileType.WallRedBrick] = new TileData(TileType.WallRedBrick, false, Constants.Colors.WALL_BRICK);
			tileTypeDefs[TileType.WallBlueBrick] = new TileData(TileType.WallBlueBrick, false, Constants.Colors.WALL_DUNGEON_BLUE);
			tileTypeDefs[TileType.WallGreenBrick] = new TileData(TileType.WallGreenBrick, false, Constants.Colors.WALL_DUNGEON_GREEN);
			tileTypeDefs[TileType.WallPinkBrick] = new TileData(TileType.WallPinkBrick, false, Constants.Colors.WALL_DUNGEON_PINK);
			tileTypeDefs[TileType.WallGoldBrick] = new TileData(TileType.WallGoldBrick, false, Constants.Colors.WALL_BRICK);
			tileTypeDefs[TileType.WallSilverBrick] = new TileData(TileType.WallSilverBrick, false, Constants.Colors.WALL_BRICK);
			tileTypeDefs[TileType.WallCopperBrick] = new TileData(TileType.WallCopperBrick, false, Constants.Colors.WALL_BRICK);
			tileTypeDefs[TileType.WallHellstone] = new TileData(TileType.WallHellstone, false, Constants.Colors.WALL_HELLSTONE);
			tileTypeDefs[TileType.WallObsidianBrick] = new TileData(TileType.WallObsidianBrick, false, Constants.Colors.WALL_OBSIDIAN);
			tileTypeDefs[TileType.WallMud] = new TileData(TileType.WallMud, false, Constants.Colors.WALL_MUD);
			tileTypeDefs[TileType.WallDirtSafe] = new TileData(TileType.WallDirtSafe, false, Constants.Colors.WALL_DIRT);
			tileTypeDefs[TileType.WallBlueSafe] = new TileData(TileType.WallBlueSafe, false, Constants.Colors.WALL_DUNGEON_BLUE);
			tileTypeDefs[TileType.WallGreenSafe] = new TileData(TileType.WallGreenSafe, false, Constants.Colors.WALL_DUNGEON_GREEN);
			tileTypeDefs[TileType.WallPinkSafe] = new TileData(TileType.WallPinkSafe, false, Constants.Colors.WALL_DUNGEON_PINK);
			tileTypeDefs[TileType.WallObsidian] = new TileData(TileType.WallObsidian, false, Constants.Colors.WALL_BACKGROUND);
			*/
			#endregion

			for (Int32 i = 0; i < 256; i++)
			{
				if (tileTypeDefs[i] == null)
					tileTypeDefs[i] = new TileData(false, Color.Magenta);
			}

		}
	}
}
