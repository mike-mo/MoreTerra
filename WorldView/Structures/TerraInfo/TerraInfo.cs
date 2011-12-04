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
		private Dictionary<String, MarkerInfo> markers;
		private Dictionary<Int32, TileInfo> tiles;
		private Dictionary<Int32, WallInfo> walls;
		private Dictionary<String, ItemInfo> items;
		private Dictionary<String, ItemTypeInfo> itemTypes;
		private Dictionary<String, RecolorInfo> recolors;
		private Dictionary<String, RenameInfo> renames;
		private Dictionary<String, ColorInfo> colors;
		private Dictionary<String, NpcInfo> npcs;
		private Dictionary<String, CraftingSpotInfo> craftingspots;
		private Dictionary<Int32, RecipeInfo> recipes;
		private Dictionary<String, List<SpecialObjectInfo>> specialobjects;

		private StringBuilder errorLog;

		public TerraInfo()
		{
			markers = new Dictionary<String, MarkerInfo>();
			items = new Dictionary<String, ItemInfo>();
			itemTypes = new Dictionary<String, ItemTypeInfo>();
			recolors = new Dictionary<String, RecolorInfo>();
			renames = new Dictionary<String, RenameInfo>();
			colors = new Dictionary<String, ColorInfo>();
			npcs = new Dictionary<String, NpcInfo>();
			craftingspots = new Dictionary<String, CraftingSpotInfo>();

			recipes = new Dictionary<Int32, RecipeInfo>();
			tiles = new Dictionary<Int32, TileInfo>();
			walls = new Dictionary<Int32, WallInfo>();

			specialobjects = new Dictionary<String, List<SpecialObjectInfo>>();
			 
			errorLog = new StringBuilder();
		}

		public String LoadInfo(String itemXmlFile)
		{
			XmlDocument xmlDoc = new XmlDocument();
			
#if DEBUG == false
			try
			{
#endif
				xmlDoc.LoadXml(itemXmlFile);

#if DEBUG == false
			}
			catch (FileNotFoundException e)
			{
				errorLog.AppendLine(e.Message);
				return errorLog.ToString();
			}
#endif
			
			XmlNode dataNode = xmlDoc.DocumentElement;

			LoadMarkers(dataNode.SelectSingleNode("markers").SelectNodes("marker"));

			LoadItemTypes(dataNode.SelectSingleNode("itemtypes").SelectNodes("itemtype"));
			LoadRecolors(dataNode.SelectSingleNode("recoloring").SelectNodes("recolor"));
			LoadRenames(dataNode.SelectSingleNode("namechanges").SelectNodes("item"));
			LoadNpcs(dataNode.SelectSingleNode("npcs").SelectNodes("npc"));
			LoadItems(dataNode.SelectSingleNode("items").SelectNodes("item"));

			LoadColors(dataNode.SelectSingleNode("colors").SelectNodes("color"));
			LoadTiles(dataNode.SelectSingleNode("tiles").SelectNodes("tile"));
			LoadWalls(dataNode.SelectSingleNode("walls").SelectNodes("wall"));

			LoadCrafting(dataNode.SelectSingleNode("crafting").SelectNodes("craftingspot"));
			LoadSpecialObjects(dataNode.SelectSingleNode("specialobjects").SelectNodes("object"));
			
			return errorLog.ToString();
		}

		private void LoadMarkers(XmlNodeList markerNodes)
		{
			Int32 count = recipes.Count - 1;

			if ((markerNodes == null) || (markerNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Marker items to load.");
				return;
			}

			foreach (XmlNode markerNode in markerNodes)
			{
				String name = String.Empty;
				String inSet = String.Empty;
				String markerImage = String.Empty;
				Boolean notInList = false;

				count++;

				foreach (XmlAttribute att in markerNode.Attributes)
				{

					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "partOfSet":
							inSet = att.Value;
							break;
						case "markerImage":
							markerImage = att.Value;
							break;
						case "notInList":
							if (Boolean.TryParse(att.Value, out notInList) == false)
							{
								errorLog.AppendLine(String.Format("Marker #{0} has an invalid notInList attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("Marker #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Marker #{0} had no name attribute.", count));
					continue;
				}

				if (inSet != String.Empty)
				{
					if (!markers.ContainsKey(inSet))
					{
						errorLog.AppendLine(String.Format("Marker #{0} is in a set that does not exist. Value=\"{1}\"",
							count, inSet));
						continue;
					}
				}

				if (markerImage == String.Empty)
					markerImage = name;

				if (markers.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Marker #{0} is a duplicate of {1}.",
						count, name));
					continue;
				}



				MarkerInfo marker = new MarkerInfo();
				marker.name = name;
				marker.markerSet = inSet;
				marker.markerImage = markerImage;
				marker.notInList = notInList;

				markers.Add(name, marker);
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
				String type = String.Empty;

				Int32 imageId = -1;
				count++;

				foreach (XmlAttribute att in npcNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "type":
							type = att.Value;
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

				if (type == String.Empty)
				{
					type = "enemy";
				}

				if ((type != "enemy") && (type != "friend"))
				{
					errorLog.AppendLine(String.Format("Npc #{0} had an invalid type attribute.  Value=\"{1}\"",
						count, type));
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
				npc.type = type;
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
				Color useColor;
				Boolean safe = false;

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
								errorLog.AppendLine(String.Format("Wall #{0} had an invalid unsafe attribute.  Value=\"{1\"",
									count, att.Value));
								continue;
							}
							safe = !safe;
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
				wall.safe = safe;

				walls.Add(wallImage, wall);
			}
		}

		private void LoadCrafting(XmlNodeList craftingSpotNodes)
		{
			Int32 count = -1;

			if ((craftingSpotNodes == null) || (craftingSpotNodes.Count == 0))
			{
				errorLog.AppendLine("There are no CraftingSpot items to load.");
				return;
			}

			foreach (XmlNode craftingSpotNode in craftingSpotNodes)
			{
				String name = String.Empty;
				String type = String.Empty;
				count++;

				foreach (XmlAttribute att in craftingSpotNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "type":
							type = att.Value;
							break;
						default:
							errorLog.AppendLine(String.Format("CraftingSpot #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("CraftingSpot #{0} had no name attribute.", count));
					continue;
				}

				if (craftingspots.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("CraftingSpot #{0} had a duplicate name to {1}.",
						count, name));
					continue;
				}

				CraftingSpotInfo craftingSpot = new CraftingSpotInfo();
				craftingSpot.name = name;
				craftingSpot.type = type;
				craftingspots.Add(name, craftingSpot);

				XmlNodeList recipeNodes = craftingSpotNode.SelectNodes("recipe");

				if (recipeNodes.Count > 0)
					LoadRecipes(recipeNodes, name);
			}
		}

		private void LoadRecipes(XmlNodeList recipeNodes, String useCraftingSpot = "")
		{
			Int32 count = recipes.Count - 1;

			if ((recipeNodes == null) || (recipeNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Recipe items to load.");
				return;
			}

			foreach (XmlNode recipeNode in recipeNodes)
			{
				String name = String.Empty;
				Int32 numMade = -1;

				count++;

				foreach (XmlAttribute att in recipeNode.Attributes)
				{

					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "numMade":
							if (Int32.TryParse(att.Value, out numMade) == false)
							{
								errorLog.AppendLine(String.Format("Recipe #{0} has an invalid itemImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if ((numMade < 0) || (numMade > 255))
							{
								errorLog.AppendLine(String.Format("Recipe #{0} had a out of range numMade attribute.  Value=\"{1}\"",
									count, numMade));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("Recipe #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Recipe #{0} had no name attribute.", count));
					continue;
				}

				if (!items.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Recipe #{0} is for an item that does not exist.  Value=\"{1}\"",
						count, name));
					continue;
				}

				RecipeInfo recipe = new RecipeInfo();
				recipe.name = name;
				recipe.craftingSpot = useCraftingSpot;

				XmlNodeList materialNodes = recipeNode.SelectNodes("material");
				Int32 materialCount = -1;

				foreach (XmlNode materialNode in materialNodes)
				{
					String materialName = String.Empty;
					Int32 materialNeeded = -1;
					materialCount++;

					foreach (XmlAttribute att in materialNode.Attributes)
					{
						switch (att.Name)
						{
							case "name":
								materialName = att.Value;
								break;
							case "quantity":
								if (Int32.TryParse(att.Value, out materialNeeded) == false)
								{
									errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has an invalid quantity attribute. Value=\"{2}\"",
										materialCount, count, att.Value));
									continue;
								}

								if ((materialNeeded < 0) || (materialNeeded > 255))
								{
									errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has an out of range quantity attribute. Value=\"{2}\"",
										materialCount, count, att.Value));
									continue;
								}
								break;
							default:
								errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has unknown attribute \"{2}\" has value \"{3}\"",
									materialCount, count, att.Name, att.Value));
								break;
						}
					}

					if (materialName == String.Empty)
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has no name attribute.",
							materialCount, count));
						continue;
					}

					if (!items.ContainsKey(materialName))
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} is for an item that does not exist. Value=\"{2}\"",
							materialCount, count, materialName));
						continue;
					}

					if (recipe.materials.ContainsKey(materialName))
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} is a duplicate material to \"{2}\"",
							materialCount, count, materialName));
						continue;
					}

					if (materialNeeded == -1)
						materialNeeded = 1;

					recipe.materials.Add(materialName, materialNeeded);

				}

				if (recipe.materials.Count == 0)
				{
					errorLog.AppendLine(String.Format("Recipe #{0} does not have any materials to craft it.",
						count));
					continue;
				}

				recipes.Add(count, recipe);
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
				String autoGen = String.Empty;
				String blendWith = String.Empty;
				String color = String.Empty;
				String marker = String.Empty;
				Color useColor;

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

							if ((tileImage < 0) || (tileImage > 255))
							{
								errorLog.AppendLine(String.Format("Tile #{0} had a out of range numMade attribute.  Value=\"{1}\"",
									count, tileImage));
								continue;
							}
							break;
						case "autoGenType":
							autoGen = att.Value;
							break;
						case "blendWith":
							blendWith = att.Value;
							break;
						case "color":
							color = att.Value;
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

				if ((autoGen != String.Empty) && (blendWith == String.Empty))
				{
					if ((autoGen == "Ore Blend"))
						blendWith = "Dirt";
				}

				if ((blendWith != String.Empty) && (autoGen == String.Empty))
				{
					errorLog.AppendLine(String.Format("Tile #{0} had a blend type but had no autoGenType attribute.",
						count));
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

				TileInfo tile = new TileInfo();
				tile.name = name;
				tile.autoGenType = autoGen;
				tile.blendWith = blendWith;
				tile.colorName = color;
				tile.color = useColor;
				tile.markerName = marker;
				tile.tileImage = tileImage;

/*				XmlNodeList materialNodes = recipeNode.SelectNodes("material");
				Int32 materialCount = -1;

				foreach (XmlNode materialNode in materialNodes)
				{
					String materialName = String.Empty;
					Int32 materialNeeded = -1;
					materialCount++;

					foreach (XmlAttribute att in materialNode.Attributes)
					{
						switch (att.Name)
						{
							case "name":
								materialName = att.Value;
								break;
							case "quantity":
								if (Int32.TryParse(att.Value, out materialNeeded) == false)
								{
									errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has an invalid quantity attribute. Value=\"{2}\"",
										materialCount, count, att.Value));
									continue;
								}

								if ((materialNeeded < 0) || (materialNeeded > 255))
								{
									errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has an out of range quantity attribute. Value=\"{2}\"",
										materialCount, count, att.Value));
									continue;
								}
								break;
							default:
								errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has unknown attribute \"{2}\" has value \"{3}\"",
									materialCount, count, att.Name, att.Value));
								break;
						}
					}

					if (materialName == String.Empty)
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} has no name attribute.",
							materialCount, count));
						continue;
					}

					if (!items.ContainsKey(materialName))
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} is for an item that does not exist. Value=\"{2}\"",
							materialCount, count, materialName));
						continue;
					}

					if (recipe.materials.ContainsKey(materialName))
					{
						errorLog.AppendLine(String.Format("Material #{0} in Recipe #{1} is a duplicate material to \"{2}\"",
							materialCount, count, materialName));
						continue;
					}

					if (materialNeeded == -1)
						materialNeeded = 1;

					recipe.materials.Add(materialName, materialNeeded);

				}

				if (recipe.materials.Count == 0)
				{
					errorLog.AppendLine(String.Format("Recipe #{0} does not have any materials to craft it.",
						count));
					continue;
				} */

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
				String type = String.Empty;
				String found = String.Empty;
				String recolor = String.Empty;
				String droppedBy = String.Empty;

				Int32 imageId = -1;
				Int32 stack = -1;
				Int32 defaultStack = -1;
				count++;

				foreach (XmlAttribute att in itemNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "type":
							type = att.Value;
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
						case "recolor":
							recolor = att.Value;
							break;
						case "maxStack":
							if (Int32.TryParse(att.Value, out stack) == false)
							{
								errorLog.AppendLine(String.Format("Item #{0} has an invalid maxStack attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((stack < 1) || (stack > 255))
							{
								errorLog.AppendLine(String.Format("Item #{0} had a out of range maxStack attribute.  Value=\"{1}\"",
									count, stack));
								continue;
							}
							break;
						case "droppedBy":
							droppedBy = att.Value;
							break;
						case "subType":
							// Not implemented yet.
							break;
						case "ammoType":
							// Not implemented yet.
							break;
						case "plantsIn":
							// Not implemented yet.
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

				if (type == String.Empty)
					type = "Generic";

				if (!itemTypes.ContainsKey(type))
				{
					errorLog.AppendLine(String.Format("Item #{0} has an itemType that does not exist.  Value=\"{1}\"",
						count, type));
					continue;
				}
				else
				{
					defaultStack = itemTypes[type].defaultSize;
				}

				if (stack == -1)
				{
					stack = defaultStack;
				}


				if (imageId == -1)
				{
					errorLog.AppendLine(String.Format("Item #{0} had no itemImage attribute.", count));
					continue;
				}


				if (items.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Item #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				ItemInfo item = new ItemInfo();
				item.name = name;
				item.type = type;
				item.droppedBy = droppedBy;
				item.foundIn = found;
				item.recolor = recolor;
				item.imageId = imageId;
				item.stackSize = stack;

				items.Add(name, item);
			}
		}

		private void LoadItemTypes(XmlNodeList itemTypeNodes)
		{
			Int32 count = -1;

			if ((itemTypeNodes == null) || (itemTypeNodes.Count == 0))
			{
				errorLog.AppendLine("There are no ItemType items to load.");
				return;
			}

			foreach (XmlNode itemTypeNode in itemTypeNodes)
			{
				String name = String.Empty;
				Int32 stack = -1;
				count++;

				foreach (XmlAttribute att in itemTypeNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "defaultStack":
							if (Int32.TryParse(att.Value, out stack) == false)
							{
								errorLog.AppendLine(String.Format("ItemType #{0} has an invalid defaultStack attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((stack < 1) || (stack > 255))
							{
								errorLog.AppendLine(String.Format("ItemType #{0} had a out of range defaultStack attribute.  Value=\"{1}\"",
									count, stack));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("ItemType #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}

				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("ItemType #{0} had no name attribute.", count));
					continue;
				}

				if (stack == -1)
				{
					errorLog.AppendLine(String.Format("ItemType #{0} had no defaultStack attribute.", count));
					continue;
				}

				if (itemTypes.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("ItemType #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				ItemTypeInfo type = new ItemTypeInfo();
				type.name = name;
				type.defaultSize = stack;

				itemTypes.Add(name, type);
			}
		}

		private void LoadRecolors(XmlNodeList recolorNodes)
		{
			Int32 count = -1;

			if ((recolorNodes == null) || (recolorNodes.Count == 0))
			{
				errorLog.AppendLine("There are no Recolor items to load.");
				return;
			}

			foreach (XmlNode recolorNode in recolorNodes)
			{
				String name = String.Empty;
				Single tintR = -1;
				Single tintG = -1;
				Single tintB = -1;
				count++;

				foreach (XmlAttribute att in recolorNode.Attributes)
				{
					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "tintRed":
							if (Single.TryParse(att.Value, out tintR) == false)
							{
								errorLog.AppendLine(String.Format("Recolor #{0} has an invalid tintRed attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((tintR < 0) || (tintR > 2))
							{
								errorLog.AppendLine(String.Format("Recolor #{0} had a out of range tintRed attribute.  Value=\"{1}\"",
									count, tintR));
								continue;
							}
							break;
						case "tintGreen":
							if (Single.TryParse(att.Value, out tintG) == false)
							{
								errorLog.AppendLine(String.Format("Recolor #{0} has an invalid tintGreen attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((tintG < 0) || (tintG > 2))
							{
								errorLog.AppendLine(String.Format("Recolor #{0} had a out of range tintGreen attribute.  Value=\"{1}\"",
									count, tintG));
								continue;
							}
							break;
						case "tintBlue":
							if (Single.TryParse(att.Value, out tintB) == false)
							{
								errorLog.AppendLine(String.Format("Recolor #{0} has an invalid tintBlue attribute.  Value=\"{1}\"",
									count, att.Value));
								continue;
							}

							if ((tintB < 0) || (tintB > 2))
							{
								errorLog.AppendLine(String.Format("Recolor #{0} had a out of range tintBlue attribute.  Value=\"{1}\"",
									count, tintB));
								continue;
							}
							break;
						default:
							errorLog.AppendLine(String.Format("Recolor #{0} has unknown attribute \"{1}\" has value \"{2}\"",
								count, att.Name, att.Value));
							break;
					}
				}
				if (name == String.Empty)
				{
					errorLog.AppendLine(String.Format("Recolor #{0} had no name attribute.", count));
					continue;
				}

				if (tintR == -1)
				{
					errorLog.AppendLine(String.Format("Recolor #{0} had no tintRed attribute.", count));
					continue;
				}

				if (tintG == -1)
				{
					errorLog.AppendLine(String.Format("Recolor #{0} had no tintGreen attribute.", count));
					continue;
				}

				if (tintB == -1)
				{
					errorLog.AppendLine(String.Format("Recolor #{0} had no tintBlue attribute.", count));
					continue;
				}

				if (recolors.ContainsKey(name))
				{
					errorLog.AppendLine(String.Format("Recolor #{0} had a duplicate name to {1}.", count, name));
					continue;
				}

				RecolorInfo recolor = new RecolorInfo();
				recolor.name = name;
				recolor.tintR = tintR;
				recolor.tintG = tintG;
				recolor.tintB = tintB;

				recolors.Add(name, recolor);

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
				errorLog.AppendLine("There are no Tile items to load.");
				return;
			}

			foreach (XmlNode objectNode in soNodes)
			{
				String name = String.Empty;
				String type = String.Empty;
				String color = String.Empty;
				Color useColor;

				Int32 objectImage = -1;

				count++;

				foreach (XmlAttribute att in objectNode.Attributes)
				{

					switch (att.Name)
					{
						case "name":
							name = att.Value;
							break;
						case "objectImage":
							if (Int32.TryParse(att.Value, out objectImage) == false)
							{
								errorLog.AppendLine(String.Format("Special Object #{0} has an invalid objectImage attribute. Value = \"{1}\"",
									count, att.Value));
								continue;
							}

							if ((objectImage < 0) || (objectImage > 255))
							{
								errorLog.AppendLine(String.Format("Special Object #{0} had a out of range objectImage attribute.  Value=\"{1}\"",
									count, objectImage));
								continue;
							}
							break;
						case "type":
							type = att.Value;
							break;
						case "color":
							color = att.Value;
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

				SpecialObjectInfo so = new SpecialObjectInfo();
				so.name = name;
				so.type = type;
				so.colorName = color;
				so.color = useColor;
				so.objectImage = objectImage;

				if (!specialobjects.ContainsKey(type))
					specialobjects.Add(type, new List<SpecialObjectInfo>());

				specialobjects[type].Add(so);
			}
		}


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

		public Dictionary<String, MarkerInfo> Markers
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
			foreach (KeyValuePair<String, MarkerInfo> kvp in markers)
			{
				if (kvp.Value.markerImage == findName)
					return kvp.Key;
			}

			return String.Empty;
		}

		public ItemEnum GetItemEnum(String itemName)
		{
			ItemInfo ii;

			if (items.ContainsKey(itemName))
			{
				ii = items[itemName];

				if (ii.isCustom == true)
					return ItemEnum.Custom;

				if (ii.foundIn == "Chest")
					return ItemEnum.InChest;

				return ItemEnum.Normal;
			}
			else
			{
				return ItemEnum.NotFound;
			}
		}

		public void AddCustomItem(String itemName)
		{
			ItemInfo ii = new ItemInfo();

			ii.name = itemName;
			ii.isCustom = true;

			items.Add(itemName, ii);
		}

		public void RemoveCustomItem(String itemName)
		{
			if (!items.ContainsKey(itemName))
				return;

			if (items[itemName].isCustom == false)
				return;

			items.Remove(itemName);
		}

		public RecolorInfo GetRecolor(String recolorName)
		{
			if (recolors.ContainsKey(recolorName))
				return recolors[recolorName];

			return null;
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
		Normal,
		Custom
	}
	#endregion
}
