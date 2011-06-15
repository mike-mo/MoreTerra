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
        private long chestOffset;

        private string filePath;
        private FileStream stream;
        private BinaryReader reader;

        public WorldReader(string filePath)
        {
            this.filePath = filePath;
            this.stream = new FileStream(this.filePath, FileMode.Open);
            this.reader = new BinaryReader(stream);
            tileOffset = 0;
            chestOffset = 0;
        }

        public void SeekToTiles()
        {
            this.stream.Seek(this.tileOffset, SeekOrigin.Begin);
        }

        public WorldHeader ReadHeader()
        {
            // Reset to origin
            stream.Seek(0, SeekOrigin.Begin);

            int releaseNumber = this.reader.ReadInt32();
            WorldHeader header;
            if (releaseNumber == 38)
            {
                header = new WorldHeader
                {
                    ReleaseNumber = releaseNumber,
                    Name = this.reader.ReadString(),
         //           Id = this.reader.ReadInt32(),
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
            }
            else
            {
                header = new WorldHeader
                {
                    ReleaseNumber = releaseNumber,
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

            }

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
                    if (!WorldMapper.tileTypeDefs.ContainsKey((int)wallType + Constants.WallOffset))
                    {
                        tileType = TileType.Unknown;
                    }
                    else
                    {
                        tileType = WorldMapper.tileTypeDefs[(int)wallType + Constants.WallOffset].TileType;
                    }
                    
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

        /// <summary>
        /// Gets the next chest, if the next item is not a chest, return null
        /// </summary>
        /// <returns></returns>
        public Chest GetNextChest(int chestId)
        {
            bool isChest = reader.ReadBoolean();
            if (!isChest)
            {
                return null;
            }
            Chest chest = new Chest(chestId, new Point(this.reader.ReadInt32(), this.reader.ReadInt32()));

            // Iterate through items contained within chest
            for (int i = 0; i < Constants.ChestMaxItems; i++)
            {
                byte count = this.reader.ReadByte();
                if ((int)count > 0)
                {
                    chest.AddItem(new Item(reader.ReadString(), (int)count));
                }
            }
            return chest;
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
