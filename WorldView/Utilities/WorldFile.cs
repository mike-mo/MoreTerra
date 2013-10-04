using MoreTerra.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MoreTerra
{
    internal class WorldFile
    {
        private BinaryReader reader;
        private int version;

        internal World LoadFile(string worldPath)
        {
            var stream = new FileStream(worldPath, FileMode.Open, FileAccess.Read);
            reader = new BinaryReader(stream);
            
            World world = new World();
            ReadHeader(world);
            var mapTiles = ReadTiles(world);
            ReadChests(world);
            ReadSigns(world);
            ReadNPCs(world);
            ReadNPCNames(world);
            world.TileTypes = mapTiles;
            return world;
        }

        private void ReadHeader(World world)
        {
            version = reader.ReadInt32();
            int x, y, w, h;
            
            var header = new WorldHeader();

            header.ReleaseNumber = version;
            header.Name = reader.ReadString();
            header.Id = reader.ReadInt32();
            x = reader.ReadInt32();
            w = reader.ReadInt32();
            y = reader.ReadInt32();
            h = reader.ReadInt32();

            header.WorldCoords = new Rect(x, w, y, h);

            y = reader.ReadInt32();
            x = reader.ReadInt32();
            header.MaxTiles = new Point(x, y);

            if (version >= 0x3F)
                header.MoonType = (int)reader.ReadByte();

            if (version >= 0x2C)
            {
                //Main.treeX[0] = 
                reader.ReadInt32();
                //Main.treeX[1] = 
                reader.ReadInt32();
                //Main.treeX[2] = 
                reader.ReadInt32();
                //Main.treeStyle[0] = 
                reader.ReadInt32();
                //Main.treeStyle[1] = 
                reader.ReadInt32();
                //Main.treeStyle[2] = 
                reader.ReadInt32();
                //Main.treeStyle[3] = 
                reader.ReadInt32();
            }
            if (version >= 60)
            {
                //Main.caveBackX[0] = 
                reader.ReadInt32();
                //Main.caveBackX[1] = 
                reader.ReadInt32();
                //Main.caveBackX[2] = 
                reader.ReadInt32();
                //Main.caveBackStyle[0] = 
                reader.ReadInt32();
                //Main.caveBackStyle[1] = 
                reader.ReadInt32();
                //Main.caveBackStyle[2] = 
                reader.ReadInt32();
                //Main.caveBackStyle[3] = 
                reader.ReadInt32();
                //Main.iceBackStyle = 
                reader.ReadInt32();
                if (version >= 61)
                {
                    //Main.jungleBackStyle = 
                    reader.ReadInt32();
                    //Main.hellBackStyle = 
                    reader.ReadInt32();
                }
            }
            header.SpawnPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
            header.SurfaceLevel = reader.ReadDouble();
            header.RockLayer = reader.ReadDouble();
            header.TemporaryTime = reader.ReadDouble();
            header.IsDayTime = reader.ReadBoolean();
            header.MoonPhase = reader.ReadInt32();
            header.IsBloodMoon = reader.ReadBoolean();
            header.DungeonPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
            //crimson
            reader.ReadBoolean();
            header.IsBoss1Dead = reader.ReadBoolean();
            header.IsBoss2Dead = reader.ReadBoolean();
            header.IsBoss3Dead = reader.ReadBoolean();
            if (version >= 66)
                //NPC.downedQueenBee = 
                reader.ReadBoolean();
            if (version >= 44)
            {
                //NPC.downedMechBoss1 = 
                reader.ReadBoolean();
                //NPC.downedMechBoss2 = 
                reader.ReadBoolean();
                //NPC.downedMechBoss3 = 
                reader.ReadBoolean();
                //NPC.downedMechBossAny = 
                reader.ReadBoolean();
            }
            if (version >= 64)
            {
                //NPC.downedPlantBoss = 
                reader.ReadBoolean();
                //NPC.downedGolemBoss = 
                reader.ReadBoolean();
            }
            if (version >= 0x24)
            {
                header.IsGoblinSaved = reader.ReadBoolean();
                header.IsWizardSaved = reader.ReadBoolean();
                header.IsMechanicSaved = reader.ReadBoolean();
                header.IsGoblinArmyDefeated = reader.ReadBoolean();
                header.IsClownDefeated = reader.ReadBoolean();
            }
            if (version >= 0x25)
            {
                header.IsFrostDefeated = reader.ReadBoolean();
            }
            if (version >= 56)
                header.IsPiratesDefeated = reader.ReadBoolean();

            header.IsShadowOrbSmashed = reader.ReadBoolean();
            header.IsMeteorSpawned = reader.ReadBoolean();
            header.ShadowOrbsSmashed = reader.ReadByte();

            if (version >= 0x24)
            {
                header.AltarsDestroyed = reader.ReadInt32();
                header.HardMode = reader.ReadBoolean();
            }

            header.InvasionDelay = reader.ReadInt32();
            header.InvasionSize = reader.ReadInt32();
            header.InvasionType = reader.ReadInt32();
            header.InvasionPointX = reader.ReadDouble();

            if (version >= 53)
            {
                var tempRaining = reader.ReadBoolean();
                var tempRainTime = reader.ReadInt32();
                var tempMaxRain = reader.ReadSingle();
            }
            if (version >= 54)
            {
                var oreTier1 = reader.ReadInt32();
                //WorldGen.oreTier2 = 
                reader.ReadInt32();
                //WorldGen.oreTier3 = 
                reader.ReadInt32();
            }

            int style1 = 0;
            int style2 = 0;
            int style3 = 0;
            int style4 = 0;
            int style5 = 0;
            int style6 = 0;
            int style7 = 0;
            int style8 = 0;
            if (version >= 55)
            {
                style1 = (int)reader.ReadByte();
                style2 = (int)reader.ReadByte();
                style3 = (int)reader.ReadByte();
            }
            if (version >= 60)
            {
                style4 = (int)reader.ReadByte();
                style5 = (int)reader.ReadByte();
                style6 = (int)reader.ReadByte();
                style7 = (int)reader.ReadByte();
                style8 = (int)reader.ReadByte();
            }
            if (version >= 60)
            {
                var cloudBGActive = reader.ReadInt32();

            }
            if (version >= 62)
            {
                var  numClouds = reader.ReadInt16();
                var windSpeedSet = reader.ReadSingle();

            }
          //  posTiles = stream.Position;
          //  progressPosition = stream.Position;
            world.Header = header;
        }

        private byte[,] ReadTiles(World world)
        {
            var mapTiles = new Byte[world.Header.MaxTiles.X, world.Header.MaxTiles.Y];
            var importantTiles = new byte[] { 247, 245, 246, 239, 240, 241, 242, 243, 244, 237, 238, 235, 236, 233, 227, 228, 231, 216, 217, 218, 219, 220, 165, 209, 215, 210, 212, 207, 178, 184, 185, 186, 187, 170, 171, 172, 173, 174, 139, 149, 142, 143, 144, 136, 137, 138, 201, 3, 4, 5, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 26, 27, 28, 29, 31, 33, 34, 35, 36, 42, 50, 55, 61, 71, 72, 73, 74, 77, 78, 79, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 101, 102, 103, 104, 105, 100, 106, 110, 113, 114, 125, 126, 128, 129, 132, 133, 134, 135, 141 };

  

            //Read all the tile data using the RLE format.
            for (int column = 0; column < world.Header.MaxTiles.X; column++)
            {
                for (int row = 0; row < world.Header.MaxTiles.Y; row++)
                {
                    byte tileType = 0;

                    //Check if tile is Active.
                    if (reader.ReadBoolean())
                    {
                        tileType = reader.ReadByte();

                        if (importantTiles.Contains(tileType)) //TileProperties.tileTypeDefs[tileType].IsImportant)
                        {
                                reader.ReadInt16();
                                reader.ReadInt16();
                        }
                        if (world.Header.ReleaseNumber >= 48 && reader.ReadBoolean())
                            reader.ReadByte(); //a color?

                    }
                    else
                    {
                        if (row < world.Header.SurfaceLevel)
                            tileType = TileProperties.BackgroundOffset;
                        else if (row == world.Header.SurfaceLevel)
                            tileType = (Byte)(TileProperties.BackgroundOffset + 1); // Dirt Transition
                        else if (row < (world.Header.RockLayer + 38))
                            tileType = (Byte)(TileProperties.BackgroundOffset + 2); // Dirt
                        else if (row == (world.Header.RockLayer + 38))
                            tileType = (Byte)(TileProperties.BackgroundOffset + 4); // Rock Transition
                        else if (row < (world.Header.MaxTiles.Y - 202))
                            tileType = (Byte)(TileProperties.BackgroundOffset + 3); // Rock 
                        else if (row == (world.Header.MaxTiles.Y - 202))
                            tileType = (Byte)(TileProperties.BackgroundOffset + 6); // Underworld Transition
                        else
                            tileType = (Byte)(TileProperties.BackgroundOffset + 5); // Underworld

                    }
                    

                    //Check if tile is a wall.
                    if (reader.ReadBoolean())
                    {
                        byte wallType = reader.ReadByte();

                        if (tileType >= TileProperties.Unknown)
                        {
                            if (wallType + TileProperties.WallOffset > 255)
                            {
                                tileType = TileProperties.Unknown;
                            }
                            else
                            {
                                tileType = (Byte)(wallType + TileProperties.WallOffset);
                            }
                        }
                        //Wall color.
                        if (world.Header.ReleaseNumber >= 48 && reader.ReadBoolean())
                            reader.ReadByte();
                    }

                    //Check if tile is Liquid.
                    if (reader.ReadBoolean())
                    {
                        byte liquidLevel = reader.ReadByte();
                        bool isLava = reader.ReadBoolean();
                        bool isHoney = reader.ReadBoolean(); //honey


                        if (isLava)
                            tileType = TileProperties.Lava;
                        else if (isHoney)
                            tileType = TileProperties.Honey;
                        else
                            tileType = TileProperties.Water;

                    }

                    //Check if tile is wire
                    bool hasWire = reader.ReadBoolean();
                    if (world.Header.ReleaseNumber >= 43)
                    {
                        reader.ReadBoolean(); // wire2
                        var wire3 = reader.ReadBoolean(); //wire3
                    }

                    if ((hasWire == true) && (SettingsManager.Instance.DrawWires))
                        tileType = TileProperties.Wire;

                    //Check if tileis half brick
                    if (world.Header.ReleaseNumber >= 41)
                    {
                        bool halfBrick = reader.ReadBoolean(); 
                        var slope = 0;
                        if (world.Header.ReleaseNumber >= 49)
                            slope = reader.ReadByte();
                    }

                    if (world.Header.ReleaseNumber >= 42)
                    {
                        bool actuator = reader.ReadBoolean(); 
                        bool inactive = reader.ReadBoolean();
                    }


                   var RLERemaining = reader.ReadInt16();
                   mapTiles[column, row] = tileType;
                   if (RLERemaining > 0)
                   {
                       for (int fillRow = row + 1; fillRow < row + RLERemaining + 1; ++fillRow)
                       {
                           mapTiles[column, fillRow] = tileType;
                       }
                       row += RLERemaining;
                   }

                }

            }

            return mapTiles;
        }

        private void ReadChests(World world)
        {
            Boolean isChest;
            short itemCount;
            Chest theChest = null;
            Item theItem;
            Int32 i, j;
            Dictionary<Point, ChestType> chestTypeList = new Dictionary<Point, ChestType>();
            var chests = new List<Chest>();

            for (i = 0; i < 1000; i++)
            {
                isChest = reader.ReadBoolean();

                if (isChest)
                {
                    theChest = new Chest();
                    theChest.ChestId = i;
                    theChest.Active = isChest;

                    theChest.Coordinates = new Point(reader.ReadInt32(), reader.ReadInt32());

                    if (chestTypeList != null)
                    {
                        if (chestTypeList.ContainsKey(theChest.Coordinates))
                            theChest.Type = chestTypeList[theChest.Coordinates];
                    }
                    else
                    {
                        theChest.Type = ChestType.Chest;
                    }

                    for (j = 0; j < 40; j++)
                    {
                        itemCount = reader.ReadInt16();

                        if (itemCount > 0)
                        {
                            theItem = new Item();
                            theItem.Id = j;
                            theItem.Count = itemCount;

                            if (world.Header.ReleaseNumber >= 0x26)
                            {
                                theItem.Name = Global.Instance.Info.GetItemName(reader.ReadInt32());
                            }
                            else
                            {
                                theItem.Name = reader.ReadString();
                            }

                            if (world.Header.ReleaseNumber >= 0x24)
                                theItem.Prefix = reader.ReadByte();

                            theChest.Items.Add(theItem);
                        }
                    }
                    chests.Add(theChest);
                }

               // progressPosition = stream.Position;
            }
            world.Chests = chests;

        }

        private void ReadSigns(World world)
        {
            Boolean isSign;
            Sign theSign;
            var signs = new List<Sign>(1000);


            for (Int32 i = 0; i < 1000; i++)
            {
                isSign = reader.ReadBoolean();
                if (isSign == true)
                {
                    theSign = new Sign();
                    theSign.Id = i;
                    theSign.Active = isSign;

                    theSign.Text = reader.ReadString();
                    theSign.Position = new Point(reader.ReadInt32(), reader.ReadInt32());

                    signs.Add(theSign);
                }

               // progressPosition = stream.Position;
            }

            world.Signs = signs;

   
        }

        private void ReadNPCs(World world)
        {
            Boolean nextNPC;
            NPC theNPC;
            String nameCrunch;
            String[] nameArray;
            NPCType npcType;
            Int32 i;

            var npcs = new List<NPC>(20);
            i = 0;



            nextNPC = reader.ReadBoolean();
            while (nextNPC == true)
            {
                theNPC = new NPC();

                theNPC.Id = i;

                theNPC.Active = nextNPC;
                theNPC.Name = reader.ReadString();
                theNPC.Position = new PointSingle(reader.ReadSingle(), reader.ReadSingle());
                theNPC.Homeless = reader.ReadBoolean();
                theNPC.HomeTile = new Point(reader.ReadInt32(), reader.ReadInt32());

                nameArray = theNPC.Name.Split(' ');
                nameCrunch = "";
                for (Int32 j = 0; j < nameArray.Length; j++)
                    nameCrunch += nameArray[j];

                if (Enum.TryParse<NPCType>(nameCrunch, out npcType))
                    theNPC.Type = npcType;
                else
                    theNPC.Type = NPCType.Unknown;

                npcs.Add(theNPC);
                i++;

                nextNPC = reader.ReadBoolean();

               // progressPosition = stream.Position;
            }
            world.Npcs = npcs;

    
        }

        private void ReadNPCNames(World world)
        {
            var header = world.Header;
            if (header.ReleaseNumber >= 0x24)
            {
                header.MerchantsName = reader.ReadString();
                header.NursesName = reader.ReadString();
                header.ArmsDealersName = reader.ReadString();
                header.DryadsName = reader.ReadString();
                header.GuidesName = reader.ReadString();
                header.ClothiersName = reader.ReadString();
                header.DemolitionistsName = reader.ReadString();
                header.TinkerersName = reader.ReadString();
                header.WizardsName = reader.ReadString();
                header.MechanicsName = reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
            }
            else
            {
                header.MerchantsName = "Not set";
                header.NursesName = "Not Set";
                header.ArmsDealersName = "Not Set";
                header.DryadsName = "Not Set";
                header.GuidesName = "Not Set";
                header.ClothiersName = "Not Set";
                header.DemolitionistsName = "Not Set";
                header.TinkerersName = "Not Set";
                header.WizardsName = "Not Set";
                header.MechanicsName = "Not Set";
            }
            world.Header = header;
        }
    }
}
