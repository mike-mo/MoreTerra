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

            for (int i = 0; i < tiles.Count; i++)
            {
                useMap.type = (byte)i;

                ti = tiles[i];

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

            for (int i = 1; i <= walls.Count; i++)
            {
                useMap.type = (byte)i;

                wi = walls[i];

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
    }
}
