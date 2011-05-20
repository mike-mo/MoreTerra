using System.Drawing;
using System.IO;
namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing.Imaging;  

    public class WorldReader
    {
        private long tileOffset;
        private string filePath;
        private FileStream stream;
        private BinaryReader reader;

        public WorldReader(string filePath)
        {
            this.filePath = filePath;
            this.stream = new FileStream(this.filePath, FileMode.Open);
            this.reader = new BinaryReader(stream);
            tileOffset = 0;

            //tileTypeDefs = new TileInfo[256];
            //for (int i = 0; i <= 255; i++)
            //{
            //    tileTypeDefs[i] = new TileInfo { isImportant = false, tileType = TileType.Unknown };
            //}
            

            //tileTypeDefs[0] = new TileInfo { isImportant = false, tileType = TileType.Dirt };
            //tileTypeDefs[1] = new TileInfo { isImportant = false, tileType = TileType.Stone };
            //tileTypeDefs[2] = new TileInfo { isImportant = false, tileType = TileType.Grass };
            //tileTypeDefs[3] = new TileInfo { isImportant = true, tileType = TileType.Plant };
            //tileTypeDefs[4] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Torch
            //tileTypeDefs[5] = new TileInfo { isImportant = true, tileType = TileType.Wood };

            //tileTypeDefs[6] = new TileInfo { isImportant = false, tileType = TileType.Iron };
            //tileTypeDefs[7] = new TileInfo { isImportant = false, tileType = TileType.Copper };
            //tileTypeDefs[8] = new TileInfo { isImportant = false, tileType = TileType.Gold };
            //tileTypeDefs[9] = new TileInfo { isImportant = false, tileType = TileType.Silver };

            //tileTypeDefs[10] = new TileInfo { isImportant = true, tileType = TileType.ManMade };
            //tileTypeDefs[11] = new TileInfo { isImportant = true, tileType = TileType.Stone };
            //tileTypeDefs[12] = new TileInfo { isImportant = true, tileType = TileType.Dirt };
            //tileTypeDefs[13] = new TileInfo { isImportant = true, tileType = TileType.Unknown };
            //tileTypeDefs[14] = new TileInfo { isImportant = true, tileType = TileType.Unknown };
            //tileTypeDefs[15] = new TileInfo { isImportant = true, tileType = TileType.Unknown };
            //tileTypeDefs[16] = new TileInfo { isImportant = true, tileType = TileType.Unknown };
            //tileTypeDefs[17] = new TileInfo { isImportant = true, tileType = TileType.ManMade }; // Furnace
            //tileTypeDefs[18] = new TileInfo { isImportant = true, tileType = TileType.ManMade }; // Crafting Table
            //tileTypeDefs[19] = new TileInfo { isImportant = false, tileType = TileType.Unknown };

            //tileTypeDefs[20] = new TileInfo { isImportant = true, tileType = TileType.Unknown };
            //tileTypeDefs[21] = new TileInfo { isImportant = true, tileType = TileType.Important }; // Chest
            //tileTypeDefs[22] = new TileInfo { isImportant = false, tileType = TileType.CorruptionPlants }; // Spikey Plants
            //tileTypeDefs[23] = new TileInfo { isImportant = false, tileType = TileType.CorruptionGrass }; // Grass 2
            //tileTypeDefs[24] = new TileInfo { isImportant = true, tileType = TileType.CorruptionGrass }; // Corruption Grass
            //tileTypeDefs[25] = new TileInfo { isImportant = false, tileType = TileType.Corruption }; // Corruption Blocks
            //tileTypeDefs[26] = new TileInfo { isImportant = true, tileType = TileType.Important }; // Altars
            //tileTypeDefs[27] = new TileInfo { isImportant = true, tileType = TileType.Plant }; // Sunflower
            //tileTypeDefs[28] = new TileInfo { isImportant = true, tileType = TileType.Important }; // Pots
            //tileTypeDefs[29].isImportant = true;
            //tileTypeDefs[30] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Wood Blocks
            //tileTypeDefs[31] = new TileInfo { isImportant = true, tileType = TileType.Important }; // Shadow Orbs
            //tileTypeDefs[33].isImportant = true;
            //tileTypeDefs[34].isImportant = true;
            //tileTypeDefs[35].isImportant = true;
            //tileTypeDefs[36].isImportant = true;

            //tileTypeDefs[38] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Gray Bricks
            //tileTypeDefs[39] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Red Bricks
            //tileTypeDefs[40] = new TileInfo { isImportant = false, tileType = TileType.Clay };
            //tileTypeDefs[41] = new TileInfo { isImportant = false, tileType = TileType.Dungeon };
            //tileTypeDefs[42].isImportant = true;
            //tileTypeDefs[43] = new TileInfo { isImportant = false, tileType = TileType.Dungeon };
            //tileTypeDefs[44] = new TileInfo { isImportant = false, tileType = TileType.Dungeon };
            //tileTypeDefs[45] = new TileInfo { isImportant = false, tileType = TileType.ManMade };
            //tileTypeDefs[46] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Other Construction Materials
            //tileTypeDefs[47] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // Floating House Building Materials
            
            //tileTypeDefs[50].isImportant = true;
            //tileTypeDefs[51] = new TileInfo { isImportant = false, tileType = TileType.Web };
            //tileTypeDefs[52] = new TileInfo { isImportant = false, tileType = TileType.Plant };
            //tileTypeDefs[53] = new TileInfo { isImportant = false, tileType = TileType.Sand };
            //tileTypeDefs[54] = new TileInfo { isImportant = false, tileType = TileType.Sand };
            //tileTypeDefs[55].isImportant = true;
            //tileTypeDefs[57] = new TileInfo { isImportant = false, tileType = TileType.Ash };
            //tileTypeDefs[58] = new TileInfo { isImportant = false, tileType = TileType.LavaRock };
            //tileTypeDefs[59] = new TileInfo { isImportant = false, tileType = TileType.Mud };

            //tileTypeDefs[60] = new TileInfo { isImportant = false, tileType = TileType.UnderGroundJungle };
            //tileTypeDefs[61] = new TileInfo { isImportant = true, tileType = TileType.UnderGroundJungle };
            //tileTypeDefs[62] = new TileInfo { isImportant = false, tileType = TileType.UnderGroundJungle };
            //tileTypeDefs[63] = new TileInfo { isImportant = false, tileType = TileType.UnderGroundJungle };

            //tileTypeDefs[70] = new TileInfo { isImportant = false, tileType = TileType.UnderGroundJunlePlants };
            //tileTypeDefs[71] = new TileInfo { isImportant = true, tileType = TileType.UnderGroundJunlePlants };
            //tileTypeDefs[72] = new TileInfo { isImportant = true, tileType = TileType.Wood }; // Underground Mushroom Trees
            //tileTypeDefs[73] = new TileInfo { isImportant = true, tileType = TileType.Wood }; // Underground Mushroom Trees
            //tileTypeDefs[74].isImportant = true;
            //tileTypeDefs[76] = new TileInfo { isImportant = false, tileType = TileType.ManMade }; // hell Bricks?
            //tileTypeDefs[77].isImportant = true;
            //tileTypeDefs[78].isImportant = true;
            //tileTypeDefs[79].isImportant = true;

            ////tileTypeDefs[0x3f].tileType = TileType.Stone;
            ////tileTypeDefs[0x40].tileType = TileType.Stone;
            ////tileTypeDefs[0x41].tileType = TileType.Stone;
            ////tileTypeDefs[0x42].tileType = TileType.Stone;
            ////tileTypeDefs[0x43].tileType = TileType.Stone;
            ////tileTypeDefs[0x44].tileType = TileType.Stone;


            //tileColorDefs = new Dictionary<TileType, Color>();
            //tileColorDefs.Add(TileType.Dirt, Color.FromArgb(175, 131, 101));
            //tileColorDefs.Add(TileType.Stone, Color.FromArgb(117, 117, 117));
            //tileColorDefs.Add(TileType.Resource, Color.Red);
            //tileColorDefs.Add(TileType.Sand, Color.FromArgb(255, 201, 56));
            //tileColorDefs.Add(TileType.Clay, Color.FromArgb(255, 92, 84));
            //tileColorDefs.Add(TileType.Copper, Color.FromArgb(167, 80, 23));
            //tileColorDefs.Add(TileType.Iron, Color.FromArgb(140, 147, 148));
            //tileColorDefs.Add(TileType.Silver, Color.FromArgb(251, 255, 186));
            //tileColorDefs.Add(TileType.Gold, Color.FromArgb(209, 192, 1));
            //tileColorDefs.Add(TileType.Wood, Color.DarkGreen);
            //tileColorDefs.Add(TileType.Corruption, Color.FromArgb(65, 58, 74));
            //tileColorDefs.Add(TileType.CorruptionGrass, Color.FromArgb(89, 90, 136));
            //tileColorDefs.Add(TileType.CorruptionPlants, Color.FromArgb(80, 83, 126));
            //tileColorDefs.Add(TileType.Grass, Color.FromArgb(10, 71, 26));
            //tileColorDefs.Add(TileType.Plant, Color.FromArgb(26, 198, 86));
            //tileColorDefs.Add(TileType.Water, Color.Blue);
            //tileColorDefs.Add(TileType.Lava, Color.FromArgb(195, 45, 7));
            //tileColorDefs.Add(TileType.Wall, Color.FromArgb(84, 33, 0));
            //tileColorDefs.Add(TileType.Mud, Color.FromArgb(73, 54, 58));
            //tileColorDefs.Add(TileType.Dungeon, Color.Purple);
            //tileColorDefs.Add(TileType.ManMade, Color.Yellow);
            //tileColorDefs.Add(TileType.Web, Color.FromArgb(233, 217, 224));
            //tileColorDefs.Add(TileType.Important, Color.Red);
            //tileColorDefs.Add(TileType.UnderGroundJungle, Color.FromArgb(168, 255, 7));
            //tileColorDefs.Add(TileType.UnderGroundJunlePlants, Color.FromArgb(158, 73, 255));
            //tileColorDefs.Add(TileType.Ash, Color.FromArgb(60, 59, 66));
            //tileColorDefs.Add(TileType.LavaRock, Color.FromArgb(255, 148, 99));
            //tileColorDefs.Add(TileType.LavaRock2, Color.FromArgb(255, 71, 95));
            //tileColorDefs.Add(TileType.Mushroom, Color.FromArgb(187, 253, 14));
            //tileColorDefs.Add(TileType.Unknown, Color.Magenta);
        }

        public void SeekToTiles()
        {
            this.stream.Seek(this.tileOffset, SeekOrigin.Begin);
        }

        public WorldHeader ReadHeader()
        {
            // Reset to origin
            stream.Seek(0, SeekOrigin.Begin);

            WorldHeader header = new WorldHeader
            {
                ReleaseNumber = this.reader.ReadInt32(),
                Name = this.reader.ReadString(),
                Id = this.reader.ReadInt32(),
                WorldCoords = new Rect(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32()),
                MaxTiles = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
                SpawnPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
                SurfaceLevel = this.reader.ReadDouble(),
                RockLayer = this.reader.ReadDouble(),
                TemporaryTime = this.reader.ReadDouble(),
                IsDayTime = this.reader.ReadBoolean(),
                MoonPhase = this.reader.ReadInt32(),
                IsBloodMoon = this.reader.ReadBoolean(),
                DungeonPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
                IsBoss1Dead = this.reader.ReadBoolean(),
                IsBoss2Dead = this.reader.ReadBoolean(),
                IsBoss3Dead = this.reader.ReadBoolean(),
                IsShadowOrbSmashed = this.reader.ReadBoolean(),
                IsMeteorSpawned = this.reader.ReadBoolean(),
                ShadowOrbsSmashed = this.reader.ReadByte(),
                InvasionDelay = this.reader.ReadInt32(),
                InvasionSize = this.reader.ReadInt32(),
                InvasionType = this.reader.ReadInt32(),
                InvasionPointX = this.reader.ReadDouble(),
            };

            this.tileOffset = stream.Position;
            return header;
        }

        public TileType GetNextTile()
        {
            bool isTileActive = reader.ReadBoolean();
            TileType tileType = TileType.Unknown;
            byte blockType = 0x00;
            if (isTileActive)
            {
                blockType = reader.ReadByte();
                if (WorldMapper.tileTypeDefs[blockType].IsImportant)
                {
                    reader.ReadInt16();
                    reader.ReadInt16();
                }
                tileType = WorldMapper.tileTypeDefs[blockType].TileType;
            }
            else
            {
                tileType = TileType.Sky;
            }
            bool isLighted = reader.ReadBoolean();
            bool isWall = reader.ReadBoolean();
            if (isWall)
            {
                byte wallType = reader.ReadByte();
                if (tileType == TileType.Unknown || tileType == TileType.Sky)
                {
                    tileType = WorldMapper.tileTypeDefs[(int)wallType + Constants.WallOffset].TileType;
                }
            }
            bool isLiquid = reader.ReadBoolean();
            if (isLiquid)
            {
                byte liquidLevel = reader.ReadByte();
                bool isLava = reader.ReadBoolean();
                if (isWall || tileType == TileType.Sky)
                {
                    tileType = isLava ? TileType.Lava : TileType.Water;
                }
                
            }
            return tileType;
        }

        public void Close()
        {
            this.reader.Close();
        }
  
        //public void CreatePreviewImage(string outputFilePath)
        //{
        //    int maxX = (int)Header.MaxTiles.Y;
        //    int maxY = (int)Header.MaxTiles.X;
        //    //byte[,] pixels = new byte[cols, rows];

        //    Bitmap bitmap = new Bitmap(maxX, maxY);
        //    Graphics g2 = Graphics.FromImage((Image)bitmap);
        //    g2.FillRectangle(Brushes.SkyBlue, 0, 0, bitmap.Width, bitmap.Height);
        //    Dictionary<byte, Color> randomColors = new Dictionary<byte, Color>();
        //    Random random = new Random();
        //    for (int col = 0; col < maxX; col++)
        //    {
        //        for (int row = 0; row < maxY; row++)
        //        {
        //            bool isTileActive = reader.ReadBoolean();
        //            TileType tileType = TileType.Unknown;
        //            byte blockType = 0x00;
        //            if (isTileActive)
        //            {
        //                blockType = reader.ReadByte();
        //                if (tileTypeDefs[blockType].isImportant)
        //                {
        //                    reader.ReadInt16();
        //                    reader.ReadInt16();
        //                }
        //                tileType = tileTypeDefs[blockType].tileType;
        //            }
        //            else
        //            {
        //                tileType = TileType.Sky;
        //            }
        //            bool isLighted = reader.ReadBoolean();
        //            bool isWall = reader.ReadBoolean();
        //            if (isWall)
        //            {
        //                byte wallType = reader.ReadByte();
        //                if (tileType == TileType.Unknown)
        //                {
        //                    tileType = TileType.Wall;    
        //                }
                        
        //               // bitmap.SetPixel(col, row, Color.PowderBlue);
        //            }
        //            bool isLiquid = reader.ReadBoolean();
                    
        //            if (isLiquid)
        //            {
        //                byte liquidLevel = reader.ReadByte();
        //                bool isLava = reader.ReadBoolean();
        //                tileType = isLava ? TileType.Lava : TileType.Water;
        //               // bitmap.SetPixel(col, row, isLava ? Color.Pink : Color.Blue);
        //            }
        //            //if (tileType != TileType.Lava || tileType != TileType.Wall || tileType != TileType.Water)
        //            //{
        //            //    if (!randomColors.ContainsKey(blockType))
        //            //    {

        //            //        int r = random.Next(0, 255);
        //            //        int g = random.Next(0, 255);
        //            //        int b = random.Next(0, 255);

        //            //        randomColors.Add(blockType, Color.FromArgb(r, g, b));
        //            //    }
        //            //    bitmap.SetPixel(col, row, randomColors[blockType]);
        //            //}
        //            //if (tileType == TileType.Unknown)
        //            //{
        //            //    if (!randomColors.ContainsKey(blockType))
        //            //    {
        //            //        int r = random.Next(0, 255);
        //            //        int g = random.Next(0, 255);
        //            //        int b = random.Next(0, 255);
        //            //        randomColors.Add(blockType, Color.FromArgb(r, g, b));
        //            //    }
        //            //    bitmap.SetPixel(col, row, randomColors[blockType]);
        //            //}
        //            if (tileType != TileType.Sky)
        //            {
        //                bitmap.SetPixel(col, row, tileColorDefs[tileType]);
        //            }
        //            else
        //            {
        //                // Don't draw its sky
        //            }
        //        }
        //    }


        //    //TextWriter fs = new StreamWriter(@"C:\Users\Frank\Documents\My Games\Terraria\Worlds\dump.txt");
        //    //foreach (var pair in randomColors)
        //    //{
        //    //    fs.WriteLine(string.Format("{0}, ({1}, {2}, {3})", pair.Key, pair.Value.R, pair.Value.G, pair.Value.B));
        //    ////}
        //    //fs.Close();


        //    bitmap.Save(outputFilePath, ImageFormat.Png);
        //    reader.Close();
        //}
    }
}
