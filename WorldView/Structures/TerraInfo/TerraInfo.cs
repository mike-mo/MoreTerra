using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace MoreTerra.Structures.TerraInfo
{
	public class TerraInfo
	{
        private Dictionary<Int32, MarkerInfo> markers;
        private Dictionary<String, List<MarkerInfo>> markerSets;
		private Dictionary<Int32, TileInfo> tiles;
		private Dictionary<Int32, WallInfo> walls;
		private Dictionary<String, ItemInfo> items;
		private Dictionary<String, RenameInfo> renames;
		private Dictionary<String, ColorInfo> colors;
		private Dictionary<String, NpcInfo> npcs;
		private Dictionary<String, List<SpecialObjectInfo>> specialobjects;
		private Dictionary<Int32, String> itemNames;

		private StringBuilder errorLog;

		public TerraInfo()
		{
            markers = new Dictionary<Int32, MarkerInfo>();
			markerSets = new Dictionary<String, List<MarkerInfo>>();
			items = new Dictionary<String, ItemInfo>();
			itemNames = new Dictionary<Int32, String>();
			renames = new Dictionary<String, RenameInfo>();
			colors = new Dictionary<String, ColorInfo>();
			npcs = new Dictionary<String, NpcInfo>();

			tiles = new Dictionary<Int32, TileInfo>();
			walls = new Dictionary<Int32, WallInfo>();

			specialobjects = new Dictionary<String, List<SpecialObjectInfo>>();
			 
			errorLog = new StringBuilder();
		}

        public String LoadInfo(String itemXmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.LoadXml(itemXmlFile);

            }
            catch (FileNotFoundException e)
            {
                errorLog.AppendLine(e.Message);
                return errorLog.ToString();
            }

            XmlNode dataNode = xmlDoc.DocumentElement;

            LoadMarkers(dataNode.SelectSingleNode("markers"));

            LoadRenames(dataNode.SelectSingleNode("namechanges").SelectNodes("item"));
            LoadNpcs(dataNode.SelectSingleNode("npcs").SelectNodes("npc"));
            LoadItems(dataNode.SelectSingleNode("items").SelectNodes("item"));

            LoadColors(dataNode.SelectSingleNode("colors").SelectNodes("color"));
            LoadTiles(dataNode.SelectSingleNode("tiles").SelectNodes("tile"));
            LoadWalls(dataNode.SelectSingleNode("walls").SelectNodes("wall"));

            LoadSpecialObjects(dataNode.SelectSingleNode("specialobjects").SelectNodes("object"));

            SetupItemNames();

            return errorLog.ToString();
        }

        public String SaveInfo(String toFile)
        {
            FileStream stream = new FileStream(toFile, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            writer.WriteLine("<data>");

            SaveTiles(writer);
            SaveWalls(writer);
            SaveItems(writer);

            SaveRenames(writer);
            SaveNPCs(writer);
            SaveColors(writer);
            SaveMarkers(writer);
            SaveSpecialObjects(writer);

            writer.WriteLine("</data>");
            writer.Close();

            return errorLog.ToString();
        }


#region Load Functions
        private void LoadMarkers(XmlNode markerNodes)
		{
			Int32 setCount = -1;
            Int32 markerCount = -1;

            if ((markerNodes == null) || (markerNodes.ChildNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Marker items to load.");
				return;
			}

            XmlNodeList markerSetNodes = markerNodes.SelectNodes("markerset");

			foreach (XmlNode setNode in markerSetNodes)
			{
                String setName = String.Empty;
                List<MarkerInfo> curSet;

                foreach (XmlAttribute att in setNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
                            setName = att.Value;
							break;
                        default:
                            errorLog.AppendLine(String.Format("Marker Set #{0} has unknown attribute \"{1}\" has value \"{2}\"",
                                setCount, att.Name, att.Value));
							break;
                    }

                    if (setName == String.Empty)
                    {
                        errorLog.AppendLine(String.Format("Marker Set #{0} has an empty name attribute.", setCount));
                        continue;
                    }

                    if (markerSets.ContainsKey(setName))
							{
                        errorLog.AppendLine(String.Format("Marker Set #{0} has a duplicate name. Value=\"{1}\"",
                            setCount, setName));
								continue;
							}
                    else
                    {
                        curSet = new List<MarkerInfo>();
                        markerSets.Add(setName, curSet);
                    }

                    XmlNodeList markerNodeList = setNode.SelectNodes("marker");

                    foreach (XmlNode markerNode in markerNodeList)
                    {

                        String name = String.Empty;
                        String markerImage = String.Empty;

                        markerCount++;

                        foreach (XmlAttribute markerAtt in markerNode.Attributes)
                        {

                            switch (markerAtt.Name)
                            {
                                case "name":
                                    name = markerAtt.Value;
							break;
						default:
							errorLog.AppendLine(String.Format("Marker #{0} has unknown attribute \"{1}\" has value \"{2}\"",
                                        markerCount, markerAtt.Name, markerAtt.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
                            errorLog.AppendLine(String.Format("Marker #{0} had no name attribute.", markerCount));
					continue;
				}

                        if (name.IndexOf(" ") != -1)
				{
                            String[] imageSegments = name.Split(' ');

                            markerImage = "";
                            foreach (String str in imageSegments)
					{
                                markerImage += str;
					}
				}
                        else
                        {
					markerImage = name;
				}

				MarkerInfo marker = new MarkerInfo();
				marker.name = name;
                        marker.markerSet = setName;
				marker.markerImage = markerImage;

                        curSet.Add(marker);
                        markers.Add(markerCount, marker);
                    }
                }
			}

		}

		private void LoadNpcs(XmlNodeList npcNodes)
		{
			Int32 count = -1;

			if ((npcNodes == null) || (npcNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Npc items to load.");
				return;
			}

			foreach (XmlNode npcNode in npcNodes)
			{
				String name = String.Empty;

				Int32 imageId = -1;
				count++;

				foreach (XmlAttribute att in npcNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "npcImage":
							if (Int32.TryParse(att.Value, out imageId) == false)
							{
								errorLog.AppendLine(String.Format("Npc #{0} has an invalid itemImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if (imageId < 0)
							{
								errorLog.AppendLine(String.Format("Npc #{0} had a out of range itemImage attribute.  Value=\"{1}\"",
									count, imageId));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("Npc #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Npc #{0} had no name attribute.", count));
					continue;
				}

				if (imageId == -1)
				{
					errorLog.AppendLine(String.Format("Npc #{0} had no npcImage attribute.", count));
					continue;
				}


				if (npcs.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Npc #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				NpcInfo npc = new NpcInfo();
				npc.name = name;
				npc.imageId = imageId;

				npcs.Add(name, npc);
			}
		}

		private void LoadWalls(XmlNodeList wallNodes)
		{
			Int32 count = -1;

			if ((wallNodes == null) || (wallNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Wall items to load.");
				return;
			}

			foreach (XmlNode wallNode in wallNodes)
			{
				String name = String.Empty;
				String color = String.Empty;
                String officialColor = String.Empty;
				Color useColor;
                Color useOfficialColor;
				Boolean safe = false;
                Boolean transparent = false;

				Int32 wallImage = -1;
				count++;

				foreach (XmlAttribute att in wallNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "color":
							color = att.Value;
							break;
                        case "officialColor":
                            officialColor = att.Value;
                            break;
						case "wallImage":
							if (Int32.TryParse(att.Value, out wallImage) == false)
							{
								errorLog.AppendLine(String.Format("Wall #{0} has an invalid wallImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if (wallImage < 0)
							{
								errorLog.AppendLine(String.Format("Wall #{0} had an out of range wallImage attribute.  Value=\"{1}\"",
									count, wallImage));
								continue;
							}
							break;
						case "unsafe":
							if (Boolean.TryParse(att.Value, out safe) == false)
							{
								errorLog.AppendLine(String.Format("Wall #{0} had an invalid unsafe attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}
							safe = !safe;
							break;
                        case "transparent":
							if (Boolean.TryParse(att.Value, out transparent) == false)
							{
								errorLog.AppendLine(String.Format("Wall #{0} had an invalid transparent attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}
							break;
						default:
								errorLog.AppendLine(String.Format("Wall #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Wall #{0} had no name attribute.", count));
					continue;
				}

                if (color == String.Empty)
                {
                    errorLog.AppendLine(String.Format("Wall #{0} had no color attribute.", count));
                    continue;
                }

                if (Global.TryParseColor(color, out useColor) == false)
                {
                    if (!colors.ContainsKey(color))
                    {
                        errorLog.AppendLine(String.Format("Wall #{0} had a color attribute that was not a color or a color lookup name. Value=\"{1}\"",
                            count, color));
                        continue;
                    }
                    else
                    {
                        useColor = colors[color].color;
                    }
                }
                else
                {
                    color = String.Empty;
                }

                if (officialColor == String.Empty)
                {
                    errorLog.AppendLine(String.Format("Wall #{0} had no officialColor attribute.", count));
                    continue;
                }

                if (Global.TryParseColor(officialColor, out useOfficialColor) == false)
                {
                    errorLog.AppendLine(String.Format("Wall #{0} had a officialColor attribute that was not a color. Value=\"{1}\"",
                            count, officialColor));
                    continue;
                }
                else
                {
                    officialColor = String.Empty;
                }

                if (wallImage == -1)
				{
					errorLog.AppendLine(String.Format("Wall #{0} had no wallImage attribute.", count));
					continue;
				}

				if (walls.ContainsKey(wallImage))
				{
					errorLog.AppendLine(String.Format("Wall #{0} had a duplicate wallImage to {1}.", count, wallImage));
					continue;
				}

				WallInfo wall = new WallInfo();
				wall.name = name;
				wall.wallImage = wallImage;
				wall.colorName = color;
				wall.color = useColor;
                wall.officialColor = useOfficialColor;
                wall.transparent = transparent;
                wall.color = wall.transparent ? Color.FromArgb(0,useColor) : useColor;
                
				walls.Add(wallImage, wall);
			}
		}

		private void LoadTiles(XmlNodeList tileNodes)
		{
			Int32 count = -1;

			if ((tileNodes == null) || (tileNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Tile items to load.");
				return;
			}

			foreach (XmlNode tileNode in tileNodes)
			{
				String name = String.Empty;
				String color = String.Empty;
                String officialColor = String.Empty;
				String marker = String.Empty;
				Color useColor;
                Color useOfficialColor;
                Boolean important = false;

				Int32 tileImage = -1;

				count++;

				foreach (XmlAttribute att in tileNode.Attributes)
				{

					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "tileImage":
							if (Int32.TryParse(att.Value, out tileImage) == false)
							{
								errorLog.AppendLine(String.Format("Tile #{0} has an invalid tileImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if ((tileImage < 0) || (tileImage >= TileProperties.TYPES))
							{
								errorLog.AppendLine(String.Format("Tile #{0} had a out of range numMade attribute.  Value=\"{1}\"",
									count, tileImage));
								continue;
							}
							break;
						case "important":
							if (!Boolean.TryParse(att.Value, out important))
                            {
                                errorLog.AppendLine(String.Format("Tile #{0} had an invalid important attribute. Value=\"{1}\"",
                                    count, att.Value));
                                continue;
                            }
							break;
						case "color":
							color = att.Value;
							break;
                        case "officialColor":
                            officialColor = att.Value;
                            break;
						case "marker":
							marker = att.Value;
							break;
						default:
							errorLog.AppendLine(String.Format("Tile #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Tile #{0} had no name attribute.", count));
					continue;
				}

				if (tileImage == -1)
				{
					errorLog.AppendLine(String.Format("Tile #{0} had no tileImage attribute.", count));
					continue;
				}

				if (tiles.ContainsKey(tileImage))
				{
					errorLog.AppendLine(String.Format("Tile #{0} had a duplicate tileImage value to \"{1}\"",
						count, tileImage));
					continue;
				}

                if (color == String.Empty)
                {
                    errorLog.AppendLine(String.Format("Tile #{0} had no color attribute.", count));
                    continue;
                }

                if (Global.TryParseColor(color, out useColor) == false)
				{
					if (!colors.ContainsKey(color))
					{
						errorLog.AppendLine(String.Format("Tile #{0} had a color attribute that was not a color or a color lookup name. Value=\"{1}\"",
							count, color));
						continue;
					}
					else
					{
						useColor = colors[color].color;
					}
				}
                else
                {
                    color = String.Empty;
                }

                if (officialColor == String.Empty)
                {
                    //errorLog.AppendLine(String.Format("Tile #{0} had no officialColor attribute.", count));
                    officialColor = "#FF00FF";
//                    continue;
                }

                if (Global.TryParseColor(officialColor, out useOfficialColor) == false)
                {
                    errorLog.AppendLine(String.Format("Tile #{0} had an officialColor attribute that was not a color. Value=\"{1}\"",
                        count, officialColor));
                    continue;
                }
                else
                {
                    officialColor = String.Empty;
                }

				TileInfo tile = new TileInfo();
				tile.name = name;
				tile.colorName = color;
				tile.color = useColor;
                tile.important = important;
                tile.officialColor = useOfficialColor;
				tile.markerName = marker;
				tile.tileImage = tileImage;

				tiles.Add(tileImage, tile);
			}

		}

		private void LoadItems(XmlNodeList itemNodes)
		{
			Int32 count = -1;

			if ((itemNodes == null) || (itemNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Item items to load.");
				return;
			}

			foreach (XmlNode itemNode in itemNodes)
			{
				String name = String.Empty;
				String found = String.Empty;

				Int32 netId = 0;
				Int32 imageId = -1;
				count++;

				foreach (XmlAttribute att in itemNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "netId":
							if (Int32.TryParse(att.Value, out netId) == false)
							{
								errorLog.AppendLine(String.Format("Item #{0} has an invalid netId attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}
							break;
						case "itemImage":
							if (Int32.TryParse(att.Value, out imageId) == false)
							{
								errorLog.AppendLine(String.Format("Item #{0} has an invalid itemImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if (imageId < 0)
							{
								errorLog.AppendLine(String.Format("Item #{0} had a out of range itemImage attribute.  Value=\"{1}\"",
									count, imageId));
								continue;
							}
							break;
						case "foundIn":
							found = att.Value;
							break;
						default:
							errorLog.AppendLine(String.Format("Item #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Item #{0} had no name attribute.", count));
					continue;
				}

				if (imageId == -1)
				{
					errorLog.AppendLine(String.Format("Item #{0} had no itemImage attribute.", count));
					continue;
				}

				if (netId == 0)
					netId = imageId;

				if (items.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Item #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				ItemInfo item = new ItemInfo();
				item.name = name;
				item.foundIn = found;
				item.netId = netId;
				item.imageId = imageId;

				items.Add(name, item);
			}
		}

		private void LoadRenames(XmlNodeList renameNodes)
		{
			Int32 count = -1;

			if ((renameNodes == null) || (renameNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Rename items to load.");
				return;
			}

			foreach (XmlNode renameNode in renameNodes)
			{
				String name = String.Empty;
				String newName = String.Empty;
				count++;

				foreach (XmlAttribute att in renameNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "newName":
							newName = att.Value;
							break;
						default:
							errorLog.AppendLine(String.Format("Rename #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Rename #{0} had no name attribute.", count));
					continue;
				}

				if (newName == String.Empty)
				{
					errorLog.AppendLine(String.Format("Rename #{0} had no newName attribute.", count));
					continue;
				}

				if (renames.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Rename #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				RenameInfo rename = new RenameInfo();
				rename.name = name;
				rename.newName = newName;

				renames.Add(name, rename);
			}

		}

		private void LoadColors(XmlNodeList colorNodes)
		{
			Int32 count = -1;

			if ((colorNodes == null) || (colorNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Color items to load.");
				return;
			}

			foreach (XmlNode colorNode in colorNodes)
			{
				String name = String.Empty;
				Int32 red = -1;
				Int32 green = -1;
				Int32 blue = -1;
				Color useColor = Color.Black;
				Boolean hasColor = false;
				count++;

				foreach (XmlAttribute att in colorNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "red":
							if (Int32.TryParse(att.Value, out red) == false)
							{
								errorLog.AppendLine(String.Format("Color #{0} has an invalid red attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((red < 0) || (red > 255))
							{
								errorLog.AppendLine(String.Format("Color #{0} had a out of range red attribute.  Value=\"{1}\"",
									count, red));
								continue;
							}
							break;
						case "green":
							if (Int32.TryParse(att.Value, out green) == false)
							{
								errorLog.AppendLine(String.Format("Color #{0} has an invalid green attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((green < 0) || (green > 255))
							{
								errorLog.AppendLine(String.Format("Color #{0} had a out of range green attribute.  Value=\"{1}\"",
									count, green));
								continue;
							}
							break;
						case "blue":
							if (Int32.TryParse(att.Value, out blue) == false)
							{
								errorLog.AppendLine(String.Format("Color #{0} has an invalid blue attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((blue < 0) || (blue > 255))
							{
								errorLog.AppendLine(String.Format("Color #{0} had a out of range blue attribute.  Value=\"{1}\"",
									count, blue));
								continue;
							}
							break;
						case "colorString":
							hasColor = Global.TryParseColor(att.Value, out useColor);

							if (hasColor == false)
							{
								errorLog.AppendLine(String.Format("Color #{0} has a bad colorString value.  Value={1}",
									count, att.Value));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("Color #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Color #{0} had no name attribute.", count));
					continue;
				}

				if (hasColor == false)
				{
					if (red == -1)
					{
						errorLog.AppendLine(String.Format("Color #{0} had no colorString and no red attribute.", count));
						continue;
					}

					if (green == -1)
					{
						errorLog.AppendLine(String.Format("Color #{0} had no colorString and no green attribute.", count));
						continue;
					}

					if (blue == -1)
					{
						errorLog.AppendLine(String.Format("Color #{0} had no colorString and no blue attribute.", count));
						continue;
					}
					useColor = Color.FromArgb(red, green, blue);
				}
				else
				{
					if ((red != -1) && (useColor.R != red))
					{
						errorLog.AppendLine(String.Format("Color #{0} has both a colorString and a red attribute but they do not match.",
							count));
						continue;
					}

					if ((green != -1) && (useColor.G != green))
					{
						errorLog.AppendLine(String.Format("Color #{0} has both a colorString and a green attribute but they do not match.",
							count));
						continue;
					}

					if ((blue != -1) && (useColor.B != blue))
					{
						errorLog.AppendLine(String.Format("Color #{0} has both a colorString and a blue attribute but they do not match.",
							count));
						continue;
					}
				}

				if (colors.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Color #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				ColorInfo color = new ColorInfo();
				color.name = name;
				color.color = useColor;

				colors.Add(name, color);
			}
		}

		private void LoadSpecialObjects(XmlNodeList soNodes)
		{
			Int32 count = -1;

			if ((soNodes == null) || (soNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Special Object items to load.");
				return;
			}

			foreach (XmlNode objectNode in soNodes)
			{
				String name = String.Empty;
				String type = String.Empty;
				String color = String.Empty;
                String officialColor = String.Empty;
				Color useColor;
                Color useOfficialColor;

				count++;

				foreach (XmlAttribute att in objectNode.Attributes)
				{

					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "type":
							type = att.Value;
							break;
						case "color":
							color = att.Value;
							break;
                        case "officialColor":
                            officialColor = att.Value;
                            break;
						default:
							errorLog.AppendLine(String.Format("Special Object #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Special Object #{0} had no name attribute.", count));
					continue;
				}

				if (type == String.Empty)
				{
					errorLog.AppendLine(String.Format("Special Object #{0} had no type attribute.", count));
					continue;
				}

                if (color == String.Empty)
                {
                    errorLog.AppendLine(String.Format("Special Object #{0} had no color attribute.", count));
                    continue;
                }

                if (Global.TryParseColor(color, out useColor) == false)
                {
                    if (!colors.ContainsKey(color))
                    {
                        errorLog.AppendLine(String.Format("Special Object #{0} had a color attribute that was not a color or a color lookup name. Value=\"{1}\"",
                            count, color));
                        continue;
                    }
                    else
                    {
                        useColor = colors[color].color;
                    }
                }
                else
                {
                    color = String.Empty;
                }

                if (officialColor == String.Empty)
                {
                    errorLog.AppendLine(String.Format("Special Object #{0} had no officialColor attribute.", count));
                    continue;
                }

                if (Global.TryParseColor(officialColor, out useOfficialColor) == false)
                {
                    errorLog.AppendLine(String.Format("Special Object #{0} had a officialColor attribute that was not a color. Value=\"{1}\"",
                        count, officialColor));
                    continue;
                }

                SpecialObjectInfo so = new SpecialObjectInfo();
				so.name = name;
				so.type = type;
				so.colorName = color;
				so.color = useColor;
                so.officialColor = useOfficialColor;

                if (!specialobjects.ContainsKey(type))
					specialobjects.Add(type, new List<SpecialObjectInfo>());

				specialobjects[type].Add(so);
			}
		}

		private void SetupItemNames()
		{
			itemNames.Add(0, "Unknown Item");

			foreach (KeyValuePair<String, ItemInfo> kvp in items)
			{
				if (itemNames.ContainsKey(kvp.Value.netId))
				{
					errorLog.AppendLine(String.Format("Item {0} had a duplicate netId: \"{1}\"",
						kvp.Value.name, kvp.Value.netId));
				}
				else
				{
					itemNames.Add(kvp.Value.netId, kvp.Value.name);
				}
			}
		}
#endregion

#region Save Functions
        private void SaveTiles(StreamWriter writer)
        {
            Dictionary<int, TileInfo> tiles = Global.Instance.Info.Tiles;
            TileInfo ti;
            String tileXML;

            writer.WriteLine("  <tiles>");


            for (int i = 0; i < tiles.Count; i++)
            {
                ti = tiles[i];

                // Bit of a hack but this needs to stay in until it's coded properly.
                if (ti.name == "Copper Cache")
                {
                    writer.WriteLine();
                    writer.WriteLine("    <!-- Tiles after this are a hack and not meant to be kept past testing -->");
                }

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

                tileXML = tileXML + String.Format("officialColor=\"#{0:X2}{1:X2}{2:X2}\" ", 
                    ti.officialColor.R, ti.officialColor.G, ti.officialColor.B);

                if (!String.IsNullOrEmpty(ti.markerName))
                    tileXML = tileXML + "marker=\"" + ti.markerName + "\" ";

                tileXML = tileXML + "/>";

                writer.WriteLine(tileXML);

                if (i % 10 == 9)
                    writer.WriteLine();
            }
            writer.WriteLine("  </tiles>");
        }

        private void SaveWalls(StreamWriter writer)
        {
            Dictionary<int, WallInfo> walls = Global.Instance.Info.Walls;
            WallInfo wi;
            String wallXML;

            writer.WriteLine("  <walls>");

            for (int i = 1; i <= walls.Count; i++)
            {
                wi = walls[i];

                wallXML = "    <wall ";
                if (wi.name != null)
                    wallXML = wallXML + "name=\"" + wi.name + "\" ";

                wallXML = wallXML + "wallImage=\"" + i + "\" ";

                if (!String.IsNullOrEmpty(wi.colorName))
                    wallXML = wallXML + "color=\"" + wi.colorName + "\" ";
                else
                    wallXML = wallXML + String.Format("color=\"#{0:X2}{1:X2}{2:X2}\" ", wi.color.R, wi.color.G, wi.color.B);

                wallXML = wallXML + String.Format("officialColor=\"#{0:X2}{1:X2}{2:X2}\" ",
                    wi.officialColor.R, wi.officialColor.G, wi.officialColor.B);

                if (wi.transparent)
                    wallXML = wallXML + "transparent=\"true\" ";

                wallXML = wallXML + "/>";

                writer.WriteLine(wallXML);

                if (i % 10 == 9)
                    writer.WriteLine();
            }

            writer.WriteLine("  </walls>");
        }

        private void SaveItems(StreamWriter writer)
        {
            SortedDictionary<String, ItemInfo> sorted = new SortedDictionary<string,ItemInfo>();
            String itemXML;
            String prevLetter = "";
            String curLetter;
            String numbers = "1234567890";

            foreach (KeyValuePair<String, ItemInfo> kvp in items)
            {
                sorted.Add(kvp.Key, kvp.Value);
            }

            writer.WriteLine("  <items>");

            foreach (KeyValuePair<String, ItemInfo> kvp in sorted)
            {
                curLetter = kvp.Value.name.Substring(0, 1);

                if (numbers.IndexOf(curLetter) == -1)
                {
                    if (curLetter != prevLetter)
                    {
                        writer.WriteLine();
                        prevLetter = curLetter;
                    }
                }

                itemXML = String.Format("    <item name=\"{0}\" ", kvp.Value.name);
                
                if (!String.IsNullOrEmpty(kvp.Value.foundIn))
                    itemXML = itemXML + String.Format("foundIn=\"{0}\" ", kvp.Value.foundIn);

                itemXML = itemXML + String.Format("itemImage=\"{0}\" ", kvp.Value.imageId);

                if (kvp.Value.netId != kvp.Value.imageId)
                    itemXML = itemXML + String.Format("netId=\"{0}\" ", kvp.Value.netId);

                itemXML = itemXML + "/>";
                writer.WriteLine(itemXML);

            }

            writer.WriteLine("  </items>");
        }

        private void SaveRenames(StreamWriter writer)
        {
            writer.WriteLine("  <namechanges>");

            foreach (KeyValuePair<String, RenameInfo> kvp in renames)
            {
                writer.WriteLine(String.Format("    <item name=\"{0}\" newName=\"{1}\" />", kvp.Value.name, kvp.Value.newName));
            }

            writer.WriteLine("  </namechanges>");
        }

        private void SaveNPCs(StreamWriter writer)
        {
            writer.WriteLine("  <npcs>");

            foreach (KeyValuePair<String, NpcInfo> kvp in npcs)
            {
                writer.WriteLine(String.Format("    <npc name=\"{0}\" npcImage=\"{1}\" />", kvp.Value.name, kvp.Value.imageId));
            }

            writer.WriteLine("  </npcs>");
        }

        private void SaveColors(StreamWriter writer)
        {
            String colorXML;
            writer.WriteLine("  <colors>");

            foreach (KeyValuePair<String, ColorInfo> kvp in colors)
            {
                colorXML = String.Format("    <color name=\"{0}\" ", kvp.Value.name);

                colorXML = colorXML + String.Format("red=\"{0}\" green=\"{1}\" blue=\"{2}\" ", 
                    kvp.Value.color.R, kvp.Value.color.G, kvp.Value.color.B);

                colorXML = colorXML + "/>";

                writer.WriteLine(colorXML);
            }

            writer.WriteLine("  </colors>");
        }

        private void SaveMarkers(StreamWriter writer)
        {
            writer.WriteLine("  <markers>");
            foreach (KeyValuePair<String, List<MarkerInfo>> kvp in markerSets)
            {
                writer.WriteLine("    <markerset name=\"{0}\">", kvp.Key);

                foreach (MarkerInfo mi in kvp.Value)
                {
                    writer.WriteLine(String.Format("      <marker name=\"{0}\"  />", mi.name));
                }
                writer.WriteLine("    </markerset>");
            }
            writer.WriteLine("  </markers>");
        }

        private void SaveSpecialObjects(StreamWriter writer)
        {
            String soXML;
            Boolean skipNewline = true;

            writer.WriteLine("  <specialobjects>");

            foreach (KeyValuePair<String, List<SpecialObjectInfo>> kvp in specialobjects)
            {
                if (skipNewline == true)
                    skipNewline = false;
                else
                    writer.WriteLine();

                // I don't like hacking in the comments but neither do I like leaving them out.
                if (kvp.Key == "Wire")
                    writer.WriteLine("    <!-- Wire does not have an official color so it's copied over -->");
                else if (kvp.Key == "Background")
                    writer.WriteLine("    <!-- Backgrounds have to be last so they do not turn off the Lava & Water when Draw Walls is off.-->");

                foreach (SpecialObjectInfo soi in kvp.Value)
                {
                    soXML = String.Format("    <object name=\"{0}\" type=\"{1}\" ", soi.name, soi.type);

                    if (!String.IsNullOrEmpty(soi.colorName))
                        soXML = soXML + string.Format("color=\"{0}\" ", soi.colorName);
                    else
                        soXML = soXML + String.Format("color=\"#{0:X2}{1:X2}{2:X2}\" ",
                            soi.color.R, soi.color.G, soi.color.B);

                    soXML = soXML + String.Format("officialColor=\"#{0:X2}{1:X2}{2:X2}\" ",
                        soi.officialColor.R, soi.officialColor.G, soi.officialColor.B);

                    soXML = soXML + "/>";

                    writer.WriteLine(soXML);

                }
            }

            writer.WriteLine("  </specialobjects>");
        }
#endregion
 
        public ItemInfo GetItem(String itemName)
		{
			if (!items.ContainsKey(itemName))
			{
				if (renames.ContainsKey(itemName))
				{
					String newName = renames[itemName].newName;

					if (!items.ContainsKey(newName))
						return null;

					return items[newName];
				}
				else
				{
					return null;
				}
			}

			return items[itemName];
		}

		public Dictionary<String, ItemInfo> Items
		{
			get
			{
				return items;
			}
		}

		public Dictionary<Int32, TileInfo> Tiles
		{
			get
			{
				return tiles;
			}
		}

		public Dictionary<Int32, WallInfo> Walls
		{
			get
			{
				return walls;
			}
		}

        public Dictionary<String, List<MarkerInfo>> MarkerSets
        {
            get
            {
                return markerSets;
            }
        }

		public Dictionary<Int32, MarkerInfo> Markers
		{
			get
			{
				return markers;
			}
		}

		public Dictionary<String, List<SpecialObjectInfo>> SpecialObjects
		{
			get
			{
				return specialobjects;
			}
		}

		public Dictionary<String, ColorInfo> Colors
		{
			get
			{
				return colors;
			}
		}

		public String MarkerImageToName(String findName)
		{
			foreach (KeyValuePair<Int32, MarkerInfo> kvp in markers)
			{
				if (kvp.Value.markerImage == findName)
					return kvp.Value.name;
			}

			return String.Empty;
		}

        public MarkerInfo GetMarkerByName(String markerName)
        {
            foreach (KeyValuePair<Int32, MarkerInfo> kvp in markers)
                if (kvp.Value.name == markerName)
                    return kvp.Value;

            return null;
        }

		public String GetItemName(Int32 netId)
		{
			if (itemNames.ContainsKey(netId))
				return itemNames[netId];

			return "Unknown Item";
		}

		public ItemEnum GetItemEnum(String itemName)
		{
			ItemInfo ii;

			if (items.ContainsKey(itemName))
			{
				ii = items[itemName];

				if (ii.foundIn == "Chest")
					return ItemEnum.InChest;

				return ItemEnum.Normal;
			}
			else
			{
				return ItemEnum.NotFound;
			}
		}

		public void AddCustomColor(String colorName, Color newColor)
		{
			if (colors.ContainsKey(colorName))
				return;

			ColorInfo ci = new ColorInfo();

			ci.name = colorName;
			ci.color = newColor;
			ci.isCustom = true;

			colors.Add(colorName, ci);
		}

		public void RemoveCustomColor(String colorName)
		{
			if (!colors.ContainsKey(colorName))
				return;

			if (colors[colorName].isCustom == true)
				colors.Remove(colorName);
		}

		public ColorInfo GetColor(String colorName)
		{
			if (colors.ContainsKey(colorName))
				return colors[colorName];

			return null;
		}

	}

	#region Helper enumerations
	public enum ItemEnum
	{
		NotFound = 0,
		InChest,
		Normal
	}
	#endregion
}
