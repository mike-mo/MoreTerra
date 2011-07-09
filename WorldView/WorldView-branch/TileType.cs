namespace MoreTerra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum TileType
    {
        Dirt,
        Stone,
        Grass,
        Plants,
        Torches,
        Trees,
        Iron,
        Copper,
        Gold,
        Silver,
        Door1,
        Door2,
        Heart,
        Bottles,
        Table,
        Chair,
        Anvil,
        Furnance,
        CraftingTable,
        WoodenPlatform,
        PlantsDecorative,
        Chest,
        CorruptionStone1,
        CorruptionGrass,
        CorruptionPlants,
        CorruptionStone2,
        Altar,
        Sunflower,
        Pot,
        PiggyBank,
        BlockWood,
        ShadowOrb,
        CorruptionVines,
        Candle,
        ChandlerCopper,
        ChandlerSilver,
        ChandlerGold,
        Meterorite, // Credit Vib Rib
        BlockStone,
        BlockRedStone,
        Clay,
        BlockBlueStone,
        LightGlobe,
        BlockGreenStone,
        BlockPinkStone,
        BlockGold,
        BlockSilver,
        BlockCopper,
        Spikes,
        CandleBlue,
        Books,
        Web,
        Vines,
        Sand,
        Glass,
        Signs,
        Obsidian,
        Ash, // Credit Infinite Monkeys
        Hellstone, // Credit Vib Rib
        Mud,
        UndergroundJungleGrass,
        UndergroundJunglePlants,
        UndergroundJungleVines,
        Sapphire,
        Ruby,
        Emerald,
        Topaz,
        Amethyst,
        Diamond,
        UndergroundJungleThorns, // Credit Dr VideoGames 0031
        UndergroundMushroomGrass,
        UndergroundMushroomPlants,
        UndergroundMushroomTrees,
        Plants2,
        Plants3,
        BlockObsidian,
        BlockHellstone,
        UnderworldFurnance,
        DecorativePot,
        Bed,

        /*
         *  81-85 are "important tiles", and have U/V data, 80 doesn't.
            80 is cactus
            81 is coral
            82 to 84 are the various plants in their various stages of growth. I'm calling 82 "herb sprouts", 83 "herb stalks", and 84 "herbs".. you can use the U coordinate to determine if it's moonglow, or blinkweed, or whatever.
            85 is the tombstone.*/

        //1.0.5
        Cactus = 80,
        Coral = 81,
        HerbSprout = 82,
        HerbStalk = 83,
        Herb = 84,
        Tombstone = 85,

        Unknown,




        // Additonal Placed Out of range of byte
        Spawn = 255,
        Sky = 256,
        Water = 257,
        Lava = 258,

        WallStone = 259,
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
