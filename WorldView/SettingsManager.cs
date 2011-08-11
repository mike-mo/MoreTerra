using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using MoreTerra.Structures;

namespace MoreTerra
{
    public sealed class SettingsManager
    {
        [Serializable]
        public class UserSettings
        {
            public string InputWorldDirectory;
            public string OutputPreviewDirectory;
            public bool IsChestFilterEnabled;
            public bool IsWallsDrawable;
			public Boolean OpenImageAfterDraw;
			public Boolean ScanForNewChestItems;
			public Int32 ChestListSortType;
			public Int32 HighestVersion;
            public SerializableDictionary<string, bool> SymbolStates;

			// This contains the actual list we use to filter with.
			public SerializableDictionary<string, bool> ChestFilterItems;
		}

        private static SettingsManager instance = null;
        private static readonly object mutex = new object();
        private UserSettings settings;

		// This contains only the items that we have encoded in the program itself.
		private List<String> DefaultFilterItems;

		// Added to allow certain things to never happen when the console is on.
		// Mainly pop-up forms and Dialogs.
		private Boolean runningConsole = false;

        private SettingsManager()
        {
            this.settings = new UserSettings();

			InitializeSymbolStates();

			this.DefaultFilterItems = new List<String>();

			foreach (string s in Constants.defaultItems)
			{
				this.DefaultFilterItems.Add(s);
			}

			InitializeItemFilter();

			this.settings.IsChestFilterEnabled = false;
			this.settings.IsWallsDrawable = true;
			this.settings.ChestListSortType = 0;
            this.settings.InputWorldDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Terraria\\Worlds");
            if (!Directory.Exists(this.settings.InputWorldDirectory))
            {
                this.settings.InputWorldDirectory = string.Empty;
            }
        }

		private void InitializeSymbolStates()
		{
			if (this.settings.SymbolStates == null)
				this.settings.SymbolStates = new SerializableDictionary<string, bool>();

			foreach (string s in Constants.ExternalSymbolNames)
			{
				if (!this.settings.SymbolStates.ContainsKey(s))
					this.settings.SymbolStates.Add(s, true);
			}
		}

		private void InitializeItemFilter()
		{
			SerializableDictionary<String, Boolean> sortedItems;

			if (this.settings.ChestFilterItems == null)
				this.settings.ChestFilterItems = new SerializableDictionary<string, bool>();

			foreach (String s in Constants.defaultItems)
			{
				if (!this.settings.ChestFilterItems.ContainsKey(s))
					this.settings.ChestFilterItems.Add(s, false);
			}

			// Now that we've done that let's sort them alphabetically.
			IEnumerable<KeyValuePair<String, Boolean>> e = this.settings.ChestFilterItems.OrderBy<KeyValuePair<String, Boolean>, String>(kvp => kvp.Key);
			sortedItems = new SerializableDictionary<String, Boolean>();

			foreach (KeyValuePair<String, Boolean> kvp in e)
			{
				sortedItems.Add(kvp.Key, kvp.Value);
			}

			this.settings.ChestFilterItems = sortedItems;
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
            if (!System.IO.File.Exists(Constants.ApplicationUserSettingsFile)) return;


            // Load User Preference File
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(Constants.ApplicationUserSettingsFile));
            XmlNodeReader reader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer inputSerializer = new XmlSerializer(this.settings.GetType());
            this.settings = (UserSettings)inputSerializer.Deserialize(reader);

			InitializeSymbolStates();
			InitializeItemFilter();
		}

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

        public Dictionary<string, bool> SymbolStates
        {
            get
            {
                return this.settings.SymbolStates;
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

        public bool DrawWalls
        {
            get
            {
                return this.settings.IsWallsDrawable;
            }
            set
            {
                this.settings.IsWallsDrawable = value;
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

		// This is the highest version we have opened and said "Don't show again" to.
		public Int32 TopVersion
		{
			get
			{
				return this.settings.HighestVersion;
			}
			set
			{
				this.settings.HighestVersion = value;
			}
		}

        public bool DrawMarker(TileType type)
        {
            // convert to string index
			if (this.settings.SymbolStates.ContainsKey(Enum.GetName(typeof(TileType), type)))
				return this.settings.SymbolStates[Enum.GetName(typeof(TileType), type)];
			return false;
        }

		public bool DrawMarker(string marker)
		{
			if (this.settings.SymbolStates.ContainsKey(marker))
				return this.settings.SymbolStates[marker];
			return false;
		}

		public void MarkerVisible(string key, bool status)
		{
			this.settings.SymbolStates[key] = status;
		}
		
		public Boolean IsDefaultItem(String itemName)
		{
			if (this.DefaultFilterItems.Contains(itemName))
				return true;

			return false;
		}

		public void FilterItem(string itemName, bool status)
		{
			this.settings.ChestFilterItems[itemName] = status;
		}

		public Dictionary<string, bool> FilterItemStates
		{
			get
			{
				return this.settings.ChestFilterItems;
			}
		}

		public Boolean ScanForNewItems
		{
			get
			{
				return this.settings.ScanForNewChestItems;
			}
			set
			{
				this.settings.ScanForNewChestItems = value;
			}
		}

		public Boolean InConsole
		{
			get
			{
				return runningConsole;
			}
			set
			{
				runningConsole = value;
			}
		}

        public void Shutdown()
        {           
            // Serialize Symbols / Etc
            XmlSerializer outputSerializer = new XmlSerializer(this.settings.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            outputSerializer.Serialize(writer, this.settings);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(sb.ToString());
            xmlDocument.Save(Constants.ApplicationUserSettingsFile);
        }

        
    }
}
