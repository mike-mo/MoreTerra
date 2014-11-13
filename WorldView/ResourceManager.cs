using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using MoreTerra.Structures;
using MoreTerra.Utilities;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra
{


    public sealed class ResourceManager
    {
        static ResourceManager instance = null;
        static readonly object mutex = new object();
		
        private Dictionary<String, Bitmap> markerBitmaps;
		private Dictionary<String, Bitmap> customMarkerBitmaps;
		private Boolean useCustom;

        private ResourceManager()
        {
			useCustom = false;
            this.markerBitmaps = new Dictionary<String, Bitmap>();
			this.customMarkerBitmaps = new Dictionary<String, Bitmap>();
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
            if (!Directory.Exists(Global.ApplicationRootDirectory))
            {

				String oldRoot;
				String newRoot = Global.ApplicationRootDirectory;

				Directory.CreateDirectory(Global.ApplicationRootDirectory);

				foreach(String s in Global.OldProgramNames)
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

            if (!Directory.Exists(Global.ApplicationLogDirectory))
            {
                // Create it
                Directory.CreateDirectory(Global.ApplicationLogDirectory);
            }

            if (!Directory.Exists(Global.ApplicationResourceDirectory))
            {
                // Create it
                Directory.CreateDirectory(Global.ApplicationResourceDirectory);
            }

			// Load
			LoadMarkers();
			
			// Copy all the markers
            ValidateMarkerResources(false);

			// Load custom markers from the files.
			LoadCustomMarkers();

        }


        /// <summary>
        /// Copy the markers externally to the resource directory
        /// </summary>
        private void ValidateMarkerResources(Boolean overwrite)
        {
            foreach (KeyValuePair<String, Bitmap> kvp in markerBitmaps)
            {
                // if it doesnt exist recopy
                if((!File.Exists(Path.Combine(Global.ApplicationResourceDirectory,
					string.Format("{0}.png", kvp.Key)))) || overwrite == true)
                {
                    SaveMarkerToResourceDirectory(kvp.Value, kvp.Key);
                }
            }
        }

		private void LoadMarkers()
		{
			foreach (KeyValuePair<String, List<MarkerInfo>> kvp in Global.Instance.Info.MarkerSets)
			{
                Bitmap b = (Bitmap)Properties.Resources.ResourceManager.GetObject(kvp.Key);
                markerBitmaps.Add(kvp.Key, b);

                foreach (MarkerInfo mi in kvp.Value)
                {
                    b = (Bitmap)Properties.Resources.ResourceManager.GetObject(mi.markerImage);
                    markerBitmaps.Add(mi.markerImage, b);
                }
			}
		}


        /// <summary>
        /// Loads Markers
        /// </summary>
        private void LoadCustomMarkers()
        {
			// Changed to FileStream as new Bitmap locks the image down.
			// Once loaded why do we need to keep the file from changing?
			FileStream stream;
            foreach (KeyValuePair<String, Bitmap> kvp in markerBitmaps)
            {
				string markerPath = GetMarkerPath(kvp.Key);
				stream = new FileStream(markerPath, FileMode.Open);
                this.customMarkerBitmaps.Add(kvp.Key, new Bitmap(stream));
				stream.Close();
            }
        }

		public void ResetCustomMarkers()
		{
			this.customMarkerBitmaps.Clear();
			ValidateMarkerResources(true);
			LoadCustomMarkers();
		}

        public Bitmap GetMarker(MarkerType markerType)
        {
			return this.GetMarker(Enum.GetName(typeof(MarkerType), markerType));
        }

		public Bitmap GetMarker(String markerName)
		{
			if (useCustom == false)
			{
				if (this.markerBitmaps.ContainsKey(markerName))
					return this.markerBitmaps[markerName];
			}
			else
			{
				if (this.customMarkerBitmaps.ContainsKey(markerName))
					return this.customMarkerBitmaps[markerName];
			}
			throw new ApplicationException(string.Format("Tile type {0} does not have an associated Marker", markerName));
		}

        private string GetMarkerPath(string markerName)
        {
            return Path.Combine(Global.ApplicationResourceDirectory, string.Format("{0}.png", markerName));
        }

        private void SaveMarkerToResourceDirectory(Bitmap marker, string name)
        {
            marker.Save(Path.Combine(Global.ApplicationResourceDirectory, string.Format("{0}.png", name)), ImageFormat.Png);
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

		public Boolean Custom
		{
			get
			{
				return useCustom;
			}
			set
			{
				useCustom = value;
			}
		}

    }
}
