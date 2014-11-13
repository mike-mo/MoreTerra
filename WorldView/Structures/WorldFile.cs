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
        private short sectionCount;
        private int[] sectionPointers;
        private short tiletypeCount;
        private bool[] tileImportance;

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
            //world.TileTypes = mapTiles;
            return world;
        }

        private void ReadHeader(World world)
        {
            version = reader.ReadInt32();
            sectionCount = reader.ReadInt16();
            sectionPointers = new int[sectionCount];
            for(int i = 0; i < sectionCount; i++)
            {
                sectionPointers[i] = reader.ReadInt32();
            }
            tiletypeCount = reader.ReadInt16();
            tileImportance = new bool[tiletypeCount];
            byte mask = 0x80;
            byte flags = 0;
            for (int i = 0; i < tiletypeCount; i++)
            {
                if (mask == 0x80)
                {
                    flags = reader.ReadByte();
                    mask = 0x01;
                }
                else
                {
                    mask <<= 1;
                }

                if ((flags & mask) == mask)
                    tileImportance[i] = true;

                
            }
            int x, y, w, h;
            
            var header = new WorldHeader();

           // header.ReleaseNumber = version;
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

            header.TreeX = new int[3];
            header.TreeStyle = new int[4];
            header.CaveBackX = new int[3];
            header.CaveBackStyle = new int[4];

            if (version >= 0x3F)
            {
                header.MoonType = reader.ReadByte();
                for (int i = 0; i < 3; i++)
                {
                    header.TreeX[i] = reader.ReadInt32();
                }
                for (int i = 0; i < 4; i++)
                {
                    header.TreeStyle[i] = reader.ReadInt32();
                }
                for (int i = 0; i < 3; i++)
                {
                    header.CaveBackX[i] = reader.ReadInt32();
                }
                for (int i = 0; i < 4; i++)
                {
                    header.CaveBackStyle[i] = reader.ReadInt32();
                }
                header.IceBackStyle = reader.ReadInt32();
                header.JungleBackStyle = reader.ReadInt32();
                header.HellBackStyle = reader.ReadInt32();
            }


            header.SpawnPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
            header.SurfaceLevel = reader.ReadDouble();
            header.RockLayer = reader.ReadDouble();
            header.TemporaryTime = reader.ReadDouble();
            header.IsDayTime = reader.ReadBoolean();
            header.MoonPhase = reader.ReadInt32();
            header.IsBloodMoon = reader.ReadBoolean();
            if (version >= 70)
                header.IsEclipse = reader.ReadBoolean();
            header.DungeonPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
            if (version >= 66)
			    header.Crimson = reader.ReadBoolean();
            header.IsBoss1Dead = reader.ReadBoolean();
            header.IsBoss2Dead = reader.ReadBoolean();
            header.IsBoss3Dead = reader.ReadBoolean();
            if (version >= 66)
            {
                header.IsQueenBeeDead = reader.ReadBoolean();
                header.IsMechBoss1Dead = reader.ReadBoolean();
                header.IsMechBoss2Dead = reader.ReadBoolean();
                header.IsMechBoss3Dead = reader.ReadBoolean();
                header.IsMechBossAnyDead = reader.ReadBoolean();
                header.IsPlantBossDead = reader.ReadBoolean();
                header.IsGolemBossDead = reader.ReadBoolean();
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
            if (version >= 66)
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

            header.OreTiers = new int[3];
            header.Styles = new byte[8];
            if (version >= 66)
            {
                header.IsRaining = reader.ReadBoolean();
                header.RainTime = reader.ReadInt32();
                header.MaxRain = reader.ReadSingle();

                for (int i = 0; i < 3; i++)
                {
                    header.OreTiers[i] = reader.ReadInt32();
                }

                for (int i = 0; i < 8; i++)
                {
                    header.Styles[i] = reader.ReadByte();
                }

                header.CloudsActive = reader.ReadInt32();
                header.NumClouds = reader.ReadInt16();
                header.WindSpeed = reader.ReadSingle();
            }
            else if (version >= 24)
            {
                if (header.AltarsDestroyed == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        header.OreTiers[i] = -1;
                    }
                }
                else
                {
                    header.OreTiers[1] = 107;
                    header.OreTiers[1] = 108;
                    header.OreTiers[1] = 111;
                }
            }

            world.Header = header;
        }
        

        private byte[,] ReadTiles(World world)
        {
            var mapTiles = new Byte[world.Header.MaxTiles.X, world.Header.MaxTiles.Y];

  
            byte firstHeader, secondHeader;
            
            for (int column = 0; column < world.Header.MaxTiles.X; column++)
            {
                for (int row = 0; row < world.Header.MaxTiles.Y; row++)
                {
                    firstHeader = reader.ReadByte();
                    if ((firstHeader & 1) == 1)
                        secondHeader = reader.ReadByte();

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

                    if (world.ChestTypeList != null)
                    {
                        if (world.ChestTypeList.ContainsKey(theChest.Coordinates))
                            theChest.Type = world.ChestTypeList[theChest.Coordinates];
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
                header.DyeTradersName =  reader.ReadString();
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
