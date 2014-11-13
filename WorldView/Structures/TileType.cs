namespace MoreTerra.Structures
{
    using System;

    public class TileType
    {
        public const Byte Dirt = 0;
        public const Byte Stone = 1;
        public const Byte Grass = 2;
        public const Byte Plants = 3;
        public const Byte Torch = 4;
        public const Byte Trees = 5;
        public const Byte Iron = 6;
        public const Byte Copper = 7;
        public const Byte Gold = 8;
        public const Byte Silver = 9;

        public const Byte Door = 10;
        public const Byte DoorOpen = 11;
        public const Byte Heart = 12;
        public const Byte Bottles = 13;
        public const Byte Table = 14;
        public const Byte Chair = 15;
        public const Byte Anvil = 16;
        public const Byte Furnace = 17;
        public const Byte CraftingTable = 18;
        public const Byte WoodenPlatform = 19;

        public const Byte PlantsDecorative = 20;
        public const Byte Chest = 21;
        public const Byte Demonite = 22;
        public const Byte CorruptionGrass = 23;
        public const Byte CorruptionPlants = 24;
        public const Byte Ebonstone = 25;
        public const Byte Altar = 26;
        public const Byte Sunflower = 27;
        public const Byte Pot = 28;
        public const Byte PiggyBank = 29;

        public const Byte BlockWood = 30;
        public const Byte ShadowOrb = 31;
        public const Byte CorruptionVines = 32;
        public const Byte Candle = 33;
        public const Byte ChandlerCopper = 34;
        public const Byte ChandlerSilver = 35;
        public const Byte ChandlerGold = 36;
        public const Byte Meteorite = 37; // Credit Vib Rib
        public const Byte BlockStone = 38;
        public const Byte BlockRedStone = 39;

		public const Byte Clay = 40;
        public const Byte BlockBlueStone = 41;
        public const Byte LightGlobe = 42;
        public const Byte BlockGreenStone = 43;
        public const Byte BlockPinkStone = 44;
        public const Byte BlockGold = 45;
        public const Byte BlockSilver = 46;
        public const Byte BlockCopper = 47;
        public const Byte Spikes = 48;
        public const Byte CandleBlue = 49;

		public const Byte Books = 50;
        public const Byte Web = 51;
        public const Byte Vines = 52;
        public const Byte Sand = 53;
        public const Byte Glass = 54;
        public const Byte Sign = 55;
        public const Byte Obsidian = 56;
        public const Byte Ash = 57; // Credit Infinite Monkeys
        public const Byte Hellstone = 58; // Credit Vib Rib
        public const Byte Mud = 59;

		public const Byte UndergroundJungleGrass = 60;
        public const Byte UndergroundJunglePlants = 61;
        public const Byte UndergroundJungleVines = 62;
        public const Byte Sapphire = 63;
        public const Byte Ruby = 64;
        public const Byte Emerald = 65;
        public const Byte Topaz = 66;
        public const Byte Amethyst = 67;
        public const Byte Diamond = 68;
        public const Byte UndergroundJungleThorns = 69; // Credit Dr VideoGames 0031

		public const Byte UndergroundMushroomGrass = 70;
        public const Byte UndergroundMushroomPlants = 71;
        public const Byte UndergroundMushroomTrees = 72;
        public const Byte Plants2 = 73;
        public const Byte Plants3 = 74;
        public const Byte BlockObsidian = 75;
        public const Byte BlockHellstone = 76;
        public const Byte Hellforge = 77;
        public const Byte DecorativePot = 78;
        public const Byte Bed = 79;

        public const Byte Cactus = 80;
        public const Byte Coral = 81;
        public const Byte HerbImmature = 82;
        public const Byte HerbMature = 83;
        public const Byte HerbBlooming = 84;
        public const Byte Tombstone = 85;
		public const Byte Loom = 86;
		public const Byte Piano = 87;
		public const Byte Dresser = 88;
		public const Byte Bench = 89;

		public const Byte Bathtub = 90;
		public const Byte Banner = 91;
		public const Byte Lamppost = 92;
		public const Byte Tikitorch = 93;
		public const Byte Keg = 94;
		public const Byte ChineseLamp = 95;
		public const Byte CookingPot = 96;
		public const Byte Safe = 97;
		public const Byte SkullCandle = 98;
		public const Byte Trashcan = 99;

		public const Byte Candleabra = 100;
		public const Byte Bookcase = 101;
		public const Byte Throne = 102;
		public const Byte Plate = 103;
		public const Byte Clock = 104;
		public const Byte Statue = 105;
		public const Byte Sawmill = 106;

        public const Byte Unknown = 228;

        // Additional non-tile types, here to shrink things into a byte.
		public const Byte Processed = 229;

		public const Byte Water = 230;
		public const Byte Lava = 231;

		public const Byte BackgroundSky = 232;
		public const Byte BackgroundDirt = 233;
		public const Byte BackgroundCave = 234;
		public const Byte BackgroundHell = 235;

		public const Byte WallStone = 236;
        public const Byte WallDirt = 237;
        public const Byte WallEbonstone = 238;
        public const Byte WallWood = 239;
        public const Byte WallGreyBrick = 240;
        public const Byte WallRedBrick = 241;
        public const Byte WallBlueBrick = 242;
        public const Byte WallGreenBrick = 243;
        public const Byte WallPinkBrick = 244;
        public const Byte WallGoldBrick = 245;
        public const Byte WallSilverBrick = 246;
        public const Byte WallCopperBrick = 247;
        public const Byte WallHellstone = 248;
		public const Byte WallObsidianBrick = 249;
		public const Byte WallMud = 250;
		public const Byte WallDirtSafe = 251;
		public const Byte WallBlueSafe = 252;
		public const Byte WallGreenSafe = 253; 
		public const Byte WallPinkSafe = 254;
		public const Byte WallObsidian = 255;

		public static String[] typeStrings;

		public static void Initialize()
		{
			typeStrings = new String[256];

			typeStrings[TileType.Dirt] = "Dirt";
			typeStrings[TileType.Stone] = "Stone";
			typeStrings[TileType.Grass] = "Grass";
			typeStrings[TileType.Plants] = "Plants";
			typeStrings[TileType.Torch] = "Torch";
			typeStrings[TileType.Trees] = "Trees";
			typeStrings[TileType.Iron] = "Iron";
			typeStrings[TileType.Copper] = "Copper";
			typeStrings[TileType.Gold] = "Gold";
			typeStrings[TileType.Silver] = "Silver";

			typeStrings[TileType.Door] = "Door";
			typeStrings[TileType.DoorOpen] = "DoorOpen";
			typeStrings[TileType.Heart] = "Heart";
			typeStrings[TileType.Bottles] = "Bottles";
			typeStrings[TileType.Table] = "Table";
			typeStrings[TileType.Chair] = "Chair";
			typeStrings[TileType.Anvil] = "Anvil";
			typeStrings[TileType.Furnace] = "Furnace";
			typeStrings[TileType.CraftingTable] = "CraftingTable";
			typeStrings[TileType.WoodenPlatform] = "WoodenPlatform";

			typeStrings[TileType.PlantsDecorative] = "PlantsDecorative";
			typeStrings[TileType.Chest] = "Chest";
			typeStrings[TileType.Demonite] = "Demonite";
			typeStrings[TileType.CorruptionGrass] = "CorruptionGrass";
			typeStrings[TileType.CorruptionPlants] = "CorruptionPlants";
			typeStrings[TileType.Ebonstone] = "Ebonstone";
			typeStrings[TileType.Altar] = "Altar";
			typeStrings[TileType.Sunflower] = "Sunflower";
			typeStrings[TileType.Pot] = "Pot";
			typeStrings[TileType.PiggyBank] = "PiggyBank";

			typeStrings[TileType.BlockWood] = "BlockWood";
			typeStrings[TileType.ShadowOrb] = "ShadowOrb";
			typeStrings[TileType.CorruptionVines] = "CorruptionVines";
			typeStrings[TileType.Candle] = "Candle";
			typeStrings[TileType.ChandlerCopper] = "ChandlerCopper";
			typeStrings[TileType.ChandlerSilver] = "ChandlerSilver";
			typeStrings[TileType.ChandlerGold] = "ChandlerGold";
			typeStrings[TileType.Meteorite] = "Meteorite";
			typeStrings[TileType.BlockStone] = "BlockStone";
			typeStrings[TileType.BlockRedStone] = "BlockRedStone";

			typeStrings[TileType.Clay] = "Clay";
			typeStrings[TileType.BlockBlueStone] = "BlockBlueStone";
			typeStrings[TileType.LightGlobe] = "LightGlobe";
			typeStrings[TileType.BlockGreenStone] = "BlockGreenStone";
			typeStrings[TileType.BlockPinkStone] = "BlockPinkStone";
			typeStrings[TileType.BlockGold] = "BlockGold";
			typeStrings[TileType.BlockSilver] = "BlockSilver";
			typeStrings[TileType.BlockCopper] = "BlockCopper";
			typeStrings[TileType.Spikes] = "Spikes";
			typeStrings[TileType.CandleBlue] = "CandleBlue";

			typeStrings[TileType.Books] = "Books";
			typeStrings[TileType.Web] = "Web";
			typeStrings[TileType.Vines] = "Vines";
			typeStrings[TileType.Sand] = "Sand";
			typeStrings[TileType.Glass] = "Glass";
			typeStrings[TileType.Sign] = "Sign";
			typeStrings[TileType.Obsidian] = "Obsidian";
			typeStrings[TileType.Ash] = "Ash";
			typeStrings[TileType.Hellstone] = "Hellstone";
			typeStrings[TileType.Mud] = "Mud";

			typeStrings[TileType.UndergroundJungleGrass] = "UndergroundJungleGrass";
			typeStrings[TileType.UndergroundJunglePlants] = "UndergroundJunglePlants";
			typeStrings[TileType.UndergroundJungleVines] = "UndergroundJungleVines";
			typeStrings[TileType.Sapphire] = "Sapphire";
			typeStrings[TileType.Ruby] = "Ruby";
			typeStrings[TileType.Emerald] = "Emerald";
			typeStrings[TileType.Topaz] = "Topaz";
			typeStrings[TileType.Amethyst] = "Amethyst";
			typeStrings[TileType.Diamond] = "Diamond";
			typeStrings[TileType.UndergroundJungleThorns] = "UndergroundJungleThorns";

			typeStrings[TileType.UndergroundMushroomGrass] = "UndergroundMushroomGrass";
			typeStrings[TileType.UndergroundMushroomPlants] = "UndergroundMushroomPlants";
			typeStrings[TileType.UndergroundMushroomTrees] = "UndergroundMushroomTrees";
			typeStrings[TileType.Plants2] = "Plants2";
			typeStrings[TileType.Plants3] = "Plants3";
			typeStrings[TileType.BlockObsidian] = "BlockObsidian";
			typeStrings[TileType.BlockHellstone] = "BlockHellstone";
			typeStrings[TileType.Hellforge] = "Hellforge";
			typeStrings[TileType.DecorativePot] = "DecorativePot";
			typeStrings[TileType.Bed] = "Bed";

			typeStrings[TileType.Cactus] = "Cactus";
			typeStrings[TileType.Coral] = "Coral";
			typeStrings[TileType.HerbImmature] = "HerbImmature";
			typeStrings[TileType.HerbMature] = "HerbMature";
			typeStrings[TileType.HerbBlooming] = "HerbBlooming";
			typeStrings[TileType.Tombstone] = "Tombstone";
			typeStrings[TileType.Loom] = "Loom";
			typeStrings[TileType.Piano] = "Piano";
			typeStrings[TileType.Dresser] = "Dresser";
			typeStrings[TileType.Bench] = "Bench";

			typeStrings[TileType.Bathtub] = "Bathtub";
			typeStrings[TileType.Banner] = "Banner";
			typeStrings[TileType.Lamppost] = "Lamppost";
			typeStrings[TileType.Tikitorch] = "Tikitorch";
			typeStrings[TileType.Keg] = "Keg";
			typeStrings[TileType.ChineseLamp] = "ChineseLamp";
			typeStrings[TileType.CookingPot] = "CookingPot";
			typeStrings[TileType.Safe] = "Safe";
			typeStrings[TileType.SkullCandle] = "SkullCandle";
			typeStrings[TileType.Trashcan] = "Trashcan";

			typeStrings[TileType.Candleabra] = "Candleabra";
			typeStrings[TileType.Bookcase] = "Bookcase";
			typeStrings[TileType.Throne] = "Throne";
			typeStrings[TileType.Plate] = "Plate";
			typeStrings[TileType.Clock] = "Clock";
			typeStrings[TileType.Statue] = "Statue";
			typeStrings[TileType.Sawmill] = "Sawmill";

			typeStrings[TileType.Unknown] = "Unknown";

			typeStrings[TileType.Processed] = "Processed";

			typeStrings[TileType.Water] = "Water";
			typeStrings[TileType.Lava] = "Lava";

			typeStrings[TileType.BackgroundSky] = "BackgroundSky";
			typeStrings[TileType.BackgroundDirt] = "BackgroundDirt";
			typeStrings[TileType.BackgroundCave] = "BackgroundCave";
			typeStrings[TileType.BackgroundHell] = "BackgroundHell";

			typeStrings[TileType.WallStone] = "WallStone";
			typeStrings[TileType.WallDirt] = "WallDirt";
			typeStrings[TileType.WallEbonstone] = "WallEbonstone";
			typeStrings[TileType.WallWood] = "WallWood";
			typeStrings[TileType.WallGreyBrick] = "WallGreyBrick";
			typeStrings[TileType.WallRedBrick] = "WallRedBrick";
			typeStrings[TileType.WallBlueBrick] = "WallBlueBrick";
			typeStrings[TileType.WallGreenBrick] = "WallGreenBrick";
			typeStrings[TileType.WallPinkBrick] = "WallPinkBrick";
			typeStrings[TileType.WallGoldBrick] = "WallGoldBrick";
			typeStrings[TileType.WallSilverBrick] = "WallSilverBrick";
			typeStrings[TileType.WallCopperBrick] = "WallCopperBrick";
			typeStrings[TileType.WallHellstone] = "WallHellstone";
			typeStrings[TileType.WallObsidianBrick] = "WallObsidianBrick";
			typeStrings[TileType.WallMud] = "WallMud";
			typeStrings[TileType.WallDirtSafe] = "WallDirtSafe";
			typeStrings[TileType.WallBlueSafe] = "WallBlueSafe";
			typeStrings[TileType.WallGreenSafe] = "WallGreenSafe";
			typeStrings[TileType.WallPinkSafe] = "WallPinkSafe";
			typeStrings[TileType.WallObsidian] = "WallObsidian";
		}

    }

}
