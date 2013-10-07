using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using NDesk.Options;
using MoreTerra.Utilities;
using MoreTerra.Structures;
using MoreTerra.Structures.TerraInfo;

using System.Xml;
using System.Net;
using System.IO.Compression;
using System.Globalization;

//public class Win32
//{
//    /// <summary>
//    /// Allocates a new console for current process.
//    /// </summary>
//    [DllImport("kernel32.dll")]
//    public static extern bool AllocConsole();

//    /// <summary>
//    /// Frees the console.
//    /// </summary>
//    [DllImport("kernel32.dll")]
//    public static extern bool FreeConsole();
//}


namespace MoreTerra
{

    static class Program
    {

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

		static void ReadUpdate()
		{
			WebClient wc = new WebClient();
			String page = wc.DownloadString("http://moreterra.codeplex.com/wikipage?title=Updates");
			String header = "<div class=\"wikidoc\">";
			Int32 start = page.IndexOf(header) + header.Length;
			Int32 end = page.IndexOf("</div>", start);

			String versionString = String.Empty;
			versionString = page.Substring(start, end - start);
			versionString = versionString.Replace("&amp;", "&");

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(versionString);

			MessageBox.Show(xmlDoc.InnerXml);
		}

        [STAThread]
        static void Main(string[] args)
        {
			String error = Global.Instance.Initialize();

#if DEBUG == true
			if (error != String.Empty)
			{
				MessageBox.Show(error);
				return;
			}
#endif
			
            // Initialize Managers
			TileProperties.Initialize();
            ResourceManager.Instance.Initialize();
            SettingsManager.Instance.Initialize();

//            DumpNewWalls();
//            DumpNewTiles();
//            return;
//            World w = new World();
            //w.SanityCheckWorld("c:\\Users\\Jason\\Documents\\My Games\\Terraria\\Worlds\\1-2_World.wld");
            //return;

            if (args.Length == 0) //started from windows
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormWorldView());
                SettingsManager.Instance.Shutdown();                
            }
            else
            {
            
                //for (int i = 1; i < args.Length; i += 2)
                //{
                //    MessageBox.Show(args[i - 1] + args[i]);
                //}

				// See if we are running in Mono and if so do not do the Attach.
				// Ugly but at least it lets the code run.
				Type t = Type.GetType("Mono.Runtime");
				
				if (t == null)
					AttachConsole(ATTACH_PARENT_PROCESS);

				bool show_help = false;
                string worldPath = string.Empty;
                string mapPath = string.Empty;

                var p = new OptionSet () {
                    { "w|world=", "The path to the {WORLD} to map.",
                       v => worldPath = v },
                    { "o|output=", "The path to the {OUTPUT} file where the map PNG will be written.",
                       v => mapPath = v},
                    { "h|help",  "Show this message and exit.", 
                       v => show_help = v != null },
                };

                List<string> extra = new List<string>();

#if (DEBUG == false)
                try
                {
#endif
                    extra = p.Parse(args);
#if (DEBUG == false)
                }
                catch (OptionException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try '" + System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath) + " --help' for more information.");
                }
#endif


                if (show_help || args.Contains<string>("-?") || worldPath == string.Empty || mapPath == string.Empty)
                {
                    Console.WriteLine("Generates a PNG from a Terraria World file (*.wld).");
                    Console.WriteLine();
                    Console.WriteLine("Usage:");
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "NoGui.bat [option1] <path1> [option2] <path2>");
                    Console.WriteLine();
                    Console.WriteLine("Options:");
                    p.WriteOptionDescriptions(Console.Out);
                    Console.WriteLine();
                    Console.WriteLine("Example:");
                    Console.WriteLine();
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "NoGui.bat " +
                                      "-w \"C:\\Terraria Worlds\\world1.wld\" -o \"C:\\Terraria Maps\\World 1.png\"");
                }
                else if (worldPath == string.Empty || !File.Exists(worldPath))
                {
                    Console.WriteLine("The World file could not be found: " + worldPath);
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Generating map from:" + 
						Environment.NewLine + " " + worldPath);                    

                    WorldMapper mapper = new WorldMapper();

					Global.Instance.InConsole = true;
#if (DEBUG == false)
                    try
                    {
#endif
                        mapper.Initialize();
                        Console.WriteLine("Reading World file...");
                        mapper.OpenWorld();
						mapper.ProcessWorld(worldPath, null);
                        Console.WriteLine("World file closed. Generating PNG...");
                        mapper.CreatePreviewPNG(mapPath, null);
                        Console.WriteLine("Done! Saved to: " + Environment.NewLine + " " + mapPath);
#if (DEBUG == false)
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.ToString());
                    }
#endif
                }    
            }          
        }

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

                if (!String.IsNullOrEmpty(ti.autoGenType))
                    tileXML = tileXML + "autoGenType=\"" + ti.autoGenType + "\" ";

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

                if (wi.safe == false)
                    tileXML = tileXML + "unsafe=\"true\" ";

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
