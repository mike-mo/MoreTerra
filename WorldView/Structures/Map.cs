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
        public byte type;
        public byte light;
        public byte misc;
        public byte misc2;

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
                switch (this.type)
                {
                    case (byte)0:
                    case (byte)5:
                    case (byte)30:
                    case (byte)191:
                        return new OfficialColor(151, 107, 75);
                    case (byte)1:
                    case (byte)38:
                    case (byte)48:
                    case (byte)130:
                    case (byte)138:
                        return new OfficialColor(128, 128, 128);
                    case (byte)2:
                        return new OfficialColor(28, 216, 94);
                    case (byte)3:
                    case (byte)192:
                        return new OfficialColor(26, 196, 84);
                    case (byte)4:
                        if ((int)this.option() == 0)
                            return new OfficialColor(169, 125, 93);
                        else
                            return new OfficialColor(253, 221, 3);
                    case (byte)6:
                        return new OfficialColor(140, 101, 80);
                    case (byte)7:
                    case (byte)47:
                        return new OfficialColor(150, 67, 22);
                    case (byte)8:
                    case (byte)45:
                        return new OfficialColor(185, 164, 23);
                    case (byte)9:
                    case (byte)46:
                        return new OfficialColor(185, 194, 195);
                    case (byte)10:
                    case (byte)11:
                        return new OfficialColor(119, 105, 79);
                    case (byte)12:
                        return new OfficialColor(174, 24, 69);
                    case (byte)13:
                        return new OfficialColor(133, 213, 247);
                    case (byte)14:
                    case (byte)15:
                    case (byte)18:
                    case (byte)19:
                    case (byte)55:
                    case (byte)79:
                    case (byte)86:
                    case (byte)87:
                    case (byte)88:
                    case (byte)89:
                    case (byte)94:
                    case (byte)101:
                    case (byte)104:
                    case (byte)106:
                    case (byte)114:
                    case (byte)128:
                    case (byte)139:
                    case (byte)216:
                        return new OfficialColor(191, 142, 111);
                    case (byte)16:
                        return new OfficialColor(140, 130, 116);
                    case (byte)17:
                    case (byte)90:
                    case (byte)96:
                    case (byte)97:
                    case (byte)99:
                    case (byte)132:
                    case (byte)142:
                    case (byte)143:
                    case (byte)144:
                    case (byte)207:
                    case (byte)209:
                    case (byte)212:
                    case (byte)217:
                    case (byte)218:
                    case (byte)219:
                    case (byte)220:
                    case (byte)228:
                        return new OfficialColor(144, 148, 144);
                    case (byte)20:
                        return new OfficialColor(163, 116, 81);
                    case (byte)21:
                        if ((int)this.option() == 0)
                            return new OfficialColor(174, 129, 92);
                        if ((int)this.option() == 1)
                            return new OfficialColor(233, 207, 94);
                        if ((int)this.option() == 2)
                            return new OfficialColor(137, 128, 200);
                        if ((int)this.option() == 3)
                            return new OfficialColor(160, 160, 160);
                        else
                            return new OfficialColor(106, 210, 255);
                    case (byte)22:
                    case (byte)140:
                        return new OfficialColor(98, 95, 167);
                    case (byte)23:
                        return new OfficialColor(141, 137, 223);
                    case (byte)24:
                        return new OfficialColor(122, 116, 218);
                    case (byte)25:
                        return new OfficialColor(109, 90, 128);
                    case (byte)26:
                        if ((int)this.option() == 1)
                            return new OfficialColor(214, (int)sbyte.MaxValue, 133);
                        else
                            return new OfficialColor(119, 101, 125);
                    case (byte)27:
                        if ((int)this.option() == 1)
                            return new OfficialColor(226, 196, 49);
                        else
                            return new OfficialColor(54, 154, 54);
                    case (byte)28:
                        if ((int)this.option() == 0)
                            return new OfficialColor(151, 79, 80);
                        if ((int)this.option() == 1)
                            return new OfficialColor(90, 139, 140);
                        if ((int)this.option() == 2)
                            return new OfficialColor(192, 136, 70);
                        if ((int)this.option() == 3)
                            return new OfficialColor(203, 185, 151);
                        if ((int)this.option() == 4)
                            return new OfficialColor(73, 56, 41);
                        if ((int)this.option() == 5)
                            return new OfficialColor(148, 159, 67);
                        if ((int)this.option() == 6)
                            return new OfficialColor(138, 172, 67);
                        if ((int)this.option() == 8)
                            return new OfficialColor(198, 87, 93);
                        else
                            return new OfficialColor(226, 122, 47);
                    case (byte)29:
                        return new OfficialColor(175, 105, 128);
                    case (byte)31:
                        if ((int)this.option() == 1)
                            return new OfficialColor(212, 105, 105);
                        else
                            return new OfficialColor(141, 120, 168);
                    case (byte)32:
                        return new OfficialColor(151, 135, 183);
                    case (byte)33:
                    case (byte)93:
                    case (byte)98:
                    case (byte)100:
                    case (byte)173:
                    case (byte)174:
                        return new OfficialColor(253, 221, 3);
                    case (byte)34:
                        return new OfficialColor(235, 166, 135);
                    case (byte)35:
                        return new OfficialColor(197, 216, 219);
                    case (byte)36:
                    case (byte)102:
                        return new OfficialColor(229, 212, 73);
                    case (byte)37:
                        return new OfficialColor(104, 86, 84);
                    case (byte)39:
                        return new OfficialColor(181, 62, 59);
                    case (byte)40:
                        return new OfficialColor(146, 81, 68);
                    case (byte)41:
                        return new OfficialColor(66, 84, 109);
                    case (byte)42:
                        return new OfficialColor(251, 235, (int)sbyte.MaxValue);
                    case (byte)43:
                        return new OfficialColor(84, 100, 63);
                    case (byte)44:
                        return new OfficialColor(107, 68, 99);
                    case (byte)49:
                        return new OfficialColor(89, 201, 255);
                    case (byte)50:
                        return new OfficialColor(170, 48, 114);
                    case (byte)51:
                        return new OfficialColor(192, 202, 203);
                    case (byte)52:
                        return new OfficialColor(23, 177, 76);
                    case (byte)53:
                        return new OfficialColor(186, 168, 84);
                    case (byte)54:
                        return new OfficialColor(200, 246, 254);
                    case (byte)56:
                        return new OfficialColor(43, 40, 84);
                    case (byte)57:
                        return new OfficialColor(68, 68, 76);
                    case (byte)58:
                    case (byte)76:
                        return new OfficialColor(142, 66, 66);
                    case (byte)59:
                    case (byte)120:
                        return new OfficialColor(92, 68, 73);
                    case (byte)60:
                        return new OfficialColor(143, 215, 29);
                    case (byte)61:
                        return new OfficialColor(135, 196, 26);
                    case (byte)62:
                        return new OfficialColor(121, 176, 24);
                    case (byte)63:
                        return new OfficialColor(110, 140, 182);
                    case (byte)64:
                        return new OfficialColor(196, 96, 114);
                    case (byte)65:
                        return new OfficialColor(56, 150, 97);
                    case (byte)66:
                        return new OfficialColor(160, 118, 58);
                    case (byte)67:
                        return new OfficialColor(140, 58, 166);
                    case (byte)68:
                        return new OfficialColor(125, 191, 197);
                    case (byte)69:
                        return new OfficialColor(190, 150, 92);
                    case (byte)70:
                        return new OfficialColor(93, (int)sbyte.MaxValue, 255);
                    case (byte)71:
                    case (byte)72:
                    case (byte)190:
                        return new OfficialColor(182, 175, 130);
                    case (byte)73:
                        return new OfficialColor(27, 197, 109);
                    case (byte)74:
                        return new OfficialColor(96, 197, 27);
                    case (byte)75:
                        return new OfficialColor(26, 26, 26);
                    case (byte)77:
                        return new OfficialColor(238, 85, 70);
                    case (byte)78:
                        return new OfficialColor(121, 110, 97);
                    case (byte)80:
                    case (byte)188:
                        return new OfficialColor(73, 120, 17);
                    case (byte)81:
                        return new OfficialColor(245, 133, 191);
                    case (byte)82:
                    case (byte)83:
                    case (byte)84:
                        if ((int)this.option() == 0)
                            return new OfficialColor(246, 197, 26);
                        if ((int)this.option() == 1)
                            return new OfficialColor(76, 150, 216);
                        if ((int)this.option() == 2)
                            return new OfficialColor(185, 214, 42);
                        if ((int)this.option() == 3)
                            return new OfficialColor(167, 203, 37);
                        if ((int)this.option() == 4)
                            return new OfficialColor(72, 145, 125);
                        else
                            return new OfficialColor(177, 69, 49);
                    case (byte)85:
                        return new OfficialColor(192, 192, 192);
                    case (byte)91:
                        return new OfficialColor(13, 88, 130);
                    case (byte)92:
                        return new OfficialColor(213, 229, 237);
                    case (byte)95:
                        return new OfficialColor(255, 162, 31);
                    case (byte)103:
                        return new OfficialColor(141, 98, 77);
                    case (byte)105:
                        if ((int)this.option() == 1)
                            return new OfficialColor(177, 92, 31);
                        if ((int)this.option() == 2)
                            return new OfficialColor(201, 188, 170);
                        else
                            return new OfficialColor(144, 148, 144);
                    case (byte)107:
                    case (byte)121:
                        return new OfficialColor(11, 80, 143);
                    case (byte)108:
                    case (byte)122:
                        return new OfficialColor(91, 169, 169);
                    case (byte)109:
                        return new OfficialColor(78, 193, 227);
                    case (byte)110:
                        return new OfficialColor(48, 186, 135);
                    case (byte)111:
                    case (byte)150:
                        return new OfficialColor(128, 26, 52);
                    case (byte)112:
                        return new OfficialColor(103, 98, 122);
                    case (byte)113:
                        return new OfficialColor(48, 208, 234);
                    case (byte)115:
                        return new OfficialColor(33, 171, 207);
                    case (byte)116:
                    case (byte)118:
                        return new OfficialColor(238, 225, 218);
                    case (byte)117:
                        return new OfficialColor(181, 172, 190);
                    case (byte)119:
                        return new OfficialColor(107, 92, 108);
                    case (byte)123:
                        return new OfficialColor(106, 107, 118);
                    case (byte)124:
                        return new OfficialColor(73, 51, 36);
                    case (byte)125:
                        return new OfficialColor(141, 175, 255);
                    case (byte)126:
                        return new OfficialColor(159, 209, 229);
                    case (byte)129:
                        return new OfficialColor(255, 117, 224);
                    case (byte)131:
                        return new OfficialColor(52, 52, 52);
                    case (byte)133:
                        if ((int)this.option() == 0)
                            return new OfficialColor(231, 53, 56);
                        else
                            return new OfficialColor(192, 189, 221);
                    case (byte)134:
                        if ((int)this.option() == 0)
                            return new OfficialColor(166, 187, 153);
                        else
                            return new OfficialColor(241, 129, 249);
                    case (byte)136:
                        return new OfficialColor(213, 203, 204);
                    case (byte)137:
                        if ((int)this.option() == 0)
                            return new OfficialColor(144, 148, 144);
                        else
                            return new OfficialColor(141, 56, 0);
                    case (byte)141:
                        return new OfficialColor(192, 59, 59);
                    case (byte)145:
                        return new OfficialColor(192, 30, 30);
                    case (byte)146:
                        return new OfficialColor(43, 192, 30);
                    case (byte)147:
                    case (byte)148:
                        return new OfficialColor(211, 236, 241);
                    case (byte)149:
                        if (j % 3 == 0)
                            return new OfficialColor(220, 50, 50);
                        if (j % 3 == 1)
                            return new OfficialColor(0, 220, 50);
                        else
                            return new OfficialColor(50, 50, 220);
                    case (byte)151:
                    case (byte)154:
                        return new OfficialColor(190, 171, 94);
                    case (byte)152:
                        return new OfficialColor(128, 133, 184);
                    case (byte)153:
                        return new OfficialColor(239, 141, 126);
                    case (byte)155:
                        return new OfficialColor(131, 162, 161);
                    case (byte)156:
                        return new OfficialColor(170, 171, 157);
                    case (byte)157:
                        return new OfficialColor(104, 100, 126);
                    case (byte)158:
                    case (byte)232:
                        return new OfficialColor(145, 81, 85);
                    case (byte)159:
                        return new OfficialColor(148, 133, 98);
                    case (byte)160:
                        if (j % 3 == 0)
                            return new OfficialColor(200, 0, 0);
                        if (j % 3 == 1)
                            return new OfficialColor(0, 200, 0);
                        else
                            return new OfficialColor(0, 0, 200);
                    case (byte)161:
                        return new OfficialColor(144, 195, 232);
                    case (byte)162:
                        return new OfficialColor(184, 219, 240);
                    case (byte)163:
                        return new OfficialColor(174, 145, 214);
                    case (byte)164:
                        return new OfficialColor(218, 182, 204);
                    case (byte)165:
                        if ((int)this.option() == 0)
                            return new OfficialColor(115, 173, 229);
                        if ((int)this.option() == 1)
                            return new OfficialColor(100, 100, 100);
                        if ((int)this.option() == 2)
                            return new OfficialColor(152, 152, 152);
                        else
                            return new OfficialColor(227, 125, 22);
                    case (byte)166:
                    case (byte)175:
                        return new OfficialColor(129, 125, 93);
                    case (byte)167:
                        return new OfficialColor(62, 82, 114);
                    case (byte)168:
                    case (byte)176:
                        return new OfficialColor(132, 157, (int)sbyte.MaxValue);
                    case (byte)169:
                    case (byte)177:
                        return new OfficialColor(152, 171, 198);
                    case (byte)170:
                        return new OfficialColor(228, 219, 162);
                    case (byte)171:
                        return new OfficialColor(177, 192, 176);
                    case (byte)172:
                        return new OfficialColor(181, 194, 217);
                    case (byte)178:
                        if ((int)this.option() == 0)
                            return new OfficialColor(208, 94, 201);
                        if ((int)this.option() == 1)
                            return new OfficialColor(233, 146, 69);
                        if ((int)this.option() == 2)
                            return new OfficialColor(71, 146, 251);
                        if ((int)this.option() == 3)
                            return new OfficialColor(60, 226, 133);
                        if ((int)this.option() == 4)
                            return new OfficialColor(250, 30, 71);
                        if ((int)this.option() == 6)
                            return new OfficialColor(255, 217, 120);
                        else
                            return new OfficialColor(166, 176, 204);
                    case (byte)179:
                        return new OfficialColor(49, 134, 114);
                    case (byte)180:
                        return new OfficialColor(126, 134, 49);
                    case (byte)181:
                        return new OfficialColor(134, 59, 49);
                    case (byte)182:
                        return new OfficialColor(43, 86, 140);
                    case (byte)183:
                        return new OfficialColor(121, 49, 134);
                    case (byte)184:
                        if ((int)this.option() == 0)
                            return new OfficialColor(29, 106, 88);
                        if ((int)this.option() == 1)
                            return new OfficialColor(94, 100, 36);
                        if ((int)this.option() == 2)
                            return new OfficialColor(96, 44, 40);
                        if ((int)this.option() == 3)
                            return new OfficialColor(34, 63, 102);
                        else
                            return new OfficialColor(79, 35, 95);
                    case (byte)185:
                    case (byte)186:
                    case (byte)187:
                        if ((int)this.option() == 0)
                            return new OfficialColor(99, 99, 99);
                        if ((int)this.option() == 1)
                            return new OfficialColor(114, 81, 56);
                        if ((int)this.option() == 2)
                            return new OfficialColor(133, 133, 101);
                        if ((int)this.option() == 3)
                            return new OfficialColor(151, 200, 211);
                        if ((int)this.option() == 4)
                            return new OfficialColor(177, 183, 161);
                        if ((int)this.option() == 5)
                            return new OfficialColor(134, 114, 38);
                        if ((int)this.option() == 6)
                            return new OfficialColor(82, 62, 66);
                        if ((int)this.option() == 7)
                            return new OfficialColor(143, 117, 121);
                        if ((int)this.option() == 8)
                            return new OfficialColor(177, 92, 31);
                        else
                            return new OfficialColor(85, 73, 87);
                    case (byte)189:
                        return new OfficialColor(223, 255, 255);
                    case (byte)193:
                        return new OfficialColor(56, 121, 255);
                    case (byte)194:
                        return new OfficialColor(157, 157, 107);
                    case (byte)195:
                        return new OfficialColor(134, 22, 34);
                    case (byte)196:
                        return new OfficialColor(147, 144, 178);
                    case (byte)197:
                        return new OfficialColor(97, 200, 225);
                    case (byte)198:
                        return new OfficialColor(62, 61, 52);
                    case (byte)199:
                        return new OfficialColor(208, 80, 80);
                    case (byte)200:
                        return new OfficialColor(216, 152, 144);
                    case (byte)201:
                        return new OfficialColor(203, 61, 64);
                    case (byte)202:
                        return new OfficialColor(213, 178, 28);
                    case (byte)203:
                        return new OfficialColor(128, 44, 45);
                    case (byte)204:
                        return new OfficialColor(125, 55, 65);
                    case (byte)205:
                        return new OfficialColor(186, 50, 52);
                    case (byte)206:
                        return new OfficialColor(124, 175, 201);
                    case (byte)208:
                        return new OfficialColor(88, 105, 118);
                    case (byte)211:
                        return new OfficialColor(191, 233, 115);
                    case (byte)213:
                        return new OfficialColor(137, 120, 67);
                    case (byte)214:
                        return new OfficialColor(103, 103, 103);
                    case (byte)215:
                        return new OfficialColor(254, 121, 2);
                    case (byte)221:
                        return new OfficialColor(239, 90, 50);
                    case (byte)222:
                        return new OfficialColor(231, 96, 228);
                    case (byte)223:
                        return new OfficialColor(57, 85, 101);
                    case (byte)224:
                        return new OfficialColor(107, 132, 139);
                    case (byte)225:
                        return new OfficialColor(227, 125, 22);
                    case (byte)226:
                        return new OfficialColor(141, 56, 0);
                    case (byte)227:
                        if ((int)this.option() == 0)
                            return new OfficialColor(74, 197, 155);
                        if ((int)this.option() == 1)
                            return new OfficialColor(54, 153, 88);
                        if ((int)this.option() == 2)
                            return new OfficialColor(63, 126, 207);
                        if ((int)this.option() == 3)
                            return new OfficialColor(240, 180, 4);
                        if ((int)this.option() == 4)
                            return new OfficialColor(45, 68, 168);
                        if ((int)this.option() == 5)
                            return new OfficialColor(61, 92, 0);
                        if ((int)this.option() == 6)
                            return new OfficialColor(216, 112, 152);
                        else
                            return new OfficialColor(200, 40, 24);
                    case (byte)229:
                        return new OfficialColor(255, 156, 12);
                    case (byte)230:
                        return new OfficialColor(131, 79, 13);
                    case (byte)231:
                        return new OfficialColor(224, 194, 101);
                    case (byte)233:
                        return new OfficialColor(107, 182, 29);
                    case (byte)234:
                        return new OfficialColor(53, 44, 41);
                    case (byte)235:
                        return new OfficialColor(214, 184, 46);
                    case (byte)236:
                        return new OfficialColor(149, 232, 87);
                    case (byte)237:
                        return new OfficialColor(255, 241, 51);
                    case (byte)238:
                        return new OfficialColor(225, 128, 206);
                    case (byte)239:
                        return new OfficialColor(224, 194, 101);
                    case (byte)240:
                        if ((int)this.option() == 1)
                            return new OfficialColor(99, 50, 30);
                        if ((int)this.option() == 2)
                            return new OfficialColor(153, 153, 117);
                        else
                            return new OfficialColor(120, 85, 60);
                    case (byte)241:
                        return new OfficialColor(77, 74, 72);
                    case (byte)242:
                    case (byte)245:
                    case (byte)246:
                        return new OfficialColor(99, 50, 30);
                    case (byte)244:
                        return new OfficialColor(200, 245, 253);
                    case (byte)247:
                        return new OfficialColor(140, 150, 150);
                }
            }
            if (this.lava())
                return new OfficialColor(253, 32, 3);
            if (this.water())
                return new OfficialColor(9, 61, 191);
            if (this.honey())
                return new OfficialColor(254, 194, 20);
            if (this.wall())
            {
                switch (this.type)
                {
                    case (byte)1:
                    case (byte)5:
                    case (byte)44:
                    case (byte)48:
                    case (byte)49:
                    case (byte)50:
                    case (byte)51:
                    case (byte)52:
                    case (byte)53:
                        return new OfficialColor(52, 52, 52);
                    case (byte)2:
                    case (byte)16:
                    case (byte)59:
                        return new OfficialColor(88, 61, 46);
                    case (byte)3:
                        return new OfficialColor(61, 58, 78);
                    case (byte)4:
                        return new OfficialColor(73, 51, 36);
                    case (byte)6:
                        return new OfficialColor(91, 30, 30);
                    case (byte)7:
                    case (byte)17:
                        return new OfficialColor(27, 31, 42);
                    case (byte)8:
                    case (byte)18:
                        return new OfficialColor(31, 39, 26);
                    case (byte)9:
                    case (byte)19:
                        return new OfficialColor(41, 28, 36);
                    case (byte)10:
                        return new OfficialColor(74, 62, 12);
                    case (byte)11:
                        return new OfficialColor(46, 56, 59);
                    case (byte)12:
                        return new OfficialColor(75, 32, 11);
                    case (byte)13:
                        return new OfficialColor(67, 37, 37);
                    case (byte)14:
                    case (byte)20:
                        return new OfficialColor(15, 15, 15);
                    case (byte)15:
                        return new OfficialColor(52, 43, 45);
                    case (byte)22:
                        return new OfficialColor(113, 99, 99);
                    case (byte)23:
                        return new OfficialColor(38, 38, 43);
                    case (byte)24:
                        return new OfficialColor(53, 39, 41);
                    case (byte)25:
                        return new OfficialColor(11, 35, 62);
                    case (byte)26:
                        return new OfficialColor(21, 63, 70);
                    case (byte)27:
                        if (j % 2 == 0)
                            return new OfficialColor(88, 61, 46);
                        else
                            return new OfficialColor(52, 52, 52);
                    case (byte)28:
                        return new OfficialColor(81, 84, 101);
                    case (byte)29:
                        return new OfficialColor(88, 23, 23);
                    case (byte)30:
                        return new OfficialColor(28, 88, 23);
                    case (byte)31:
                        return new OfficialColor(78, 87, 99);
                    case (byte)32:
                        return new OfficialColor(86, 17, 40);
                    case (byte)33:
                        return new OfficialColor(49, 47, 83);
                    case (byte)34:
                    case (byte)37:
                        return new OfficialColor(69, 67, 41);
                    case (byte)35:
                        return new OfficialColor(51, 51, 70);
                    case (byte)36:
                        return new OfficialColor(87, 59, 55);
                    case (byte)38:
                        return new OfficialColor(49, 57, 49);
                    case (byte)39:
                        return new OfficialColor(78, 79, 73);
                    case (byte)40:
                        return new OfficialColor(85, 102, 103);
                    case (byte)41:
                        return new OfficialColor(52, 50, 62);
                    case (byte)42:
                        return new OfficialColor(71, 42, 44);
                    case (byte)43:
                        return new OfficialColor(73, 66, 50);
                    case (byte)45:
                        return new OfficialColor(60, 59, 51);
                    case (byte)46:
                        return new OfficialColor(48, 57, 47);
                    case (byte)47:
                        return new OfficialColor(71, 77, 85);
                    case (byte)54:
                        return new OfficialColor(40, 56, 50);
                    case (byte)55:
                        return new OfficialColor(49, 48, 36);
                    case (byte)56:
                        return new OfficialColor(43, 33, 32);
                    case (byte)57:
                        return new OfficialColor(31, 40, 49);
                    case (byte)58:
                        return new OfficialColor(48, 35, 52);
                    case (byte)60:
                        return new OfficialColor(1, 52, 20);
                    case (byte)61:
                        return new OfficialColor(55, 39, 26);
                    case (byte)62:
                        return new OfficialColor(39, 33, 26);
                    case (byte)63:
                    case (byte)65:
                    case (byte)66:
                    case (byte)68:
                        return new OfficialColor(30, 80, 48);
                    case (byte)64:
                    case (byte)67:
                        return new OfficialColor(53, 80, 30);
                    case (byte)69:
                        return new OfficialColor(43, 42, 68);
                    case (byte)70:
                        return new OfficialColor(30, 70, 80);
                    case (byte)71:
                        return new OfficialColor(78, 105, 135);
                    case (byte)72:
                        return new OfficialColor(52, 84, 12);
                    case (byte)73:
                        return new OfficialColor(190, 204, 223);
                    case (byte)74:
                    case (byte)80:
                        return new OfficialColor(64, 62, 80);
                    case (byte)75:
                        return new OfficialColor(65, 65, 35);
                    case (byte)76:
                        return new OfficialColor(20, 46, 104);
                    case (byte)77:
                        return new OfficialColor(61, 13, 16);
                    case (byte)78:
                        return new OfficialColor(63, 39, 26);
                    case (byte)79:
                        return new OfficialColor(51, 47, 96);
                    case (byte)81:
                        return new OfficialColor(101, 51, 51);
                    case (byte)82:
                        return new OfficialColor(77, 64, 34);
                    case (byte)83:
                        return new OfficialColor(62, 38, 41);
                    case (byte)84:
                        return new OfficialColor(48, 78, 93);
                    case (byte)85:
                        return new OfficialColor(54, 63, 69);
                    case (byte)86:
                    case (byte)108:
                        return new OfficialColor(138, 73, 38);
                    case (byte)87:
                        return new OfficialColor(50, 15, 8);
                    case (byte)94:
                    case (byte)100:
                        return new OfficialColor(32, 40, 45);
                    case (byte)95:
                    case (byte)101:
                        return new OfficialColor(44, 41, 50);
                    case (byte)96:
                    case (byte)102:
                        return new OfficialColor(72, 50, 77);
                    case (byte)97:
                    case (byte)103:
                        return new OfficialColor(78, 50, 69);
                    case (byte)98:
                    case (byte)104:
                        return new OfficialColor(36, 45, 44);
                    case (byte)99:
                    case (byte)105:
                        return new OfficialColor(38, 49, 50);
                }
            }
            /*
            if ((double)j < Main.worldSurface)
            {
                float num1 = (float)j / (float)Main.worldSurface;
                float num2 = 1f - num1;
                OfficialColor color1 = new OfficialColor((int)(byte)((double)num2 * 50.0), (int)(byte)((double)num2 * 40.0), (int)(byte)((double)num2 * (double)byte.MaxValue));
                OfficialColor color2 = new OfficialColor((int)(byte)((double)num1 * 145.0), (int)(byte)((double)num1 * 185.0), (int)(byte)((double)num1 * (double)byte.MaxValue));
                OfficialColor color3 = new OfficialColor((int)color1.R + (int)color2.R, (int)color1.G + (int)color2.G, (int)color1.B + (int)color2.B);
                float num3 = 1f;
                color3 = new OfficialColor((int)(byte)((double)color3.R * (double)num3), (int)(byte)((double)color3.G * (double)num3), (int)(byte)((double)color3.B * (double)num3), (int)(byte)((double)byte.MaxValue * (double)num3));
                return color3;
            }
            else if ((double)j < Main.rockLayer)
            {
                OfficialColor color1 = new OfficialColor(88, 61, 46);
                OfficialColor color2 = new OfficialColor(37, 78, 123);
                float num1 = (float)this.type / (float)byte.MaxValue;
                float num2 = 1f - num1;
                return new OfficialColor((int)(byte)((double)color1.R * (double)num2 + (double)color2.R * (double)num1), (int)(byte)((double)color1.G * (double)num2 + (double)color2.G * (double)num1), (int)(byte)((double)color1.B * (double)num2 + (double)color2.B * (double)num1));
            }
            else
            {
                if (j >= Main.maxTilesY - 200)
                    return new OfficialColor(50, 44, 38);
                OfficialColor color1 = new OfficialColor(74, 67, 60);
                OfficialColor color2 = new OfficialColor(53, 70, 97);
                float num1 = (float)this.type / (float)byte.MaxValue;
                float num2 = 1f - num1;
                return new OfficialColor((int)(byte)((double)color1.R * (double)num2 + (double)color2.R * (double)num1), (int)(byte)((double)color1.G * (double)num2 + (double)color2.G * (double)num1), (int)(byte)((double)color1.B * (double)num2 + (double)color2.B * (double)num1));
            }*/
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