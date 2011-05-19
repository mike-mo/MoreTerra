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
        private Symbol symbol;

        public TileProperties(TileType tileType, bool isTileImportant, Color colour, Symbol symbol = null)
        {
            this.type = tileType;
            this.isImportant = isTileImportant;
            this.colour = colour;
            this.symbol = symbol;
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

        public Symbol Symbol
        {
            get
            {
                return this.symbol;
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
