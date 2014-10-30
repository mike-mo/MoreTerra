using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Drawing;
using MoreTerra.Structures;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra
{
    public sealed class SettingsManager
    {
        public class UserSettings
        {
            public string SettingsName;
            public string InputWorldDirectory;
            public string OutputPreviewDirectory;
            public bool IsChestFilterEnabled;
            public Boolean UseOfficialColors;
            public Boolean AreWiresDrawable;
            public bool AreWallsDrawable;
            public Boolean OpenImageAfterDraw;
            public Boolean ShowChestItems;
            public Boolean ShowNormalItems;
            public Boolean ShowChestTypes;
            public Boolean UseCustomMarkers;
            public Int32 ChestListSortType;
            public Int32 CropImageType;

            //            public SerializableDictionary<string, MarkerInfo> MarkerStates;
            public Dictionary<String, MarkerSettings> MarkerStates;

            // This contains the actual list we use to filter with.
            public List<String> ChestFilterItems;

            public UserSettings()
            {

            }

            public UserSettings(UserSettings copy, String newName)
            {
                SettingsName = newName;
                InputWorldDirectory = copy.InputWorldDirectory;
                OutputPreviewDirectory = copy.OutputPreviewDirectory;
                IsChestFilterEnabled = copy.IsChestFilterEnabled;
                UseOfficialColors = copy.UseOfficialColors;
                AreWallsDrawable = copy.AreWallsDrawable;
                OpenImageAfterDraw = copy.OpenImageAfterDraw;
                ShowChestTypes = copy.ShowChestTypes;
                UseCustomMarkers = copy.UseCustomMarkers;
                ChestListSortType = copy.ChestListSortType;
                ShowChestItems = copy.ShowChestItems;
                ShowNormalItems = copy.ShowNormalItems;

                MarkerStates = new Dictionary<String, MarkerSettings>();

                foreach (KeyValuePair<String, MarkerSettings> kvp in copy.MarkerStates)
                    MarkerStates.Add(kvp.Key, kvp.Value);

                ChestFilterItems = new List<String>();

                foreach (String s in copy.ChestFilterItems)
                    ChestFilterItems.Add(s);
            }
        }

        public class ColorSettings
        {

        }

        private static SettingsManager instance = null;
        private static readonly object mutex = new object();
        private Dictionary<string, UserSettings> settingsList;
        private UserSettings settings;

        private List<ColorSettings> colorSettingsList;
        private ColorSettings colorSettings;

        // These items get saved into the settings file.
        // These are our Global Settings.
        private const Int32 UserSettingsVersion = 1;
        private Int32 curSettings;
        private Int32 HighestVersion;
        private String curSettingsName;

        #region Constructors
        private SettingsManager()
        {
            UserSettings us = new UserSettings();

            this.settingsList = new Dictionary<string, UserSettings>();//List<UserSettings>();
            this.settingsList.Add("Default", us);//.Add(us);
            curSettings = 0;

            this.HighestVersion = Global.CurrentVersion;

            SetDefaults(us);

            this.settings = us;
        }
        #endregion

        #region Initialization
        private void SetDefaults(UserSettings us)
        {
            // Initialize Marker States
            us.MarkerStates = new Dictionary<String, MarkerSettings>();

            foreach (KeyValuePair<Int32, MarkerInfo> kvp in Global.Instance.Info.Markers)
            {
                us.MarkerStates.Add(kvp.Value.name, new MarkerSettings());
            }

            // Initialize Item Filter
            us.ChestFilterItems = new List<String>();
            us.ChestFilterItems.Sort();

            us.SettingsName = "Default";
            us.IsChestFilterEnabled = false;
            us.UseOfficialColors = true;
            us.AreWallsDrawable = true;
            us.OpenImageAfterDraw = true;
            us.ShowChestTypes = false;
            us.UseCustomMarkers = false;
            us.ShowChestItems = true;
            us.ShowNormalItems = false;
            us.AreWiresDrawable = true;

            us.ChestListSortType = 0;
            us.CropImageType = 0;

            us.InputWorldDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Terraria\\Worlds");
            if (!Directory.Exists(us.InputWorldDirectory))
            {
                us.InputWorldDirectory = string.Empty;
            }

            us.OutputPreviewDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (!Directory.Exists(us.OutputPreviewDirectory))
            {
                us.OutputPreviewDirectory = string.Empty;
            }
        }

        public static SettingsManager Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new SettingsManager();
                    }
                    return instance;
                }
            }
        }

        public void Initialize()
        {
            // Initialization
            if (System.IO.File.Exists(Global.ApplicationUserSettingsFile))
                LoadSettings();
        }
        #endregion

        #region SettingsList Helper Functions
        public String SettingsName(Int32 name)
        {
            if ((name < 0) || (name >= settingsList.Count))
                return String.Empty;

            return settingsList.ElementAt(name).Value.SettingsName;
        }

        public Int32 SettingsCount
        {
            get
            {
                return settingsList.Count;
            }
        }

        public Int32 CurrentSettings
        {
            get
            {
                return curSettings;
            }
            set
            {
                if ((value < 0) || (value >= settingsList.Count))
                    return;

                curSettings = value;
                settings = settingsList.ElementAt(value).Value;//settingsList[value]
                curSettingsName = settings.SettingsName;
            }
        }

        public string CurrentSettingsName
        {
            get
            {
                return curSettingsName;
            }
            set
            {
                if (settingsList.ContainsKey(value))
                {
                    curSettings = settingsList.Keys.ToList().FindIndex(delegate(string Key)
                    {
                        return Key == value;
                    });
                    curSettingsName = value;
                    this.settings = settingsList[value];
                }
            }
        }

        // This does no range check as it is done in the Delete button event handler.
        public void DeleteSettings(Int32 toDelete)
        {
            KeyValuePair<string, UserSettings> setting = settingsList.ElementAt(toDelete);
            settingsList.Remove(setting.Key);//RemoveAt(toDelete);
            if (toDelete == curSettings)
            {
                curSettings = -1;
                settings = null;
            }
        }

        public Boolean AddNewSettings(String newName)
        {
            UserSettings us;
            Int32 i;

            if (newName == String.Empty)
                return false;

            if (settingsList.ContainsKey(newName))
            {
                return false;
            }

            us = new UserSettings(settings, newName);

            settingsList.Add(newName, us);

            return true;
        }
        #endregion

        #region Marker Helper Functions
        public bool DrawMarker(Int16 type)
        {
            if (type >= TileProperties.Unknown)
                return false;

            String index = Global.Instance.Info.Tiles[type].markerName;

            // convert to string index
            if (this.settings.MarkerStates.ContainsKey(index))
                return this.settings.MarkerStates[index].Drawing;

            return false;
        }

        public bool DrawMarker(ChestType type)
        {
            if (type == ChestType.Unknown)
                return false;

            String chestType = Global.Instance.Info.MarkerImageToName(Enum.GetName(typeof(ChestType), type));

            if (this.settings.MarkerStates.ContainsKey(chestType))
                return this.settings.MarkerStates[chestType].Drawing;

            return false;
        }

        public bool DrawMarker(MarkerType type)
        {
            if (type == MarkerType.Unknown)
                return false;

            String markerType = Global.Instance.Info.MarkerImageToName(Enum.GetName(typeof(MarkerType), type));

            if (this.settings.MarkerStates.ContainsKey(markerType))
                return this.settings.MarkerStates[markerType].Drawing;

            return false;
        }

        public bool DrawMarker(NPCType type)
        {
            String npcType = Global.Instance.Info.MarkerImageToName(Enum.GetName(typeof(NPCType), type));

            if (this.settings.MarkerStates.ContainsKey(npcType))
                return this.settings.MarkerStates[npcType].Drawing;

            return false;
        }

        public bool DrawMarker(string marker)
        {
            if (this.settings.MarkerStates.ContainsKey(marker))
                return this.settings.MarkerStates[marker].Drawing;

            return false;
        }

        public void MarkerVisible(string key, bool status)
        {
            this.settings.MarkerStates[key].Drawing = status;
        }
        #endregion

        #region Filter Helper Functions
        public void FilterItem(string itemName, bool status)
        {
            if (status == true)
                this.settings.ChestFilterItems.Add(itemName);
            else
                this.settings.ChestFilterItems.Remove(itemName);
        }
        #endregion

        #region GetSet Functions
        public string InputWorldDirectory
        {
            get
            {
                return this.settings.InputWorldDirectory;
            }
            set
            {
                this.settings.InputWorldDirectory = value;
            }
        }

        public string OutputPreviewDirectory
        {
            get
            {
                return this.settings.OutputPreviewDirectory;
            }
            set
            {
                this.settings.OutputPreviewDirectory = value;
            }
        }

        public Dictionary<string, MarkerSettings> MarkerStates
        {
            get
            {
                return this.settings.MarkerStates;
            }
        }

        public bool FilterChests
        {
            get
            {
                return this.settings.IsChestFilterEnabled;
            }
            set
            {
                this.settings.IsChestFilterEnabled = value;
            }
        }

        public Boolean OfficialColors
        {
            get
            {
                return this.settings.UseOfficialColors;
            }
            set
            {
                this.settings.UseOfficialColors = value;
            }
        }

        public Boolean DrawWires
        {
            get
            {
                return this.settings.AreWiresDrawable;
            }
            set
            {
                this.settings.AreWiresDrawable = value;
            }
        }

        public bool DrawWalls
        {
            get
            {
                return this.settings.AreWallsDrawable;
            }
            set
            {
                this.settings.AreWallsDrawable = value;
            }
        }

        public Boolean OpenImage
        {
            get
            {
                return this.settings.OpenImageAfterDraw;
            }
            set
            {
                this.settings.OpenImageAfterDraw = value;
            }
        }

        public Int32 SortChestsBy
        {
            get
            {
                return this.settings.ChestListSortType;
            }
            set
            {
                this.settings.ChestListSortType = value;
            }
        }

        public Int32 CropImageUsing
        {
            get
            {
                return this.settings.CropImageType;
            }
            set
            {
                this.settings.CropImageType = value;
            }
        }

        // This is the highest version we have opened and said "Don't show again" to.
        public Int32 TopVersion
        {
            get
            {
                return this.HighestVersion;
            }
            set
            {
                this.HighestVersion = value;
            }
        }

        public List<String> FilterItemStates
        {
            get
            {
                return this.settings.ChestFilterItems;
            }
        }

        public Boolean ShowChestTypes
        {
            get
            {
                return this.settings.ShowChestTypes;
            }
            set
            {
                this.settings.ShowChestTypes = value;
            }
        }

        public Boolean CustomMarkers
        {
            get
            {
                return settings.UseCustomMarkers;
            }
            set
            {
                settings.UseCustomMarkers = value;
            }
        }

        public Boolean ShowChestItems
        {
            get
            {
                return settings.ShowChestItems;
            }
            set
            {
                settings.ShowChestItems = value;
            }
        }

        public Boolean ShowNormalItems
        {
            get
            {
                return settings.ShowNormalItems;
            }
            set
            {
                settings.ShowNormalItems = value;
            }
        }
        #endregion

        #region Load/Save Functions
        public void LoadSettings()
        {
            curSettingsName = "Default";
            Int32 settingsNodeCount;
            UserSettings us;
            Int32 settingsVer;
            XmlNode baseNode;
            XmlNodeList settingsNodes;
            XmlNode parseNode;
            XmlNodeList parseNodeList;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Global.ApplicationUserSettingsFile);

            baseNode = xmlDoc.DocumentElement;

            // There's no UserSettings element so we'll use the default settings.
            if (baseNode.Name != "UserSettings")
                return;

            if (baseNode.Attributes["version"] == null)
                settingsVer = 0;
            else
                settingsVer = Int32.Parse(baseNode.Attributes["version"].Value);

            switch (settingsVer)
            {
                case 0:
                    break;
                case 1:
                    parseNode = baseNode.SelectSingleNode("GlobalSettings");

                    if (parseNode == null)
                        break;

                    parseNodeList = parseNode.SelectNodes("item");

                    foreach (XmlNode node in parseNodeList)
                    {
                        parseNode = node.Attributes["name"];

                        if (parseNode == null)
                            continue;

                        switch (parseNode.Value)
                        {
                            case "UseSettings":
                                parseNode = node.Attributes["value"];

                                if (parseNode != null)
                                    curSettingsName = parseNode.Value;
                                break;
                            case "HighestVersion":
                                parseNode = node.Attributes["value"];

                                if (parseNode != null)
                                    this.HighestVersion = Int32.Parse(parseNode.Value);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (settingsVer > 0)
                settingsNodes = baseNode.SelectNodes("Settings");
            else
                settingsNodes = xmlDoc.SelectNodes("UserSettings");

            settingsNodeCount = 0;
            foreach (XmlNode settingsNode in settingsNodes)
            {
                String settingsName;

                parseNode = settingsNode.Attributes["name"];

                if (parseNode == null)
                    settingsName = "Default";
                else
                    settingsName = parseNode.Value;

                us = null;
                if (settingsList.ContainsKey(settingsName))
                {
                    us = settingsList[settingsName];
                }
                //foreach (UserSettings testUs in this.settingsList)
                //{
                //    if (testUs.SettingsName == settingsName)
                //        us = testUs;
                //}

                if (us == null)
                {
                    us = new UserSettings();
                    SetDefaults(us);
                    us.SettingsName = settingsName;

                    this.settingsList.Add(settingsName, us);
                }

                if (us.SettingsName == curSettingsName)
                {
                    this.settings = us;
                    curSettings = settingsNodeCount;
                }
                settingsNodeCount++;

                switch (settingsVer)
                {
                    case 0:
                        #region UserSettings version 0 Loader
                        foreach (XmlNode node in settingsNode)
                        {
                            switch (node.Name)
                            {
                                case "InputWorldDirectory":
                                    us.InputWorldDirectory = node.InnerXml;
                                    break;
                                case "OutputPreviewDirectory":
                                    us.OutputPreviewDirectory = node.InnerXml;
                                    break;
                                case "IsChestFilterEnabled":
                                    us.IsChestFilterEnabled = Boolean.Parse(node.InnerXml);
                                    break;
                                case "IsWallsDrawable":
                                    us.AreWallsDrawable = Boolean.Parse(node.InnerXml);
                                    break;
                                case "OpenImageAfterDraw":
                                    us.OpenImageAfterDraw = Boolean.Parse(node.InnerXml);
                                    break;
                                case "ShowChestTypes":
                                    us.ShowChestTypes = Boolean.Parse(node.InnerXml);
                                    break;
                                case "UseCustomMarkers":
                                    us.UseCustomMarkers = Boolean.Parse(node.InnerXml);
                                    break;
                                case "ChestListSortType":
                                    us.ChestListSortType = Int32.Parse(node.InnerXml);
                                    break;
                                case "HighestVersion":
                                    this.HighestVersion = Int32.Parse(node.InnerXml);
                                    break;
                                case "SymbolStates":
                                    parseNodeList = node.SelectNodes("item");

                                    foreach (XmlNode n in parseNodeList)
                                    {
                                        String Key;
                                        Boolean Value;
                                        MarkerSettings mi;

                                        parseNode = n.SelectSingleNode("key").SelectSingleNode("string");
                                        Key = parseNode.InnerXml;

                                        parseNode = n.SelectSingleNode("value").SelectSingleNode("boolean");
                                        Value = Boolean.Parse(parseNode.InnerXml);

                                        if (us.MarkerStates.ContainsKey(Key))
                                            us.MarkerStates[Key].Drawing = Value;
                                        else
                                        {
                                            String newKey = Global.Instance.Info.MarkerImageToName(Key);

                                            if (!us.MarkerStates.ContainsKey(newKey))
                                                newKey = String.Empty;

                                            if (newKey == String.Empty)
                                            {
                                                mi = new MarkerSettings();
                                                mi.Drawing = Value;
                                                us.MarkerStates.Add(Key, mi);
                                            }
                                            else
                                            {
                                                us.MarkerStates[newKey].Drawing = Value;
                                            }
                                        }
                                    }
                                    break;
                                case "ChestFilterItems":
                                    parseNodeList = node.SelectNodes("item");

                                    foreach (XmlNode n in parseNodeList)
                                    {
                                        String Key;
                                        Boolean Value;

                                        parseNode = n.SelectSingleNode("key").SelectSingleNode("string");
                                        Key = parseNode.InnerXml;

                                        parseNode = n.SelectSingleNode("value").SelectSingleNode("boolean");
                                        Value = Boolean.Parse(parseNode.InnerXml);

                                        if (Value == true)
                                            us.ChestFilterItems.Add(Key);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        break;
                    case 1:
                        #region UserSettings version 1 Loader
                        foreach (XmlNode node in settingsNode)
                        {
                            parseNode = node.Attributes["name"];

                            if (parseNode == null)
                                continue;

                            switch (parseNode.Value)
                            {
                                case "InputWorldDirectory":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.InputWorldDirectory = parseNode.Value;
                                    break;
                                case "OutputPreviewDirectory":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.OutputPreviewDirectory = parseNode.Value;
                                    break;
                                case "IsChestFilterEnabled":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.IsChestFilterEnabled = Boolean.Parse(parseNode.Value);
                                    break;
                                case "UseOfficialColors":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.UseOfficialColors = Boolean.Parse(parseNode.Value);
                                    break;
                                case "AreWiresDrawable":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.AreWiresDrawable = Boolean.Parse(parseNode.Value);
                                    break;
                                case "AreWallsDrawable":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.AreWallsDrawable = Boolean.Parse(parseNode.Value);
                                    break;
                                case "OpenImageAfterDraw":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.OpenImageAfterDraw = Boolean.Parse(parseNode.Value);
                                    break;
                                case "ShowChestTypes":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.ShowChestTypes = Boolean.Parse(parseNode.Value);
                                    break;
                                case "UseCustomMarkers":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.UseCustomMarkers = Boolean.Parse(parseNode.Value);
                                    break;
                                case "ChestListSortType":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.ChestListSortType = Int32.Parse(parseNode.Value);
                                    break;
                                case "CropImageType":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.CropImageType = Int32.Parse(parseNode.Value);
                                    break;
                                case "ShowChestItems":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.ShowChestItems = Boolean.Parse(parseNode.Value);
                                    break;
                                case "ShowNormalItems":
                                    parseNode = node.Attributes["value"];

                                    if (parseNode != null)
                                        us.ShowNormalItems = Boolean.Parse(parseNode.Value);
                                    break;
                                case "MarkerStates":
                                    parseNodeList = node.SelectNodes("listitem");

                                    foreach (XmlNode n in parseNodeList)
                                    {
                                        String Name;
                                        MarkerSettings mi;

                                        parseNode = n.Attributes["name"];

                                        if (parseNode == null)
                                            break;

                                        Name = parseNode.Value;

                                        if (us.MarkerStates.TryGetValue(Name, out mi) == false)
                                            mi = new MarkerSettings();

                                        parseNode = n.Attributes["draw"];

                                        if (parseNode != null)
                                            mi.Drawing = Boolean.Parse(parseNode.Value);

                                        parseNode = n.Attributes["filter"];

                                        if (parseNode != null)
                                            mi.Filtering = Boolean.Parse(parseNode.Value);

                                        parseNode = n.Attributes["min"];

                                        if (parseNode != null)
                                            mi.Min = Int32.Parse(parseNode.Value);

                                        parseNode = n.Attributes["max"];

                                        if (parseNode != null)
                                            mi.Max = Int32.Parse(parseNode.Value);

                                        if (!us.MarkerStates.ContainsKey(Name))
                                            us.MarkerStates.Add(Name, mi);
                                    }
                                    break;
                                case "ChestFilterItems":
                                    parseNode = node.Attributes["filter"];

                                    if (parseNode == null)
                                        continue;

                                    String[] splitList = parseNode.Value.Split(';');

                                    foreach (String s in splitList)
                                    {
                                        us.ChestFilterItems.Add(s);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        break;
                    default:
                        return;
                }
            }

            parseNode = baseNode.SelectSingleNode("CustomColors");

            if (parseNode != null)
            {
                parseNode = parseNode.Attributes["list"];

                if (parseNode != null)
                {
                    String[] colorList = parseNode.Value.Split(';');
                    Color newColor;

                    for (Int32 sPos = 0; sPos < colorList.Length; sPos += 2)
                    {
                        if (Global.TryParseColor(colorList[sPos + 1], out newColor) == false)
                            continue;

                        if (Global.Instance.Info.Colors.ContainsKey(colorList[sPos]))
                            continue;


                        Global.Instance.Info.AddCustomColor(colorList[sPos], newColor);
                    }
                }
            }
        }

        public void SaveSettings()
        {
            StringBuilder sb = new StringBuilder();
            String splitter;
            FileStream stream = new FileStream(Global.ApplicationUserSettingsFile, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            writer.WriteLine("<UserSettings version=\"{0}\">", SettingsManager.UserSettingsVersion);

            writer.WriteLine("  <GlobalSettings>");
            writer.WriteLine("    <item name=\"UseSettings\" value=\"{0}\" />",
                this.settings.SettingsName);
            writer.WriteLine("    <item name=\"HighestVersion\" value=\"{0}\" />", this.HighestVersion);
            writer.WriteLine("  </GlobalSettings>");

            foreach (UserSettings us in this.settingsList.Values)
            {

                writer.WriteLine("  <Settings name=\"{0}\">",
                    us.SettingsName);
                writer.WriteLine("    <item name=\"InputWorldDirectory\" value=\"{0}\" />",
                    us.InputWorldDirectory);
                writer.WriteLine("    <item name=\"OutputPreviewDirectory\" value=\"{0}\" />",
                    us.OutputPreviewDirectory);
                writer.WriteLine("    <item name=\"IsChestFilterEnabled\" value=\"{0}\" />",
                    us.IsChestFilterEnabled);
                writer.WriteLine("    <item name=\"UseOfficialColors\" value=\"{0}\" />",
                    us.UseOfficialColors);
                writer.WriteLine("    <item name=\"ShowChestItems\" value=\"{0}\" />",
                    us.ShowChestItems);
                writer.WriteLine("    <item name=\"ShowNormalItems\" value=\"{0}\" />",
                    us.ShowNormalItems);
                writer.WriteLine("    <item name=\"AreWiresDrawable\" value=\"{0}\" />",
                    us.AreWiresDrawable);
                writer.WriteLine("    <item name=\"AreWallsDrawable\" value=\"{0}\" />",
                    us.AreWallsDrawable);
                writer.WriteLine("    <item name=\"OpenImageAfterDraw\" value=\"{0}\" />",
                    us.OpenImageAfterDraw);
                writer.WriteLine("    <item name=\"ShowChestTypes\" value=\"{0}\" />",
                    us.ShowChestTypes);
                writer.WriteLine("    <item name=\"CropImageType\" value=\"{0}\" />",
                    us.CropImageType);
                writer.WriteLine("    <item name=\"UseCustomMarkers\" value=\"{0}\" />",
                    us.UseCustomMarkers);
                writer.WriteLine("    <item name=\"ChestListSortType\" value=\"{0}\" />",
                    us.ChestListSortType);

                writer.WriteLine("    <item name=\"MarkerStates\">");
                foreach (KeyValuePair<String, MarkerSettings> kvp in us.MarkerStates)
                {
                    writer.WriteLine("      <listitem name=\"{0}\" draw=\"{1}\" filter=\"{2}\" min=\"{3}\" max=\"{4}\" />",
                        kvp.Key, kvp.Value.Drawing, kvp.Value.Filtering, kvp.Value.Min, kvp.Value.Max);
                }
                writer.WriteLine("    </item>");

                sb.Clear();
                splitter = "";
                foreach (String s in us.ChestFilterItems)
                {
                    sb.Append(splitter + s);
                    splitter = ";";
                }

                if (sb.Length != 0)
                    writer.WriteLine("    <item name=\"ChestFilterItems\" filter=\"{0}\" />", sb.ToString());

                writer.WriteLine("  </Settings>");
            }

            sb.Clear();
            splitter = "";
            foreach (KeyValuePair<String, ColorInfo> kvp in Global.Instance.Info.Colors)
            {
                if (kvp.Value.isCustom == true)
                {
                    sb.AppendFormat("{0}{1};{2}", splitter, kvp.Key, Global.ToColorString(kvp.Value.color));
                    splitter = ";";
                }
            }

            if (sb.Length != 0)
                writer.WriteLine("  <CustomColors list=\"{0}\" />", sb.ToString());

            writer.WriteLine("</UserSettings>");
            writer.Close();

        }
        #endregion

        public void Shutdown()
        {
            SaveSettings();
        }


    }
}
