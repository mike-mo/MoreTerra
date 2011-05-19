using System.IO;
using System.Drawing;
namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Symbol
    {
        private Bitmap bmp;

        public Symbol(string symbolPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(string.Format("WorldView.{0}", symbolPath));
            this.bmp = new Bitmap(stream);
            stream.Close();
        }

        public Bitmap BMP
        {
            get
            {
                return this.bmp;
            }
        }
    }
}
