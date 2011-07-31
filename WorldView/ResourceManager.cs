using System.Xml;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using MoreTerra.Structures;
using MoreTerra.Utilities;

namespace MoreTerra
{


    public sealed class ResourceManager
    {
        static ResourceManager instance = null;
        static readonly object mutex = new object();

        private Dictionary<TileType, Bitmap> symbolBitmaps;

        private ResourceManager()
        {
            this.symbolBitmaps = new Dictionary<TileType, Bitmap>();
        }

        public static ResourceManager Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new ResourceManager();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Initialize the resource directory
        /// </summary>
        public void Initialize()
        {
            // Check to see if root folder directory exists
            if (!Directory.Exists(Constants.ApplicationRootDirectory))
            {

				String oldRoot;
				String newRoot = Constants.ApplicationRootDirectory;

				Directory.CreateDirectory(Constants.ApplicationRootDirectory);

				foreach(String s in Constants.OldProgramNames)
				{

					oldRoot = System.IO.Path.Combine(Environment.GetFolderPath(
						Environment.SpecialFolder.ApplicationData), s);

					if (Directory.Exists(oldRoot))
					{
						MoveAndCombineDirectories(oldRoot, newRoot);
						Directory.Delete(oldRoot);
					}
				}
            }

            if (!Directory.Exists(Constants.ApplicationLogDirectory))
            {
                // Create it
                Directory.CreateDirectory(Constants.ApplicationLogDirectory);
            }

            if (!Directory.Exists(Constants.ApplicationResourceDirectory))
            {
                // Create it
                Directory.CreateDirectory(Constants.ApplicationResourceDirectory);
            }
            // Copy all the symbols
            ValidateSymbolResources();

            // Load
            LoadSymbols();
        }


        /// <summary>
        /// Copy the symbols externally to the resource directory
        /// </summary>
        public void ValidateSymbolResources()
        {
            Type resourceType = Util.GetResourceAssemblyType();

            foreach (string symbolName in Constants.ExternalSymbolNames)
            {
                // if it doesnt exist recopy
                if(!File.Exists(Path.Combine(Constants.ApplicationResourceDirectory, string.Format("{0}.png", symbolName))))
                {
                    Bitmap b = (Bitmap)Properties.Resources.ResourceManager.GetObject(symbolName);
                    SaveSymbolToResourceDirectory(b, symbolName);
                }
            }
        }

        /// <summary>
        /// Loads Symbols, filter which ones are actually enabled
        /// </summary>
        public void LoadSymbols()
        {
            //// Property file doesnt exist so recreate/reload
            //if(!File.Exists(Constants.ApplicationSymbolPropertiesFile))
            //{
            //    foreach (string symbolName in Constants.ExternalSymbolNames)
            //    {
            //        this.symbolProperties.Add((TileType)Enum.Parse(typeof(TileType), symbolName), new SymbolProperties(symbolName, true, GetSymbolPath(symbolName)));
            //    }

            //    // Serialize to File
            //    XmlSerializer outputSerializer = new XmlSerializer(this.symbolProperties.GetType());
            //    StringBuilder sb = new StringBuilder();
            //    StringWriter writer = new StringWriter(sb);
            //    outputSerializer.Serialize(writer, this.symbolProperties);
            //    XmlDocument xmlDocument = new XmlDocument();
            //    xmlDocument.LoadXml(sb.ToString());
            //    xmlDocument.Save(Constants.ApplicationSymbolPropertiesFile);
            //}
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(File.ReadAllText(Constants.ApplicationSymbolPropertiesFile));
            //XmlNodeReader reader = new XmlNodeReader(xmlDoc.DocumentElement);
            //XmlSerializer inputSerializer = new XmlSerializer(this.symbolProperties.GetType());
            //this.symbolProperties = (SerializableDictionary<TileType, SymbolProperties>)inputSerializer.Deserialize(reader);

            foreach (string symbolName in Constants.ExternalSymbolNames)
            {
                string symbolPath = GetSymbolPath(symbolName);
                this.symbolBitmaps.Add((TileType)Enum.Parse(typeof(TileType), symbolName), new Bitmap(symbolPath));
            }
        }

        public Bitmap GetSymbol(TileType symbolType)
        {
            if (!this.symbolBitmaps.ContainsKey(symbolType))
            {
                throw new ApplicationException(string.Format("Tile Type {0} Does not have an associated Symbol", symbolType));
            }
            return this.symbolBitmaps[symbolType];
        }

        public static string GetSymbolPath(string symbolName)
        {
            return Path.Combine(Constants.ApplicationResourceDirectory, string.Format("{0}.png", symbolName));
        }

        public static void SaveSymbolToResourceDirectory(Bitmap symbol, string name)
        {
            symbol.Save(Path.Combine(Constants.ApplicationResourceDirectory, string.Format("{0}.png", name)), ImageFormat.Png);
        }

		private void MoveAndCombineDirectories(String oldDir, String newDir)
		{
			String[] oldDirectories;
			String[] oldFiles;
			String newFileName;
			String newDirectoryName;

			#region MoveFiles
			oldFiles = Directory.GetFiles(oldDir);

			foreach (String file in oldFiles)
			{
				newFileName = Path.Combine(newDir, Path.GetFileName(file));

				if (File.Exists(newFileName))
				{
					// Find which one is newer and delete the older one.
					if (File.GetLastWriteTime(file) > File.GetLastWriteTime(newFileName))
					{
						File.Delete(newFileName);
						File.Move(file, newFileName);
					}
					else
					{
						File.Delete(file);
					}
				} else {
					File.Move(file, newFileName);
				}
			}
			#endregion

			oldDirectories = Directory.GetDirectories(oldDir);

			foreach (String dir in oldDirectories)
			{
				newDirectoryName = Path.Combine(newDir, Path.GetFileName(dir));

				if (Directory.Exists(newDirectoryName))
				{
					MoveAndCombineDirectories(dir, newDirectoryName);
					Directory.Delete(dir);
				}
				else
				{
					// It does not exist so let's just move it.
					Directory.Move(dir, newDirectoryName);
				}
			}
		}
    }
}
