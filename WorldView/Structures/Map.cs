using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace MoreTerra.Structures
{
    // Map is not meant to stay in the code.  This is pulled from a Terraria decompile and was
    // only used to make automating the official color grabbing easier.
    public class OfficialColor
    {
        public Byte R;
        public Byte G;
        public Byte B;

        public OfficialColor()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        public OfficialColor(Byte R, Byte G, Byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

		public OfficialColor(System.Drawing.Color c)
		{
			R = c.R;
			G = c.G;
			B = c.B;
		}
    }


    public class Map
    {
        public static int maxUpdateTile = 1000;
        public static int numUpdateTile = 0;
        public static short[] updateTileX = new short[Map.maxUpdateTile];
        public static short[] updateTileY = new short[Map.maxUpdateTile];
        public static bool saveLock = false;
        private static object padlock = new object();
        public static Map[,] mapData;
        public static int maxTilesX;
        public static int maxTilesY;
        public static int release;
        public static String worldName;
        public static int worldId;
        public ushort type;
        public byte light;
        public byte misc;
        public byte misc2;

		public static OfficialColor[][] tileColors;
		public static OfficialColor[][] wallColors;

        public Map()
        {
        }

        public Map(Map clone, Byte newLight)
        {
            if (clone == null)
                return;

            this.type = clone.type;
            this.light = newLight;
            this.misc = clone.misc;
            this.misc2 = clone.misc2;
        }

		public static void Initialize()
		{
			// To be filled in from a decompile.
		}

		public Boolean compare(Map compareTo)
        {
            if (this.type != compareTo.type)
                return false;
            if (this.light != compareTo.light)
                return false;
            if (this.misc != compareTo.misc)
                return false;
            if (this.misc2 != compareTo.misc2)
                return false;
            return true;
        }

        public bool active()
        {
            return ((int)this.misc & 1) == 1;
        }

        public void active(bool active)
        {
            if (active)
                this.misc |= (byte)1;
            else
                this.misc = (byte)((uint)this.misc & 4294967294U);
        }

        public bool water()
        {
            return ((int)this.misc & 2) == 2;
        }

        public void water(bool water)
        {
            if (water)
                this.misc |= (byte)2;
            else
                this.misc = (byte)((uint)this.misc & 4294967293U);
        }

        public bool lava()
        {
            return ((int)this.misc & 4) == 4;
        }

        public void lava(bool lava)
        {
            if (lava)
                this.misc |= (byte)4;
            else
                this.misc = (byte)((uint)this.misc & 4294967291U);
        }

        public bool honey()
        {
            return ((int)this.misc2 & 64) == 64;
        }

        public void honey(bool honey)
        {
            if (honey)
                this.misc2 |= (byte)64;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967231U);
        }

        public bool changed()
        {
            return ((int)this.misc & 8) == 8;
        }

        public void changed(bool changed)
        {
            if (changed)
                this.misc |= (byte)8;
            else
                this.misc = (byte)((uint)this.misc & 4294967287U);
        }

        public bool wall()
        {
            return ((int)this.misc & 16) == 16;
        }

        public void wall(bool wall)
        {
            if (wall)
                this.misc |= (byte)16;
            else
                this.misc = (byte)((uint)this.misc & 4294967279U);
        }

        public byte option()
        {
            byte num = (byte)0;
            if (((int)this.misc & 32) == 32) // ?
                ++num;
            if (((int)this.misc & 64) == 64) // ?
                num += (byte)2;
            if (((int)this.misc & 128) == 128) // ?
                num += (byte)4;
            if (((int)this.misc2 & 1) == 1) // ? (Misc 2)
                num += (byte)8;
            return num;
        }

        public void option(byte option)
        {
            if (((int)option & 1) == 1)
                this.misc |= (byte)32;
            else
                this.misc = (byte)((uint)this.misc & 4294967263U);
            if (((int)option & 2) == 2)
                this.misc |= (byte)64;
            else
                this.misc = (byte)((uint)this.misc & 4294967231U);
            if (((int)option & 4) == 4)
                this.misc |= (byte)128;
            else
                this.misc = (byte)((uint)this.misc & 4294967167U);
            if (((int)option & 8) == 8)
                this.misc2 |= (byte)1;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967294U);
        }

        public byte color()
        {
            byte num = (byte)0;
            if (((int)this.misc2 & 2) == 2)
                ++num;
            if (((int)this.misc2 & 4) == 4)
                num += (byte)2;
            if (((int)this.misc2 & 8) == 8)
                num += (byte)4;
            if (((int)this.misc2 & 16) == 16)
                num += (byte)8;
            if (((int)this.misc2 & 32) == 32)
                num += (byte)16;
            return num;
        }

        public void color(byte color)
        {
            if ((int)color > 27)
                color = (byte)27;
            if (((int)color & 1) == 1)
                this.misc2 |= (byte)2;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967293U);
            if (((int)color & 2) == 2)
                this.misc2 |= (byte)4;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967291U);
            if (((int)color & 4) == 4)
                this.misc2 |= (byte)8;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967287U);
            if (((int)color & 8) == 8)
                this.misc2 |= (byte)16;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967279U);
            if (((int)color & 16) == 16)
                this.misc2 |= (byte)32;
            else
                this.misc2 = (byte)((uint)this.misc2 & 4294967263U);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public OfficialColor tileColor(int j)
        {
			if (this.active())
			{
				if (this.type < 0 || this.type >= tileColors.Length) return null;
				if (tileColors[this.type][j] != null)
				{
					return tileColors[this.type][j];
				}
			}
			if (this.wall())
			{
				if (this.type < 0 || this.type >= wallColors.Length) return null;
				if (wallColors[this.type][j] != null)
				{
					return wallColors[this.type][j];
				}

			}
            return new OfficialColor(Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
        }

        public static void LoadMap(String mapName, MemoryStream mStream = null)
        {
            int maxX, maxY;
            Map mapTile = null;
            int RLECount = 0;
            Boolean active;

            int i, j;
            FileStream stream;
            BinaryReader reader;

            if (mStream == null)
            {
                stream = new FileStream(mapName, FileMode.Open);
                reader = new BinaryReader(stream);
            }
            else
            {
                mStream.Position = 0;
                reader = new BinaryReader(mStream);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Map.release = reader.ReadInt32();
            Map.worldName = reader.ReadString();
            Map.worldId = reader.ReadInt32();
            Map.maxTilesY = maxY = reader.ReadInt32();
            Map.maxTilesX = maxX = reader.ReadInt32();

            Map.mapData = new Map[maxX, maxY];

            for (i = 0; i < maxX; i++)
            {
                for (j = 0; j < maxY; j++)
                {

                    active = reader.ReadBoolean();

                    if (active)
                    {
                        mapTile = new Map();
                        mapTile.type = reader.ReadByte();
                        mapTile.light = reader.ReadByte();
                        mapTile.misc = reader.ReadByte();
                        mapTile.misc2 = reader.ReadByte();

                        Map.mapData[i, j] = mapTile;
                        RLECount = reader.ReadInt16();

                        if (RLECount > 0)
                        {
                            if (mapTile.light == 255)
                            {
                                while (RLECount > 0)
                                {
                                    j++;
                                    Map.mapData[i, j] = new Map(mapTile, 255);
                                    RLECount--;
                                }
                            }
                            else
                            {
                                while (RLECount > 0)
                                {
                                    j++;
                                    Map.mapData[i, j] = new Map(mapTile, reader.ReadByte());
                                    RLECount--;
                                }
                            }
                        }
                    }
                    else
                    {
                        Map.mapData[i, j] = new Map(); // This is wrong but we have to mimic what the offical version does to compare.
                        mapTile = null;
                        j += reader.ReadInt16();
                    }
                }
            }

            sw.Stop();

            reader.Close();

        }

        public static void loadMap(String path, MemoryStream mStream = null)
        {
            Map.saveLock = false;
            if (!File.Exists(path))
                return;

            FileStream stream;
            BinaryReader reader;

            if (mStream == null)
            {
                stream = new FileStream(path, FileMode.Open);
                reader = new BinaryReader(stream);
            } else {
                mStream.Position = 0;
                reader = new BinaryReader(mStream);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Map.release = reader.ReadInt32();
            Map.worldName = reader.ReadString();
            Map.worldId = reader.ReadInt32();
            Map.maxTilesY = reader.ReadInt32();
            Map.maxTilesX = reader.ReadInt32();
            Map.mapData = new Map[Map.maxTilesX, Map.maxTilesY];
            for (int index1 = 0; index1 < Map.maxTilesX; ++index1)
            {
                for (int index2 = 0; index2 < Map.maxTilesY; ++index2)
                {
                    Map.mapData[index1, index2] = new Map();
                    bool flag = reader.ReadBoolean();
                    if (flag)
                    {
                        Map.mapData[index1, index2].type = reader.ReadByte();
                        Map.mapData[index1, index2].light = reader.ReadByte();
                        Map.mapData[index1, index2].misc = reader.ReadByte();
                        Map.mapData[index1, index2].misc2 = Map.release < 50 ? (byte)0 : reader.ReadByte();
                        int num3 = reader.ReadInt16();
                        if (Map.mapData[index1, index2].light == byte.MaxValue)
                        {
                            if (num3 > 0)
                            {
                                for (int index3 = index2 + 1; index3 < index2 + num3 + 1; ++index3)
                                {
                                    Map.mapData[index1, index3] = new Map();
                                    Map.mapData[index1, index3].type = Map.mapData[index1, index2].type;
                                    Map.mapData[index1, index3].misc = Map.mapData[index1, index2].misc;
                                    Map.mapData[index1, index3].misc2 = Map.mapData[index1, index2].misc2;
                                    Map.mapData[index1, index3].light = Map.mapData[index1, index2].light;
                                }
                                index2 += num3;
                            }
                        }
                        else if (num3 > 0)
                        {
                            for (int index3 = index2 + 1; index3 < index2 + num3 + 1; ++index3)
                            {
                                byte num4 = reader.ReadByte();
                                if ((int)num4 > 18)
                                {
                                    Map.mapData[index1, index3] = new Map();
                                    Map.mapData[index1, index3].type = Map.mapData[index1, index2].type;
                                    Map.mapData[index1, index3].misc = Map.mapData[index1, index2].misc;
                                    Map.mapData[index1, index3].misc2 = Map.mapData[index1, index2].misc2;
                                    Map.mapData[index1, index3].light = num4;
                                }
                            }
                            index2 += num3;
                        }
                    }
                    else
                    {
                        int num3 = (int)reader.ReadInt16();
                        if (num3 > 0)
                        {
                            index2 += num3;
                            if (Map.mapData[index1, index2] != null)
                                Map.mapData[index1, index2] = new Map();
                        }
                    }
                }
            }
            sw.Stop();

            reader.Close();
            //stream.Close();
        }

        public static void SaveMap(String path)
        {
            Map mapTile;
            Map mapCompare;
            Int16 RLECount;
            int countTo;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            FileStream stream;
            BinaryWriter writer;
            

            stream = new FileStream(path, FileMode.Create);
            writer = new BinaryWriter(stream);

            writer.Write(Map.release);
            writer.Write(Map.worldName);
            writer.Write(Map.worldId);
            writer.Write(Map.maxTilesY);
            writer.Write(Map.maxTilesX);

            for (int i = 0; i < Map.maxTilesX; ++i)
            {
                for (int j = 0; j < Map.maxTilesY; j++)
                {
                    mapTile = Map.mapData[i, j];

                    countTo = Map.maxTilesY - j - 1;
                    if (mapTile != null && mapTile.light > 18)
                    {
                        writer.Write(true);
                        writer.Write(mapTile.type);
                        writer.Write(mapTile.light);
                        writer.Write(mapTile.misc);
                        writer.Write(mapTile.misc2);
                        
                        RLECount = 0;
                        if (mapTile.light == 255)
                        {
                            while (countTo > 0)
                            {
                                countTo--;
                                j++;
                                mapCompare = Map.mapData[i, j];

                                if (mapCompare != null &&
                                    mapTile.type == mapCompare.type &&
                                    mapTile.misc == mapCompare.misc &&
                                    mapTile.misc2 == mapCompare.misc2 &&
                                    mapCompare.light == 255)
                                {
                                    RLECount++;
                                    continue;
                                }
                                j--;
                                break;
                            }
                            writer.Write(RLECount);
                        }
                        else
                        {
                            int j_off = j;
                            while (countTo > 0)
                            {
                                countTo--;
                                j_off++;

                                mapCompare = Map.mapData[i, j_off];

                                if (mapCompare != null &&
                                    mapTile.type == mapCompare.type &&
                                    mapTile.misc == mapCompare.misc &&
                                    mapTile.misc2 == mapCompare.misc2 && 
                                    mapCompare.light > 18)
                                {
                                    RLECount++;
                                    continue;
                                }
                                break;
                            }
                            writer.Write(RLECount);

                            while (RLECount > 0)
                            {
                                j++;
                                RLECount--;
                                writer.Write(Map.mapData[i, j].light);
                            }
                        }
                    }
                    else
                    {
                        writer.Write(false);
                        RLECount = 0;
                        while (countTo > 0)
                        {
                            countTo--;
                            j++;
                            mapCompare = Map.mapData[i, j];

                            if (mapCompare == null || mapCompare.light == 0)
                            {
                                RLECount++;
                                continue;
                            }
                            j--;
                            break;
                        }
                        writer.Write(RLECount);
                    }
                    writer.Flush();
                }
            }

            sw.Stop();

            writer.Close();
            stream.Close();
        }

        public static void saveMap(String path)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            FileStream stream;
            BinaryWriter writer;

            stream = new FileStream(path, FileMode.Create);
            writer = new BinaryWriter(stream);

            writer.Write(Map.release);
            writer.Write(Map.worldName);
            writer.Write(Map.worldId);
            writer.Write(Map.maxTilesY);
            writer.Write(Map.maxTilesX);

            for (int i = 0; i < Map.maxTilesX; ++i)
            {
                for (int j = 0; j < Map.maxTilesY; j++)
                {
                    bool local_9 = false;
                    if (Map.mapData[i, j] != null && (int) Map.mapData[i, j].light > 18)
                    {
                        local_9 = true;
                        writer.Write(local_9);
                        writer.Write(Map.mapData[i, j].type);
                        writer.Write(Map.mapData[i, j].light);
                        writer.Write(Map.mapData[i, j].misc);
                        writer.Write(Map.mapData[i, j].misc2);
                        int count = 1;
                        if (Map.mapData[i, j].light == byte.MaxValue)
                        {
                            while (j + count < Map.maxTilesY && Map.mapData[i, j + count] != null && ((int) Map.mapData[i, j].type == (int) Map.mapData[i, j + count].type && (int) Map.mapData[i, j].misc == (int) Map.mapData[i, j + count].misc) && ((int) Map.mapData[i, j].misc2 == (int) Map.mapData[i, j + count].misc2 && (int) Map.mapData[i, j + count].light == (int) byte.MaxValue))
                                ++count;
                            int local_10_1 = count - 1;
                            writer.Write((short) local_10_1);
                            j = j + local_10_1;
                        }
                        else
                        {
                            while (j + count < Map.maxTilesY && Map.mapData[i, j + count] != null && ((int) Map.mapData[i, j + count].light > 18 && (int) Map.mapData[i, j].type == (int) Map.mapData[i, j + count].type) && ((int) Map.mapData[i, j].misc == (int) Map.mapData[i, j + count].misc && (int) Map.mapData[i, j].misc2 == (int) Map.mapData[i, j + count].misc2))
                            ++count;
                            int local_10_2 = count - 1;
                            writer.Write((short) local_10_2);
                            if (local_10_2 > 0)
                            {
                            for (int local_11 = j + 1; local_11 < j + local_10_2 + 1; ++local_11)
                                writer.Write(Map.mapData[i, local_11].light);
                            }
                            j = j + local_10_2;
                        }
                    }
                    else
                    {
                        writer.Write(local_9);
                        int local_12 = 1;
                        while (j + local_12 < Map.maxTilesY && (Map.mapData[i, j + local_12] == null || (int) Map.mapData[i, j + local_12].light == 0))
                            ++local_12;
                        int local_12_1 = local_12 - 1;
                        writer.Write((short) local_12_1);
                        j = j + local_12_1;
                    }
                }
            }
            sw.Stop();
            writer.Close();
            stream.Close();
        }

        public static void CountBadRLE(String mapName)
        {
            int release;
            String worldName;
            int worldId;
            int maxX, maxY;

            int i, j;
            int count;
            FileStream stream = new FileStream(mapName, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            release = reader.ReadInt32();
            worldName = reader.ReadString();
            worldId = reader.ReadInt32();
            maxY = reader.ReadInt32();
            maxX = reader.ReadInt32();

            for (i = 0; i < maxX; i++)
            {
                for (j = 0; j < maxY; j++)
                {
                    Boolean active = reader.ReadBoolean();

                    if (active)
                    {
                        reader.ReadByte(); // Type
                        byte light = reader.ReadByte();
                        reader.ReadByte(); // Misc
                        reader.ReadByte(); // Misc 2
                        count = reader.ReadInt16();

                        if (light == 255)
                        {
                            j += count;
                        }
                        else
                        {
                            j += count;
                            while (count > 0)
                            {
                                reader.ReadByte();
                                count--;
                            }   
                        }
                    }
                    else
                    {
                        count = reader.ReadInt16();
                        j += count;
                    }
                }
            }

            reader.Close();
        }
        /*
        public void setTile(int i, int j, byte Light)
        {
            Tile mapTile = null;

            if (mapTile == null)
                return;
            bool flag = false;
            if ((int)this.light < (int)Light)
            {
                this.light = Light;
                this.changed(true);
            }
            if (mapTile != null &&
                mapTile.Active &&
                ((int)mapTile.TileType != 135 &&
                 (int)mapTile.TileType != (int)sbyte.MaxValue) &&
                ((int)mapTile.TileType != 210 &&
                 ((int)mapTile.TileType != 51 || (i + j) % 2 != 0)))
            {
                if (!this.active())
                    this.changed(true);
                this.active(true);
                if ((int)this.type != (int)mapTile.TileType)
                    this.changed(true);
                this.type = mapTile.TileType;
                if ((int)this.type == 160)
                {
                    if ((int)this.color() != 0)
                        this.changed(true);
                    this.color((byte)0);
                }
                else
                {
                    if ((int)this.color() != (int)mapTile.TileColor)
                        this.changed(true);
                    this.color(mapTile.TileColor);
                }
                this.lava(false);
                this.water(false);
                this.honey(false);
                flag = true;
                if ((int)mapTile.TileType == 4)
                {
                    if ((int)mapTile.Frame.X < 66)
                        this.option((byte)1);
                    else
                        this.option((byte)0);
                }
                else if ((int)mapTile.TileType == 21)
                {
                    switch ((int)mapTile.Frame.X / 36)
                    {
                        case 1:
                        case 2:
                        case 10:
                        case 13:
                        case 15:
                            this.option((byte)1);
                            break;
                        case 3:
                        case 4:
                            this.option((byte)2);
                            break;
                        case 6:
                            this.option((byte)3);
                            break;
                        case 11:
                        case 17:
                            this.option((byte)4);
                            break;
                        default:
                            this.option((byte)0);
                            break;
                    }
                }
                else if ((int)mapTile.TileType == 28)
                {
                    if ((int)mapTile.Frame.Y < 144)
                        this.option((byte)0);
                    else if ((int)mapTile.Frame.Y < 252)
                        this.option((byte)1);
                    else if ((int)mapTile.Frame.Y < 360 || (int)mapTile.Frame.Y > 900 && (int)mapTile.Frame.Y < 1008)
                        this.option((byte)2);
                    else if ((int)mapTile.Frame.Y < 468)
                        this.option((byte)3);
                    else if ((int)mapTile.Frame.Y < 576)
                        this.option((byte)4);
                    else if ((int)mapTile.Frame.Y < 684)
                        this.option((byte)5);
                    else if ((int)mapTile.Frame.Y < 792)
                        this.option((byte)6);
                    else if ((int)mapTile.Frame.Y < 898)
                        this.option((byte)8);
                    else
                        this.option((byte)7);
                }
                else if ((int)mapTile.TileType == 27)
                {
                    if ((int)mapTile.Frame.Y < 34)
                        this.option((byte)1);
                    else
                        this.option((byte)0);
                }
                else if ((int)mapTile.TileType == 31)
                {
                    if ((int)mapTile.Frame.X > 36)
                        this.option((byte)1);
                    else
                        this.option((byte)0);
                }
                else if ((int)mapTile.TileType == 26)
                {
                    if ((int)mapTile.Frame.X >= 54)
                        this.option((byte)1);
                    else
                        this.option((byte)0);
                }
                else if ((int)mapTile.TileType == 137)
                {
                    if ((int)mapTile.Frame.Y == 0)
                        this.option((byte)0);
                    else
                        this.option((byte)1);
                }
                else if ((int)mapTile.TileType == 82 || (int)mapTile.TileType == 83 || (int)mapTile.TileType == 84)
                {
                    if ((int)mapTile.Frame.X < 18)
                        this.option((byte)0);
                    else if ((int)mapTile.Frame.X < 36)
                        this.option((byte)1);
                    else if ((int)mapTile.Frame.X < 54)
                        this.option((byte)2);
                    else if ((int)mapTile.Frame.X < 72)
                        this.option((byte)3);
                    else if ((int)mapTile.Frame.X < 90)
                        this.option((byte)4);
                    else
                        this.option((byte)6);
                }
                else if ((int)mapTile.TileType == 105)
                {
                    if ((int)mapTile.Frame.X >= 1548 && (int)mapTile.Frame.X <= 1654)
                        this.option((byte)1);
                    if ((int)mapTile.Frame.X >= 1656 && (int)mapTile.Frame.X <= 1798)
                        this.option((byte)2);
                    else
                        this.option((byte)0);
                }
                else if ((int)mapTile.TileType == 133)
                {
                    if ((int)mapTile.Frame.X < 52)
                        this.option((byte)0);
                    else
                        this.option((byte)1);
                }
                else if ((int)mapTile.TileType == 134)
                {
                    if ((int)mapTile.Frame.X < 28)
                        this.option((byte)0);
                    else
                        this.option((byte)1);
                }
                else if ((int)mapTile.TileType == 165)
                {
                    if ((int)mapTile.Frame.X < 54)
                        this.option((byte)0);
                    else if ((int)mapTile.Frame.X < 106)
                        this.option((byte)1);
                    else if ((int)mapTile.Frame.X < 162)
                        this.option((byte)2);
                    else
                        this.option((byte)3);
                }
                else if ((int)mapTile.TileType == 178)
                {
                    if ((int)mapTile.Frame.X < 18)
                        this.option((byte)0);
                    else if ((int)mapTile.Frame.X < 36)
                        this.option((byte)1);
                    else if ((int)mapTile.Frame.X < 54)
                        this.option((byte)2);
                    else if ((int)mapTile.Frame.X < 72)
                        this.option((byte)3);
                    else if ((int)mapTile.Frame.X < 90)
                        this.option((byte)4);
                    else if ((int)mapTile.Frame.X < 108)
                        this.option((byte)5);
                    else
                        this.option((byte)6);
                }
                else if ((int)mapTile.TileType == 184)
                {
                    if ((int)mapTile.Frame.X < 22)
                        this.option((byte)0);
                    else if ((int)mapTile.Frame.X < 44)
                        this.option((byte)1);
                    else if ((int)mapTile.Frame.X < 66)
                        this.option((byte)2);
                    else if ((int)mapTile.Frame.X < 88)
                        this.option((byte)3);
                    else
                        this.option((byte)4);
                }
                else if ((int)mapTile.TileType == 185)
                {
                    if ((int)mapTile.Frame.Y < 18)
                    {
                        int num = (int)mapTile.Frame.X / 18;
                        if (num < 6 || num == 28 || (num == 29 || num == 30) || (num == 31 || num == 32))
                            this.option((byte)0);
                        else if (num < 12 || num == 33 || (num == 34 || num == 35))
                            this.option((byte)1);
                        else if (num < 28)
                            this.option((byte)2);
                        else if (num < 48)
                            this.option((byte)3);
                        else if (num < 54)
                            this.option((byte)4);
                    }
                    else
                    {
                        int num = (int)mapTile.Frame.X / 36;
                        if (num < 6 || num == 19 || (num == 20 || num == 21) || (num == 22 || num == 23 || (num == 24 || num == 33)) || (num == 38 || num == 39 || num == 40))
                            this.option((byte)0);
                        else if (num < 16)
                            this.option((byte)2);
                        else if (num < 19 || num == 31 || num == 32)
                            this.option((byte)1);
                        else if (num < 31)
                            this.option((byte)3);
                        else if (num < 38)
                            this.option((byte)4);
                    }
                }
                else if ((int)mapTile.TileType == 186)
                {
                    int num = (int)mapTile.Frame.X / 54;
                    if (num < 7)
                        this.option((byte)2);
                    else if (num < 22 || num == 33 || (num == 34 || num == 35))
                        this.option((byte)0);
                    else if (num < 25)
                        this.option((byte)1);
                    else if (num == 25)
                        this.option((byte)5);
                    else if (num < 32)
                        this.option((byte)3);
                }
                else if ((int)mapTile.TileType == 187)
                {
                    int num = (int)mapTile.Frame.X / 54;
                    if (num < 3 || num == 14 || (num == 15 || num == 16))
                        this.option((byte)0);
                    else if (num < 6)
                        this.option((byte)6);
                    else if (num < 9)
                        this.option((byte)7);
                    else if (num < 14)
                        this.option((byte)4);
                    else if (num < 18)
                        this.option((byte)4);
                    else if (num < 23)
                        this.option((byte)8);
                    else if (num < 25)
                        this.option((byte)0);
                    else if (num < 29)
                        this.option((byte)1);
                }
                else if ((int)mapTile.TileType == 227)
                    this.option((byte)((uint)mapTile.Frame.X / 34U));
                else if ((int)mapTile.TileType == 240)
                {
                    int num = (int)mapTile.Frame.X / 54;
                    if (num >= 0 && num <= 11)
                        this.option((byte)0);
                    else if (num >= 12 && num <= 15)
                        this.option((byte)1);
                    else if (num == 16 || num == 17)
                        this.option((byte)2);
                    else if (num >= 18 && num <= 35)
                        this.option((byte)1);
                }
                else if ((int)mapTile.TileType == 241)
                {
                    int num = (int)mapTile.Frame.Y / 54;
                    this.option((byte)0);
                }
                else
                    this.option((byte)0);
            }
            if (flag)
                return;
            if (this.active())
                this.changed(true);
            this.active(false);
            if (mapTile != null && (int)mapTile.LiquidLevel > 32)
            {
                if ((int)this.color() != 0)
                    this.changed(true);
                this.color((byte)0);
                if (this.wall())
                    this.changed(true);
                if (mapTile.Lava)
                {
                    if (!this.lava())
                        this.changed(true);
                    this.lava(true);
                    this.water(false);
                    this.honey(false);
                }
                else if (mapTile.Honey)
                {
                    if (!this.honey())
                        this.changed(true);
                    this.honey(true);
                    this.lava(false);
                    this.water(false);
                }
                else
                {
                    if (!this.water())
                        this.changed(true);
                    this.water(true);
                    this.lava(false);
                    this.honey(false);
                }
            }
            else
            {
                if (this.lava() || this.water() || this.honey())
                    this.changed(true);
                this.lava(false);
                this.water(false);
                this.honey(false);
                if (mapTile != null && (int)mapTile.WallType > 0)
                {
                    if (!this.wall())
                        this.changed(true);
                    this.wall(true);
                    if ((int)mapTile.WallType != (int)this.type)
                        this.changed(true);
                    this.type = mapTile.WallType;
                    if ((int)this.type == 21)
                    {
                        if ((int)this.color() != 0)
                            this.changed(true);
                        this.color((byte)0);
                        if ((double)j >= Main.worldSurface)
                            return;
                        if ((int)this.light < (int)byte.MaxValue)
                            this.changed(true);
                        this.light = byte.MaxValue;
                    }
                    else
                    {
                        if ((int)this.color() != (int)mapTile.WallColor)
                            this.changed(true);
                        this.color(mapTile.WallColor);
                    }
                }
                else if ((double)j < Main.worldSurface)
                {
                    if ((int)this.color() != 0)
                        this.changed(true);
                    this.color((byte)0);
                    if (this.wall())
                        this.changed(true);
                    this.wall(false);
                    if (this.water() || this.lava() || this.honey())
                        this.changed(true);
                    this.water(false);
                    this.lava(false);
                    this.honey(false);
                    if ((int)this.light >= (int)byte.MaxValue)
                        return;
                    this.light = byte.MaxValue;
                    this.changed(true);
                }
                else
                {
                    if (j >= Main.maxTilesY - 200)
                        return;
                    if ((int)this.color() != 0)
                        this.changed(true);
                    this.color((byte)0);
                    if (this.wall())
                        this.changed(true);
                    this.wall(false);
                    if (this.water() || this.lava() || this.honey())
                        this.changed(true);
                    this.water(false);
                    this.lava(false);
                    this.honey(false);
                    float num1 = (float)((double)Main.screenPosition.X / 16.0 - 5.0);
                    float num2 = (float)(((double)Main.screenPosition.X + (double)Main.screenWidth) / 16.0 + 5.0);
                    float num3 = (float)((double)Main.screenPosition.Y / 16.0 - 5.0);
                    float num4 = (float)(((double)Main.screenPosition.Y + (double)Main.screenHeight) / 16.0 + 5.0);
                    if (((double)i < (double)num1 || (double)i > (double)num2 || ((double)j < (double)num3 || (double)j > (double)num4)) && (i > 40 && i < Main.maxTilesX - 40 && (j > 40 && j < Main.maxTilesY - 40)))
                    {
                        if (!this.changed())
                            return;
                        byte num5 = (byte)0;
                        int index1 = i - 36;
                        while (index1 <= i + 30)
                        {
                            int index2 = j - 36;
                            while (index2 <= j + 30)
                            {
                                if (Main.map[index1, index2] != null && Main.map[index1, index2].active())
                                {
                                    switch (Main.map[index1, index2].type)
                                    {
                                        case (byte)147:
                                        case (byte)161:
                                        case (byte)162:
                                        case (byte)163:
                                        case (byte)164:
                                        case (byte)200:
                                            num5 = byte.MaxValue;
                                            goto label_244;
                                    }
                                }
                                index2 += 10;
                            }
                        label_244:
                            if ((int)num5 == 0)
                                index1 += 10;
                            else
                                break;
                        }
                        if ((int)this.type != (int)num5)
                            this.changed(true);
                        this.type = num5;
                    }
                    else
                    {
                        float num5 = (float)Main.snowTiles / 1000f * (float)byte.MaxValue;
                        if ((double)num5 > (double)byte.MaxValue)
                            num5 = (float)byte.MaxValue;
                        if ((double)this.type == (double)num5)
                            return;
                        this.changed(true);
                        this.type = (byte)num5;
                    }
                }
            }
        }
        */
    }
}