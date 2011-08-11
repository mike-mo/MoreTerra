namespace MoreTerra.Structures
{
    using System;
    using System.Drawing;
	
    public class TileProperties
    {
        private bool isImportant;
        private TileType type;
        private Color colour;
        private bool hasSymbol;
		private bool drawSymbol;

		#region Constructors
		public TileProperties(TileType tileType, bool isTileImportant, Color colour, bool hasSymbol = false)
        {
            this.type = tileType;
            this.isImportant = isTileImportant;
            this.colour = colour;
            this.hasSymbol = hasSymbol;
			this.drawSymbol = false;
        }
		#endregion

		#region GetSet Functions
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

		public bool DrawSymbol
		{
			get
			{
				return this.drawSymbol;
			}
			set
			{
				drawSymbol = value;
			}
		}
		#endregion
	}
}
