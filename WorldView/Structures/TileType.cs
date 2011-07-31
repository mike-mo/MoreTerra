namespace MoreTerra.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum TileType
    {
        Dirt = 0,
        Stone,
        Grass,
        Plants,
        Torches,
        Trees = 5,
        Iron,
        Copper,
        Gold,
        Silver,

        Door = 10,
        DoorOpen,
        Heart,
        Bottles,
        Table,
        Chair = 15,
        Anvil,
        Furnance,
        CraftingTable,
        WoodenPlatform,

        PlantsDecorative = 20,
        Chest,
        Demonite,
        CorruptionGrass,
        CorruptionPlants,
        Ebonstone = 25,
        Altar,
        Sunflower,
        Pot,
        PiggyBank,

        BlockWood = 30,
        ShadowOrb,
        CorruptionVines,
        Candle,
        ChandlerCopper,
        ChandlerSilver = 35,
        ChandlerGold,
        Meteorite, // Credit Vib Rib
        BlockStone,
        BlockRedStone,

		Clay = 40,
        BlockBlueStone,
        LightGlobe,
        BlockGreenStone,
        BlockPinkStone,
        BlockGold = 45,
        BlockSilver,
        BlockCopper,
        Spikes,
        CandleBlue,

		Books = 50,
        Web,
        Vines,
        Sand,
        Glass,
        Sign = 55,
        Obsidian,
        Ash, // Credit Infinite Monkeys
        Hellstone, // Credit Vib Rib
        Mud,

		UndergroundJungleGrass = 60,
        UndergroundJunglePlants,
        UndergroundJungleVines,
        Sapphire,
        Ruby,
        Emerald = 65,
        Topaz,
        Amethyst,
        Diamond,
        UndergroundJungleThorns, // Credit Dr VideoGames 0031

		UndergroundMushroomGrass = 70,
        UndergroundMushroomPlants,
        UndergroundMushroomTrees,
        Plants2,
        Plants3,
        BlockObsidian = 75,
        BlockHellstone,
        Hellforge,
        DecorativePot,
        Bed,

        /*
         *  81-85 are "important tiles", and have U/V data, 80 doesn't.
            80 is cactus
            81 is coral
            82 to 84 are the various plants in their various stages of growth. 
		    Herb type names are based on the descriptions on the official Wiki.
		    you can use the U coordinate to determine if it's moonglow, or blinkweed, or whatever.
            85 is the tombstone.*/

        //1.0.5
        Cactus = 80,
        Coral = 81,
        HerbImmature = 82,
        HerbMature = 83,
        HerbBlooming = 84,
        Tombstone = 85,

        Unknown,




        // Additonal Placed Out of range of byte
        Spawn = 256,
		ArmsDealer = 257,
		Clothier = 258,
		Demolitionist = 259,
		Dryad = 260,
		Guide = 261,
		Merchant = 262,
		Nurse = 263,
		OldMan = 264,

        Sky = 265,
        Water = 266,
        Lava = 267,

        WallStone = 268,
        WallDirt,
        WallStone2,
        WallWood,
        WallBrick,
        WallRed,
        WallBlue,
        WallGreen,
        WallPink,
        WallGold,
        WallSilver,
        WallCopper,
        WallHellstone,
        WallBackground,
    }

}
