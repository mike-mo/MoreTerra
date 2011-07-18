using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
            public SerializableDictionary<string, bool> SymbolStates;
            public SerializableDictionary<string, bool> ChestFilterItems;
        }

        private static SettingsManager instance = null;
        private static readonly object mutex = new object();
        private UserSettings settings;

        private SettingsManager()
        {
            this.settings = new UserSettings();
            this.settings.SymbolStates = new SerializableDictionary<string, bool>();
            foreach (string s in Constants.ExternalSymbolNames)
            {
                this.settings.SymbolStates.Add(s, true);
            }


            InitializeItemFilter();


            this.settings.IsWallsDrawable = true;
            this.settings.InputWorldDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Terraria\\Worlds");
            if (!Directory.Exists(this.settings.InputWorldDirectory))
            {
                this.settings.InputWorldDirectory = string.Empty;
            }
        }

        private void InitializeItemFilter()
        {
            this.settings.ChestFilterItems = new SerializableDictionary<string, bool>();
            try
            {
                using (StreamReader list = new StreamReader("ItemList.txt"))
                {
                    while (!list.EndOfStream)
                    {
                        string s = list.ReadLine();

                        if (!this.settings.ChestFilterItems.ContainsKey(s))
                        {
                            this.settings.ChestFilterItems.Add(s, false);
                        }
                    }

                    list.Close();
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error reading the item list from ItemList.txt.\n\n" +
                                                        ex.ToString(), "Item List", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            if (!System.IO.File.Exists(Constants.ApplicationUserSettingsFile)) return;


            // Load User Preference File
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(Constants.ApplicationUserSettingsFile));
            XmlNodeReader reader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer inputSerializer = new XmlSerializer(this.settings.GetType());
            this.settings = (UserSettings)inputSerializer.Deserialize(reader);

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

        public bool DrawMarker(TileType type)
        {
            // convert to string index
            return this.settings.SymbolStates[Enum.GetName(typeof(TileType), type)];
        }

        public bool DrawMarker(string marker)
        {
            if (this.settings.SymbolStates.ContainsKey(marker)) return this.settings.SymbolStates[marker];
            else return false;
        }

        public void MarkerVisible(string key, bool status)
        {
            this.settings.SymbolStates[key] = status;
        }

        //public void FilterWeapon(string weaponName, bool status)
        //{
        //    this.settings.ChestFilterWeaponStates[weaponName] = status;
        //}

        //public void FilterAccessory(string accessoryName, bool status)
        //{
        //    this.settings.ChestFilterAccessoryStates[accessoryName] = status;
        //}

        public void FilterItem(string itemName, bool status)
        {
            this.settings.ChestFilterItems[itemName] = status;
        }

        public Dictionary<string, bool> FilterItemStates
        {
            get
            {
                //return this.settings.ChestFilterWeaponStates.Concat(this.settings.ChestFilterAccessoryStates).ToDictionary(pair => pair.Key, pair => pair.Value);
                return this.settings.ChestFilterItems;                
            }
        }

        //public Dictionary<string, bool> FilterWeaponStates
        //{
        //    get
        //    {
        //        return this.settings.ChestFilterWeaponStates;
        //    }
        //}

        //public Dictionary<string, bool> FilterAccessoryStates
        //{
        //    get
        //    {
        //        return this.settings.ChestFilterAccessoryStates;
        //    }
        //}

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
