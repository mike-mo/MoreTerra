using System;

namespace MoreTerra.Structures
{
    public class Item
    {
        private string name;
        private int count;
		private Int32 id;
		private Byte prefix;

		#region Constructors
        public Item(string name, int count, byte prefix)
        {
            this.name = name;
            this.count = count;
			this.prefix = prefix;
        }

		public Item()
		{
			this.name = null;
			this.count = 0;
			this.prefix = 0;
		}
		#endregion

		#region GetSet Functions
        public string Name
        {
            get
            {
                return this.name;
            }
			set
			{
				this.name = value;
			}
        }

        public int Count
        {
            get
            {
                return this.count;
            }
			set
			{
				this.count = value;
			}
        }

		public Int32 Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public Byte Prefix
		{
			get
			{
				return prefix;
			}
			set
			{
				id = value;
			}
		}
		#endregion

		#region Overrides
        public override string ToString()
        {
            if(count == 1)
            {
                return this.name;
            }
            else
            {
                return string.Format("{0}, Count: {1}", this.name, this.count);
            }
            
        }
		#endregion
    
    }
}
