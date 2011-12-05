using System;
using System.IO;
using System.Drawing;
using MoreTerra.Structures;

namespace MoreTerra.Utilities
{
	class BackwardsScanner
	{
		private FileStream stream;
		private BackwardsBinaryReader backReader;
		private Int32 MaxX, MaxY;
		private WorldHeader header;

		#region Constructors
		public BackwardsScanner(FileStream str, WorldHeader head)
		{
			stream = str;
			MaxX = head.MaxTiles.X;
			MaxY = head.MaxTiles.Y;
			header = head;
			backReader = new BackwardsBinaryReader(stream);
		}
		#endregion

		public Int64 SeekToChestsBackwards()
		{
			Footer useFooter;
			NPC useNPC;
			Sign useSign = new Sign();
			Item useItem = new Item();
			Chest useChest;
			Int32 signCount;
			Int32 chestCount;
			Int32 i, j;
			Int32 countByte;
			long beforeReads;

			stream.Seek(0, SeekOrigin.End);
			useFooter = tryReadFooterBackwards();

			// We'll fail if we don't have a good footer object.
			if (useFooter.Active == false)
				return 0;

			if (header.ReleaseNumber >= 0x24)
			{
				String NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.MechanicsName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.WizardsName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.TinkerersName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.DemolitionistsName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.ClothiersName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.GuidesName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.DryadsName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.ArmsDealersName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.NursesName = NPCName;

				NPCName = backReader.ReadBackwardsString(false);
				if (NPCName == null)
					return 0;
				header.MerchantsName = NPCName;
			}

			// The NPC section always ends with 00 for "No more NPCs".  
			if (backReader.ReadBackwardsByte() != 00)
				return 0;

			do
			{
				useNPC = tryReadNPCBackwards();
			} while (useNPC.Active == true);

			// So now we need to find a way to read backwards through all the zeros and find our way to the
			// last sign.

			// So at first it looked like most zeros we could have was 9 and still get a
			// good sign but Terraria only allows you to go to within 22 of the edges.
			// This means it's actually three max.
			signCount = countBackwardZeros(1003);

			// Every sign item ends with at least two zeros.  If we have less then
			// reading backwards failed.
			if (signCount < 2)
				return 0;

			if (signCount == 2)
			{
				stream.Seek(2, SeekOrigin.Current);
				signCount = 0;
			}
			else
			{
				stream.Seek(3, SeekOrigin.Current);
				signCount -= 3;
			}

			// Simple loop.  We set it to the earliest we could have a good sign then
			// keep reading in as many signs as we can.  If we don't get a good sign see
			// if we have a zero at the very start.  If so we can call it an empty sign
			// and shift over and try again.
			for (i = signCount; i < 1000; i++)
			{
				useSign = tryReadSignBackwards();

				if (useSign.Active == true)
					continue;

				j = backReader.ReadBackwardsByte();

				if (j != 0)
					return 0;

			}

			// Time to read the chests.

			// So just like signs the longest 0 string we can get is a pure empty chest with
			// Y coord less then 256.  This is 23.
			chestCount = countBackwardZeros(1023);

			if (chestCount < 23)
			{
				stream.Seek(chestCount, SeekOrigin.Current);
				chestCount = 0;
			}
			else
			{
				stream.Seek(23, SeekOrigin.Current);
				chestCount -= (23);
			}

			for (i = chestCount; i < 1000; i++)
			{
				beforeReads = stream.Position;

				for (j = 0; j < 20; j++)
				{
					countByte = backReader.PeekBackwardsByte();

					if (countByte != 00)
					{
						useItem = tryReadChestItemBackwards();

						if (useItem.Count == 0)
							return 0;
					}
					else
					{
						backReader.ReadBackwardsByte();
					}
				}

				useChest = tryReadChestHeaderBackwards();

				if (useChest.Active == false)
				{
					stream.Seek(beforeReads, SeekOrigin.Begin);

					countByte = backReader.ReadBackwardsByte();

					if (countByte != 00)
						return 0;
				}
			}

			return stream.Position;
		}

		private Footer tryReadFooterBackwards()
		{
			Footer newFooter = new Footer();

			newFooter.Id = backReader.ReadBackwardsInt32();
			newFooter.Name = backReader.ReadBackwardsString();
			newFooter.Active = backReader.ReadBackwardsBoolean();

			return newFooter;
		}

		/// <summary>
		/// Tries to make the next bytes backward in the stream fit into an Chest object.
		/// If it fails sets the position back to where it was.
		/// This does nothing at all for the items in the chest, only the Chest itself.
		/// </summary>
		/// <returns>An Chest object.  Active will be true on any valid Chest.</returns>
		private Chest tryReadChestHeaderBackwards()
		{
			Int32 x, y;
			Int32 strictBool;
			Chest returnChest = new Chest();
			long oldPosition = stream.Position;
			Boolean validChest = false;

#if (DEBUG == false)
			try
			{
#endif
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnChest.Coordinates = new Point(x, y);

				strictBool = backReader.ReadBackwardsByte();

				if (strictBool == 1)
					returnChest.Active = true;

				if (returnChest.Active == true && x != 00 && y != 00 && x < MaxX && y < MaxY)
					validChest = true;
#if (DEBUG == false)
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}
#endif

			if (validChest == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnChest.Active = false;
			}

			return returnChest;
		}

		/// <summary>
		/// Tries to make the next bytes backward in the stream fit into an Item object.
		/// If it fails sets the position back to where it was.
		/// </summary>
		/// <returns>An Item object.  Count will be non-zero on any valid item.</returns>
		private Item tryReadChestItemBackwards()
		{
			Item returnItem = new Item();
			long oldPosition = stream.Position;
			Boolean validItem = false;

#if (DEBUG == false)
			try
			{
#endif
				if (header.ReleaseNumber >= 0x24)
					returnItem.Prefix = backReader.ReadBackwardsByte();

				returnItem.Name = backReader.ReadBackwardsString();

				if (!String.IsNullOrEmpty(returnItem.Name))
				{
					returnItem.Count = backReader.ReadBackwardsByte();

					if (returnItem.Count != 0)
						validItem = true;
				}
#if (DEBUG == false)
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}
#endif

			if (validItem == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnItem.Count = 0;
			}

			return returnItem;
		}

		/// <summary>
		/// Tries to make the next bytes backward in the stream fit into an Sign object.
		/// If it fails sets the position back to where it was.
		/// </summary>
		/// <returns>An Sign object.  activeSign will be true for a valid Sign.</returns>
		private Sign tryReadSignBackwards()
		{
			Int32 x, y;
			Sign returnSign = new Sign();
			long oldPosition = stream.Position;
			Boolean validSign = false;

#if (DEBUG == false)
			try
			{
#endif
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnSign.Position = new Point(x, y);

				// We're going to try to read in the string.  In a Sign the string should
				// always have a 0x01 before it to show it was an active Sign.
//				returnSign.Text = backReader.ReadBackwardsString(true, 1500, 1);
				returnSign.Text = backReader.ReadBackwardsString(true, 1500);

				if (returnSign.Text != null)
				{
					returnSign.Active = backReader.ReadBackwardsBoolean();

					if (returnSign.Active == true && y != 0 && x != 0)
						validSign = true;
				}
#if (DEBUG == false)
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}
#endif

			if (validSign == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnSign.Active = false;
			}

			return returnSign;
		}

		/// <summary>
		/// Tries to make the next bytes backward in the stream fit into an NPC object.
		/// If it fails sets the position back to where it was.
		/// </summary>
		/// <returns>An NPC object.  activeNpc will be true for a valid Sign.</returns>
		private NPC tryReadNPCBackwards()
		{
			Int32 x, y;
			NPC returnNpc = new NPC();
			long oldPosition = stream.Position;
			Boolean validNPC = false;

#if (DEBUG == false)
			try
			{
#endif
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnNpc.HomeTile = new Point(x, y);

				returnNpc.Homeless = backReader.ReadBackwardsBoolean();

				returnNpc.Position = new PointSingle(backReader.ReadBackwardsSingle(),
					backReader.ReadBackwardsSingle());

				returnNpc.Name = backReader.ReadBackwardsString();

				if (!String.IsNullOrEmpty(returnNpc.Name))
				{

					returnNpc.Active = backReader.ReadBackwardsBoolean();

					if (returnNpc.Active == true)
						validNPC = true;
				}
#if (DEBUG == false)
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}
#endif

			if (validNPC == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnNpc.Active = false;
			}

			return returnNpc;
		}

		private Int32 countBackwardZeros(Int32 MaxCount = 0)
		{
			long oldPosition = stream.Position;

			Int32 upperBound = (int)oldPosition;
			Int32 count;
			Byte readByte;

			if (MaxCount > 0)
			{
				if (oldPosition > MaxCount)
					upperBound = MaxCount;
			}

			for (count = 0; count < upperBound; count++)
			{
				readByte = backReader.ReadBackwardsByte();

				if (readByte != 00)
				{
					// We need to shove it back forward a step now.
					backReader.ReadByte();
					break;
				}
			}

			if (count > upperBound)
				count = upperBound;

			return count;
		}
	}
}
