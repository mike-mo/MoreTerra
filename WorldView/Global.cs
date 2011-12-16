using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using MoreTerra.Structures.TerraInfo;

namespace MoreTerra
{
	public class Global
	{
		private static Global instance;
		private static readonly Object mutex = new Object();

		private TerraInfo info;

		// Added to allow certain things to never happen when the console is on.
		// Mainly pop-up forms and Dialogs.
		private Boolean runningConsole = false;

		// This is here to stop handler code from running when we do not wish it to.
		// Checkboxes have a bad habit of running when we set their checked state in the code.
		private Boolean blockCustomHandlers = false;

		// Our constants and other static readonly variables.
		public const Int32 CurrentVersion = 36;

		public const string Credits = @"TJChap2840, Vib Rib, Infinite Monkeys, Dr VideoGames 0031, " + 
			"Musluk, Sanktanglia, Metamorf.\r\n\r\nAnd special thanks to kdfb for donating a copy of the game!";
                                       
		public const int ChestMaxItems = 20;
		public const int ChestMaxNumber = 1000;
		public const Int32 SignMaxNumber = 1000;
		public const Int32 SignMaxSize = 1500;

		public const Int32 OldLightingCrop = 24;
		public const Int32 NewLightingCrop = 41;

		public static readonly string ApplicationRootDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MoreTerra");

		public static readonly string ApplicationLogDirectory = System.IO.Path.Combine(ApplicationRootDirectory, "Logs");
		public static readonly string ApplicationResourceDirectory = System.IO.Path.Combine(ApplicationRootDirectory, "Resources");

		public static readonly string ApplicationUserSettingsFile = System.IO.Path.Combine(ApplicationRootDirectory, "UserSettings.xml");

		public static readonly string[] OldProgramNames = { "TerrariaWorldViewer", "MoreTerrra" };

		#region Constructors
		private Global()
		{

		}
		#endregion

		#region Initlialization
		public String Initialize()
		{
			info = new TerraInfo();


			String errors = info.LoadInfo(Properties.Resources.Items);

			if (errors != String.Empty)
				return errors;

			return String.Empty;
		}

		public static Global Instance
		{
			get
			{
				lock (mutex)
				{
					if (instance == null)
						instance = new Global();
				}
				return instance;
			}
		}
		#endregion

		#region Get/Set functions
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

		public TerraInfo Info
		{
			get
			{
				return info;
			}
			set
			{
				info = value;
			}
		}

		public Boolean SkipEvents
		{
			get
			{
				return blockCustomHandlers;
			}
			set
			{
				blockCustomHandlers = value;
			}
		}

		#endregion

		#region Helper Functions
		public static Boolean TryParseColor(String colorString, out Color ret)
		{
			Byte red, green, blue;
			ret = Color.Black;

			// One for # and three sets of two characters.
			if (colorString.Length != 7)
				return false;

			if (colorString.IndexOf('#') != 0)
				return false;

			if (Byte.TryParse(colorString.Substring(1, 2), NumberStyles.HexNumber, null, out red) == false)
				return false;

			if (Byte.TryParse(colorString.Substring(3, 2), NumberStyles.HexNumber, null, out green) == false)
				return false;

			if (Byte.TryParse(colorString.Substring(5, 2), NumberStyles.HexNumber, null, out blue) == false)
				return false;

			ret = Color.FromArgb(red, green, blue);

			return true;
		}

		public static String ToColorString(Color color)
		{
			return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
		}
		#endregion

	}
}
