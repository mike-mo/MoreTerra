namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public class WorldMapper
    {
        // It should be Tiletype enum but my hand hurts too much to fix.
        public static Dictionary<int, TileProperties> tileTypeDefs;

        private List<Chest> chests;
        private Dictionary<TileType, List<Point>> tilesToSymbolize;

        private WorldHeader worldHeader;
        private WorldReader reader;

        public int progress = 0;


        public WorldMapper()
        {
            tileTypeDefs = new Dictionary<int, TileProperties>(255);
            chests = new List<Chest>();
        }

        public WorldHeader Header
        {
            get
            {
                return this.worldHeader;
            }
        }

        public void Initialize()
        {
            // :OHGOD:
            tileTypeDefs[0] = new TileProperties(TileType.Dirt, false, Constants.Colors.DIRT);
            tileTypeDefs[1] = new TileProperties(TileType.Stone, false, Constants.Colors.STONE);
            tileTypeDefs[2] = new TileProperties(TileType.Grass, false, Constants.Colors.GRASS);
            tileTypeDefs[3] = new TileProperties(TileType.Plants, true, Constants.Colors.PLANTS);
            tileTypeDefs[4] = new TileProperties(TileType.Torches, false, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[5] = new TileProperties(TileType.Trees, true, Constants.Colors.WOOD);
            tileTypeDefs[6] = new TileProperties(TileType.Iron, false, Constants.Colors.IRON);
            tileTypeDefs[7] = new TileProperties(TileType.Copper, false, Constants.Colors.COPPER);
            tileTypeDefs[8] = new TileProperties(TileType.Gold, false, Constants.Colors.GOLD);
            tileTypeDefs[9] = new TileProperties(TileType.Silver, false, Constants.Colors.SILVER);
            
            tileTypeDefs[10] = new TileProperties(TileType.Door1, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[11] = new TileProperties(TileType.Door2, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[12] = new TileProperties(TileType.Heart, true, Constants.Colors.IMPORTANT, true);
            tileTypeDefs[13] = new TileProperties(TileType.Bottles, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[14] = new TileProperties(TileType.Table, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[15] = new TileProperties(TileType.Chair, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[16] = new TileProperties(TileType.Anvil, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[17] = new TileProperties(TileType.Furnance, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[18] = new TileProperties(TileType.CraftingTable, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[19] = new TileProperties(TileType.WoodenPlatform, false, Constants.Colors.WOOD);
            tileTypeDefs[20] = new TileProperties(TileType.PlantsDecorative, true, Constants.Colors.PLANTS);

            tileTypeDefs[21] = new TileProperties(TileType.Chest, true, Constants.Colors.IMPORTANT, true);
            tileTypeDefs[22] = new TileProperties(TileType.CorruptionStone1, false, Constants.Colors.CORRUPTION_STONE);
            tileTypeDefs[23] = new TileProperties(TileType.CorruptionGrass, false, Constants.Colors.CORRUPTION_GRASS);
            tileTypeDefs[24] = new TileProperties(TileType.CorruptionPlants, true, Constants.Colors.CORRUPTION_GRASS);
            tileTypeDefs[25] = new TileProperties(TileType.CorruptionStone2, false, Constants.Colors.CORRUPTION_STONE2);
            tileTypeDefs[26] = new TileProperties(TileType.Altar, true, Constants.Colors.IMPORTANT, true);
            tileTypeDefs[27] = new TileProperties(TileType.Sunflower, true, Constants.Colors.PLANTS);
            tileTypeDefs[28] = new TileProperties(TileType.Pot, true, Constants.Colors.IMPORTANT);
            tileTypeDefs[29] = new TileProperties(TileType.PiggyBank, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[30] = new TileProperties(TileType.BlockWood, false, Constants.Colors.WOOD_BLOCK);

            tileTypeDefs[31] = new TileProperties(TileType.ShadowOrb, true, Constants.Colors.IMPORTANT, true);
            tileTypeDefs[32] = new TileProperties(TileType.CorruptionVines, false, Constants.Colors.CORRUPTION_VINES);
            tileTypeDefs[33] = new TileProperties(TileType.Candle, true, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[34] = new TileProperties(TileType.ChandlerCopper, true, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[35] = new TileProperties(TileType.ChandlerSilver, true, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[36] = new TileProperties(TileType.ChandlerGold, true, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[37] = new TileProperties(TileType.Meterorite, false, Constants.Colors.METEORITE);
            tileTypeDefs[38] = new TileProperties(TileType.BlockStone, false, Constants.Colors.BLOCK);
            tileTypeDefs[39] = new TileProperties(TileType.BlockRedStone, false, Constants.Colors.BLOCK);
            tileTypeDefs[40] = new TileProperties(TileType.Clay, false, Constants.Colors.CLAY);

            tileTypeDefs[41] = new TileProperties(TileType.BlockBlueStone, false, Constants.Colors.DUNGEON_BLUE);
            tileTypeDefs[42] = new TileProperties(TileType.LightGlobe, true, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[43] = new TileProperties(TileType.BlockGreenStone, false, Constants.Colors.DUNGEON_GREEN);
            tileTypeDefs[44] = new TileProperties(TileType.BlockPinkStone, false, Constants.Colors.DUNGEON_PINK);
            tileTypeDefs[45] = new TileProperties(TileType.BlockGold, false, Constants.Colors.BLOCK);
            tileTypeDefs[46] = new TileProperties(TileType.BlockSilver, false, Constants.Colors.BLOCK);
            tileTypeDefs[47] = new TileProperties(TileType.BlockCopper, false, Constants.Colors.BLOCK);
            tileTypeDefs[48] = new TileProperties(TileType.Spikes, false, Constants.Colors.SPIKES);
            tileTypeDefs[49] = new TileProperties(TileType.CandleBlue, false, Constants.Colors.LIGHT_SOURCE);
            tileTypeDefs[50] = new TileProperties(TileType.Books, true, Constants.Colors.DECORATIVE);

            tileTypeDefs[51] = new TileProperties(TileType.Web, false, Constants.Colors.WEB);
            tileTypeDefs[52] = new TileProperties(TileType.Vines, false, Constants.Colors.PLANTS);
            tileTypeDefs[53] = new TileProperties(TileType.Sand, false, Constants.Colors.SAND);
            tileTypeDefs[54] = new TileProperties(TileType.Glass, false, Constants.Colors.DECORATIVE);
            tileTypeDefs[55] = new TileProperties(TileType.Signs, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[56] = new TileProperties(TileType.Obsidian, false, Constants.Colors.OBSIDIAN);
            tileTypeDefs[57] = new TileProperties(TileType.Ash, false, Constants.Colors.ASH);
            tileTypeDefs[58] = new TileProperties(TileType.Hellstone, false, Constants.Colors.HELLSTONE);
            tileTypeDefs[59] = new TileProperties(TileType.Mud, false, Constants.Colors.MUD);
            tileTypeDefs[60] = new TileProperties(TileType.UndergroundJungleGrass, false, Constants.Colors.UNDERGROUNDJUNGLE_GRASS);

            tileTypeDefs[61] = new TileProperties(TileType.UndergroundJunglePlants, true, Constants.Colors.UNDERGROUNDJUNGLE_PLANTS);
            tileTypeDefs[62] = new TileProperties(TileType.UndergroundJungleVines, false, Constants.Colors.UNDERGROUNDJUNGLE_VINES);
            tileTypeDefs[63] = new TileProperties(TileType.Sapphire, false, Constants.Colors.GEMS, true);
            tileTypeDefs[64] = new TileProperties(TileType.Ruby, false, Constants.Colors.GEMS, true);
            tileTypeDefs[65] = new TileProperties(TileType.Emerald, false, Constants.Colors.GEMS, true);
            tileTypeDefs[66] = new TileProperties(TileType.Topaz, false, Constants.Colors.GEMS, true);
            tileTypeDefs[67] = new TileProperties(TileType.Amethyst, false, Constants.Colors.GEMS, true);
            tileTypeDefs[68] = new TileProperties(TileType.Diamond, false, Constants.Colors.GEMS, true);
            tileTypeDefs[69] = new TileProperties(TileType.UndergroundJungleThorns, false, Constants.Colors.UNDERGROUNDJUNGLE_THORNS);
            tileTypeDefs[70] = new TileProperties(TileType.UndergroundMushroomGrass, false, Constants.Colors.UNDERGROUNDMUSHROOM_GRASS);

            tileTypeDefs[71] = new TileProperties(TileType.UndergroundMushroomPlants, true, Constants.Colors.UNDERGROUNDMUSHROOM_PLANTS);
            tileTypeDefs[72] = new TileProperties(TileType.UndergroundMushroomTrees, true, Constants.Colors.UNDERGROUNDMUSHROOM_TREES);
            tileTypeDefs[73] = new TileProperties(TileType.Plants2, true, Constants.Colors.PLANTS);
            tileTypeDefs[74] = new TileProperties(TileType.Plants3, true, Constants.Colors.PLANTS);
            tileTypeDefs[75] = new TileProperties(TileType.BlockObsidian, false, Constants.Colors.BLOCK);
            tileTypeDefs[76] = new TileProperties(TileType.BlockHellstone, false, Constants.Colors.BLOCK);
            tileTypeDefs[77] = new TileProperties(TileType.UnderworldFurnance, true, Constants.Colors.IMPORTANT);
            tileTypeDefs[78] = new TileProperties(TileType.DecorativePot, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[79] = new TileProperties(TileType.Bed, true, Constants.Colors.DECORATIVE);
            tileTypeDefs[80] = new TileProperties(TileType.Unknown, false, Constants.Colors.UNKNOWN);

            for(int i = 80; i < 255; i++)
            {
                tileTypeDefs[i] = new TileProperties(TileType.Unknown, false, Color.Magenta);
            }

            tileTypeDefs[256] = new TileProperties(TileType.Sky, false, Constants.Colors.SKY);
            tileTypeDefs[257] = new TileProperties(TileType.Water, false, Constants.Colors.WATER);
            tileTypeDefs[258] = new TileProperties(TileType.Lava, false, Constants.Colors.LAVA);

            // Walls
            tileTypeDefs[259] = new TileProperties(TileType.WallStone, false, Constants.Colors.WALL_STONE);
            tileTypeDefs[260] = new TileProperties(TileType.WallDirt, false, Constants.Colors.WALL_DIRT);
            tileTypeDefs[261] = new TileProperties(TileType.WallStone2, false, Constants.Colors.WALL_STONE2);
            tileTypeDefs[262] = new TileProperties(TileType.WallWood, false, Constants.Colors.WALL_WOOD);
            tileTypeDefs[263] = new TileProperties(TileType.WallBrick, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[264] = new TileProperties(TileType.WallRed, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[265] = new TileProperties(TileType.WallBlue, false, Constants.Colors.WALL_DUNGEON_BLUE);
            tileTypeDefs[266] = new TileProperties(TileType.WallGreen, false, Constants.Colors.WALL_DUNGEON_GREEN);
            tileTypeDefs[267] = new TileProperties(TileType.WallPink, false, Constants.Colors.WALL_DUNGEON_PINK);
            tileTypeDefs[268] = new TileProperties(TileType.WallGold, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[269] = new TileProperties(TileType.WallSilver, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[270] = new TileProperties(TileType.WallCopper, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[271] = new TileProperties(TileType.WallHellstone, false, Constants.Colors.WALL_BRICK);
            tileTypeDefs[272] = new TileProperties(TileType.WallHellstone, false, Constants.Colors.WALL_BACKGROUND);
        }

        public void OpenWorld(string worldPath)
        {
            reader = new WorldReader(worldPath);
            this.worldHeader = reader.ReadHeader();
        }

        public void ReadChests()
        {
            progress = 0;

            this.chests = new List<Chest>();

            reader.SeekToChests();

            // Read the Chests
            for (int i = 0; i < Constants.ChestMaxNumber; i++)
            {
                Chest chest = this.reader.GetNextChest(i);

                if (chest == null) continue;
                else this.chests.Add(chest);
            }

            progress = 100;
        }

        public void CreatePreviewPNG(string outputPngPath)
        {
            progress = 0;

            reader.SeekToTiles();

            // Reset Symbol List
            this.tilesToSymbolize = new Dictionary<TileType, List<Point>>();
      
            int maxX = (int)Header.MaxTiles.Y;
            int maxY = (int)Header.MaxTiles.X;

            Bitmap bitmap = new Bitmap(maxX, maxY);
            Graphics graphicsHandle = Graphics.FromImage((Image)bitmap);
            graphicsHandle.FillRectangle(new SolidBrush(Constants.Colors.SKY), 0, 0, bitmap.Width, bitmap.Height);

            Dictionary<byte, Color> randomColors = new Dictionary<byte, Color>();
 
            TileProperties properties;
            TileType tileType;

            for (int col = 0; col < maxX; col++)
            {
                progress = (int)(((float)col / (float)maxX) * 100.0f);

                for (int row = 0; row < maxY; row++)
                {
                    tileType = reader.GetNextTile();
                    if (tileType == TileType.Sky && row > (int)this.Header.SurfaceLevel)
                    {
                        tileType = TileType.WallBackground;
                    }

                    // Skip Walls
                    if (!SettingsManager.Instance.IsWallDrawable && tileType >= TileType.WallStone) continue;

                    // Skip chests because we read the coordinates later
                    if (tileType == TileType.Chest) continue;

                    properties = tileTypeDefs[(int)tileType];

                    if (properties.HasSymbol)
                    {
                        if (!tilesToSymbolize.ContainsKey(tileType)) tilesToSymbolize.Add(tileType, new List<Point>());
         
                        tilesToSymbolize[tileType].Add(new Point(col, row));
                    }
                    else //Set Pixel Value
                    {
                        bitmap.SetPixel(col, row, tileTypeDefs[(int)tileType].Colour);
                    }
                }
            }


            // Add an empty list of Chests to populate
            tilesToSymbolize.Add(TileType.Chest, new List<Point>());

            Dictionary<string, bool> itemFilters = SettingsManager.Instance.FilterItemsStates;
            // Read the Chests
            for (int i = 0; i < Constants.ChestMaxNumber; i++)
            {
                Chest chest = this.reader.GetNextChest(i);
               
                if (chest == null) continue;
               
                this.chests.Add(chest);

                // Find out if the chest is relevant to our interests
                foreach (Item item in chest.Items)
                {
                    // If we want it
                    if (itemFilters.ContainsKey(item.Name) && itemFilters[item.Name] == true)
                    {
                        // Draw the symbol
                        tilesToSymbolize[TileType.Chest].Add(chest.Coordinates);
                        break;
                    }
                }
            }


            // Add Spawn
            tilesToSymbolize.Add(TileType.Spawn, new List<Point>());
            tilesToSymbolize[TileType.Spawn].Add(new Point(this.Header.SpawnPoint.X, this.Header.SpawnPoint.Y));

            // Draw Symbols
            foreach (KeyValuePair<TileType, List<Point>> kv in tilesToSymbolize)
            {
                bool isSymbolViewable = SettingsManager.Instance.IsSymbolViewable(kv.Key);

                if (isSymbolViewable)
                {
                    Bitmap symbolBitmap = ResourceManager.Instance.GetSymbol(kv.Key);
                    foreach (Point p in kv.Value)
                    {
                        int x = Math.Max((int)p.X - (symbolBitmap.Width / 2), 0);
                        int y = Math.Max((int)p.Y - (symbolBitmap.Height / 2), 0);
                        if (x > maxX || y > maxY)
                        {
                            continue;
                        }
                        graphicsHandle.DrawImage(symbolBitmap, x, y);
                    }
                }

            }
            bitmap.Save(outputPngPath, ImageFormat.Png);
            progress = 100;
        }

        public void CloseWorld()
        {
            reader.Close();
        }

        public List<Chest> Chests
        {
            get
            {
                if (this.chests.Count == 0) ReadChests();
                return this.chests;
            }
        }
        

    }
}
