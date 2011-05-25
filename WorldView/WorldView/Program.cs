namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using WorldView;

    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize Manager
            ResourceManager.Instance.Initialize();
            SettingsManager.Instance.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WorldViewForm());

            SettingsManager.Instance.Shutdown();
        }
    }
}
