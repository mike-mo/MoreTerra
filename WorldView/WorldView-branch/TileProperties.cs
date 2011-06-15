namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    public class TileProperties
    {
        private bool isImportant;
        private TileType type;
        private Color colour;
        private bool hasSymbol;

        public TileProperties(TileType tileType, bool isTileImportant, Color colour, bool hasSymbol = false)
        {
            this.type = tileType;
            this.isImportant = isTileImportant;
            this.colour = colour;
            this.hasSymbol = hasSymbol;
        }

        public TileType TileType
        {
            get
            {
                return this.type;
            }
        }

        public Color Colour
        {
            get
            {
                return this.colour;
            }
        }

        public bool HasSymbol
        {
            get
            {
                return this.hasSymbol;
            }
        }

        public bool IsImportant
        {
            get
            {
                return this.isImportant;
            }
        }
    }
}
