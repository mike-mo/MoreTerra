using MoreTerra.Structures;
using MoreTerra.Structures.TerraInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoreTerra.Utilities
{
    static class MapDumper
    {
        public static void DumpNewTiles()
        {
            Dictionary<int, TileInfo> tiles = Global.Instance.Info.Tiles;
            TileInfo ti;
            Map useMap = new Map();
            useMap.active(true);
            String tileXML;
            FileStream stream = new FileStream("newTileXML.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

			string[] tileNames = null;// Enum.GetNames(typeof(MoreTerra.Enums.TileEnum));


			for (int i = 0; i < 420; i++)
			{

				if (!tiles.TryGetValue(i, out ti))
				{
					ti = new TileInfo();
					ti.name = tileNames[i];
					ti.tileImage = i;
					ti.colorName = "Unknown";
				}

				useMap.type = (ushort)i;

                tileXML = "    <tile ";
                if (ti.name != null)
                    tileXML = tileXML + "name=\"" + ti.name + "\" ";

                tileXML = tileXML + "tileImage=\"" + i + "\" ";

                if (ti.important)
                    tileXML = tileXML + "important=\"true\" ";

                if (!String.IsNullOrEmpty(ti.colorName))
                    tileXML = tileXML + "color=\"" + ti.colorName + "\" ";
                else
                    tileXML = tileXML + String.Format("color=\"#{0:X2}{1:X2}{2:X2}\" ", ti.color.R, ti.color.G, ti.color.B);

                OfficialColor c = useMap.tileColor(0);
				if (c == null) c = new OfficialColor(ti.officialColor);
                tileXML = tileXML + String.Format("officialColor=\"#{0:X2}{1:X2}{2:X2}\" ", c.R, c.G, c.B);

                if (!String.IsNullOrEmpty(ti.markerName))
                    tileXML = tileXML + "marker=\"" + ti.markerName + "\" ";

                tileXML = tileXML + "/>";

                writer.WriteLine(tileXML);
            }
            writer.Close();
        }

        public static void DumpNewWalls()
        {
            Dictionary<int, WallInfo> walls = Global.Instance.Info.Walls;
            WallInfo wi;
            Map useMap = new Map();
            useMap.wall(true);
            String tileXML;
            FileStream stream = new FileStream("newWallXML.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

			string[] wallNames = null;// Enum.GetNames(typeof(MoreTerra.Enums.WallEnum));

			for (int i = 1; i <= 224; i++)
            {
                useMap.type = (byte)i;

				if (!walls.TryGetValue(i, out wi))
				{
					wi = new WallInfo();
					wi.name = wallNames[i];
					wi.wallImage = i;
					wi.colorName = "Unknown";
				}

                tileXML = "    <wall ";
                if (wi.name != null)
                    tileXML = tileXML + "name=\"" + wi.name + "\" ";

                tileXML = tileXML + "wallImage=\"" + i + "\" ";

                if (!String.IsNullOrEmpty(wi.colorName))
                    tileXML = tileXML + "color=\"" + wi.colorName + "\" ";
                else
                    tileXML = tileXML + String.Format("color=\"#{0:X2}{1:X2}{2:X2}\" ", wi.color.R, wi.color.G, wi.color.B);

                OfficialColor c = useMap.tileColor(0);
                tileXML = tileXML + String.Format("officialColor=\"#{0:X2}{1:X2}{2:X2}\" ", c.R, c.G, c.B);

                tileXML = tileXML + "/>";
                //            <wall name="Blue Dungeon Brick" wallImage="7" unsafe="true" color="Wall Dungeon Blue" />

                writer.WriteLine(tileXML);
            }
            writer.Close();
        }

		public static void FixImportance(bool[] importance)
		{
			Dictionary<int, TileInfo> tiles = Global.Instance.Info.Tiles;

			for (int i = 0; i < importance.Length; i++)
			{
				TileInfo ti = tiles[i];

				if (ti.important != importance[i])
				{
					ti.important = importance[i];
				}
			}
		}
    }
}
