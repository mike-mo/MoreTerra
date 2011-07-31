using System.Drawing;
using System.IO;
namespace MoreTerra
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Drawing.Imaging;
	using System.Windows.Forms;
	using MoreTerra.Structures;
	using MoreTerra.Utilities;

	public class WorldReader
	{
		private long tileOffset;
		private long chestOffset;

		private string filePath;
		private FileStream stream;
		private BinaryReader reader;
		private BackwardsBinaryReader backReader;

		// This is a bit of a hack to keep the tile sizes without having access to the Header object.
		private Int32 maxXTiles, maxYTiles;

		public Profiler prof;

		public BinaryReader Reader
		{
			get
			{
				return reader;
			}
		}

		public WorldReader(string filePath)
		{
			this.filePath = filePath;
			this.stream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			this.backReader = new BackwardsBinaryReader(stream);
			this.reader = (BinaryReader)this.backReader;
			tileOffset = 0;
			chestOffset = 0;
		}

		public void SeekToTiles()
		{
			this.stream.Seek(this.tileOffset, SeekOrigin.Begin);
		}

		public Boolean SeekToChestsBackwards()
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
			useFooter = ReadFooter(true);

			// We'll fail if we don't have a good footer object.
			if (useFooter.activeFooter == false)
				return false;

			// The NPC section always ends with 00 for "No more NPCs".  
			if (backReader.ReadBackwardsByte() != 00)
				return false;

			do
			{
				useNPC = tryReadNPCBackwards();
			} while (useNPC.activeNpc == true);

			// So now we need to find a way to read backwards through all the zeros and find our way to the
			// last sign.

			// So at first it looked like most zeros we could have was 9 and still get a
			// good sign but Terraria only allows you to go to within 22 of the edges.
			// This means it's actually three max.
			signCount = countBackwardZeros(Constants.SignMaxNumber + 3);

			// Every sign item ends with at least two zeros.  If we have less then
			// reading backwards failed.
			if (signCount < 2)
				return false;

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
			for (i = signCount; i < Constants.SignMaxNumber; i++)
			{
				useSign = tryReadSignBackwards();

				if (useSign.activeSign == true)
					continue;

				j = backReader.ReadBackwardsByte();

				if (j != 0)
					return false;

			}

			// Time to read the chests.

			// So just like signs the longest 0 string we can get is a pure empty chest with
			// Y coord less then 256.  This is 23.
			chestCount = countBackwardZeros(Constants.ChestMaxNumber + Constants.ChestMaxItems + 3);

			if (chestCount < Constants.ChestMaxItems + 3)
			{
				stream.Seek(chestCount, SeekOrigin.Current);
				chestCount = 0;
			}
			else
			{
				stream.Seek(Constants.ChestMaxItems + 3, SeekOrigin.Current);
				chestCount -= (Constants.ChestMaxItems + 3);
			}

			for (i = chestCount; i < Constants.ChestMaxNumber; i++)
			{
				beforeReads = stream.Position;

				for (j = 0; j < Constants.ChestMaxItems; j++)
				{
					countByte = backReader.PeekBackwardsByte();

					if (countByte != 00)
					{
						useItem = tryReadChestItemBackwards();

						if (useItem.Count == 0)
							return false;
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
						return false;
				}
			}

			chestOffset = stream.Position;
			return true;
		}

		public void SeekToChests()
		{
			if (chestOffset == 0)
			{
				// Give going backwards a shot.  If it fails for some reason go forwards, it's longer but safer.
				if (SeekToChestsBackwards() == true)
					return;

				WorldHeader header = ReadHeader();

				int maxX = (int)header.MaxTiles.Y;
				int maxY = (int)header.MaxTiles.X;

				for (int col = 0; col < maxX; col++)
				{
					for (int row = 0; row < maxY; row++)
					{
						GetNextTile();
					}
				}

				chestOffset = stream.Position;
			}
			else
			{
				stream.Seek(chestOffset, SeekOrigin.End);
			}
		}

		private void SkipToNextTile()
		{
			bool isTileActive = reader.ReadBoolean();
			TileType tileType = TileType.Unknown;
			byte blockType = 0x00;
			if (isTileActive)
			{
				blockType = reader.ReadByte();
				if (WorldMapper.tileTypeDefs[blockType].IsImportant)
				{
					reader.BaseStream.Seek(4, SeekOrigin.Current);
				}
				tileType = WorldMapper.tileTypeDefs[blockType].TileType;
			}
			else
			{
				tileType = TileType.Sky;
			}

			bool isWall = reader.ReadBoolean();
			if (isWall) reader.BaseStream.Seek(1, SeekOrigin.Current);
			bool isLiquid = reader.ReadBoolean();
			if (isLiquid)
			{
				byte liquidLevel = reader.ReadByte();
				bool isLava = reader.ReadBoolean();

			}

		}

		public WorldHeader ReadHeader(Boolean checkForVersion = false)
		{
			// Reset to origin
			stream.Seek(0, SeekOrigin.Begin);

			int releaseNumber = this.reader.ReadInt32();

			if (checkForVersion == true && releaseNumber > SettingsManager.Instance.TopVersion)
			{
				if (SettingsManager.Instance.InConsole)
				{
					Console.WriteLine(String.Format("This world is from file version {0} while this " +
					"version of MoreTerra is only designed to run version {1}." + Environment.NewLine +
					"This world might not work at all or might have bugs.", releaseNumber, Constants.currentVersion));
				}
				else
				{
					String lText = String.Format("This world is from file version {0} while this " +
					"version of MoreTerra is only designed to run version {1}." +
					Environment.NewLine + Environment.NewLine +
					"Continuing to load this world might not work at all or might have bugs. " +
					"Are you sure you wish to continue?", releaseNumber, Constants.currentVersion);
					String cbText = "Do not show for this version again.";
					String tText = "Version higher than expected!";

					FormMessageBoxWithCheckBox Warning = new FormMessageBoxWithCheckBox(lText, cbText, tText);
					DialogResult dr = Warning.ShowDialog();

					if (dr == DialogResult.Cancel)
						return new WorldHeader();

					if (Warning.checkBoxChecked == true)
						SettingsManager.Instance.TopVersion = releaseNumber;

					if (dr == DialogResult.No)
						return new WorldHeader();
				}
			}

			WorldHeader header;
			if (releaseNumber == 38)
			{
				header = new WorldHeader
				{
					ReleaseNumber = releaseNumber,
					Name = this.reader.ReadString(),
					//           Id = this.reader.ReadInt32(),
					WorldCoords = new Rect(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32()),
					MaxTiles = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					SpawnPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					SurfaceLevel = this.reader.ReadDouble(),
					RockLayer = this.reader.ReadDouble(),
					TemporaryTime = this.reader.ReadDouble(),
					IsDayTime = this.reader.ReadBoolean(),
					MoonPhase = this.reader.ReadInt32(),
					IsBloodMoon = this.reader.ReadBoolean(),
					DungeonPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					IsBoss1Dead = this.reader.ReadBoolean(),
					IsBoss2Dead = this.reader.ReadBoolean(),
					IsBoss3Dead = this.reader.ReadBoolean(),
					IsShadowOrbSmashed = this.reader.ReadBoolean(),
					IsMeteorSpawned = this.reader.ReadBoolean(),
					ShadowOrbsSmashed = this.reader.ReadByte(),
					InvasionDelay = this.reader.ReadInt32(),
					InvasionSize = this.reader.ReadInt32(),
					InvasionType = this.reader.ReadInt32(),
					InvasionPointX = this.reader.ReadDouble(),
				};
			}
			else
			{
				header = new WorldHeader
				{
					ReleaseNumber = releaseNumber,
					Name = this.reader.ReadString(),
					Id = this.reader.ReadInt32(),
					WorldCoords = new Rect(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32()),
					MaxTiles = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					SpawnPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					SurfaceLevel = this.reader.ReadDouble(),
					RockLayer = this.reader.ReadDouble(),
					TemporaryTime = this.reader.ReadDouble(),
					IsDayTime = this.reader.ReadBoolean(),
					MoonPhase = this.reader.ReadInt32(),
					IsBloodMoon = this.reader.ReadBoolean(),
					DungeonPoint = new Point(this.reader.ReadInt32(), this.reader.ReadInt32()),
					IsBoss1Dead = this.reader.ReadBoolean(),
					IsBoss2Dead = this.reader.ReadBoolean(),
					IsBoss3Dead = this.reader.ReadBoolean(),
					IsShadowOrbSmashed = this.reader.ReadBoolean(),
					IsMeteorSpawned = this.reader.ReadBoolean(),
					ShadowOrbsSmashed = this.reader.ReadByte(),
					InvasionDelay = this.reader.ReadInt32(),
					InvasionSize = this.reader.ReadInt32(),
					InvasionType = this.reader.ReadInt32(),
					InvasionPointX = this.reader.ReadDouble(),
				};

			}

			maxXTiles = header.MaxTiles.X;
			maxYTiles = header.MaxTiles.Y;

			this.tileOffset = stream.Position;
			return header;
		}

		public TileType GetNextTile()
		{
			bool isTileActive = reader.ReadBoolean();
			TileType tileType = TileType.Unknown;
			byte blockType = 0x00;
			if (isTileActive)
			{
				blockType = reader.ReadByte();
				if (WorldMapper.tileTypeDefs[blockType].IsImportant)
				{
					reader.ReadInt16();
					reader.ReadInt16();
				}
				tileType = WorldMapper.tileTypeDefs[blockType].TileType;
			}
			else
			{
				tileType = TileType.Sky;
			}
			bool isLighted = reader.ReadBoolean();
			bool isWall = reader.ReadBoolean();
			if (isWall)
			{
				byte wallType = reader.ReadByte();
				if (tileType == TileType.Unknown || tileType == TileType.Sky)
				{
					if (!WorldMapper.tileTypeDefs.ContainsKey((int)wallType + Constants.WallOffset))
					{
						tileType = TileType.Unknown;
					}
					else
					{
						tileType = WorldMapper.tileTypeDefs[(int)wallType + Constants.WallOffset].TileType;
					}

				}
			}
			bool isLiquid = reader.ReadBoolean();
			if (isLiquid)
			{
				byte liquidLevel = reader.ReadByte();
				bool isLava = reader.ReadBoolean();
				if (isWall || tileType == TileType.Sky)
				{
					tileType = isLava ? TileType.Lava : TileType.Water;
				}

			}
			return tileType;
		}

		/// <summary>
		/// Gets the next chest, if the next item is not a chest, return null
		/// </summary>
		/// <returns></returns>
		public Chest GetNextChest(int chestId)
		{
			bool isChest = reader.ReadBoolean();
			if (!isChest)
			{
				return null;
			}
			Chest chest = new Chest(chestId, new Point(this.reader.ReadInt32(), this.reader.ReadInt32()));

			// Iterate through items contained within chest
			for (int i = 0; i < Constants.ChestMaxItems; i++)
			{
				byte count = this.reader.ReadByte();
				if ((int)count > 0)
				{
					chest.AddItem(new Item(reader.ReadString(), (int)count));
				}
			}
			return chest;
		}

		public Sign GetNextSign(int signId)
		{
			bool isSign = reader.ReadBoolean();
			if (!isSign)
				return null;

			Sign sign = new Sign(signId, isSign, reader.ReadString(), 
								 new Point(reader.ReadInt32(), reader.ReadInt32()));

			return sign;
		}

		public NPC GetNextNPC(int npcId)
		{
			bool isNpc = reader.ReadBoolean();
			if (!isNpc)
				return null;

			NPC npc = new NPC(npcId, isNpc, reader.ReadString(),
							  new Point((Int32) reader.ReadSingle(), (Int32) reader.ReadSingle()),
							  reader.ReadBoolean(), new Point(reader.ReadInt32(), reader.ReadInt32()));

			return npc;
		}

		public void Close()
		{
			this.reader.Close();
		}

		public static String GetWorldName(String worldFile)
		{
			WorldReader reader;
			WorldHeader header;
			String worldName;

			reader = new WorldReader(worldFile);
			header = reader.ReadHeader();

			if (reader.CompareFooter(header.Name, header.Id) == true)
				worldName = header.Name;
			else
				worldName = "Not a valid World file";

			reader.Close();

			return worldName;

		}

		private Footer ReadFooter(Boolean backwards = false)
		{
			Footer newFooter = new Footer();

			if (backwards == true)
			{
				newFooter.worldId = backReader.ReadBackwardsInt32();
				newFooter.worldName = backReader.ReadBackwardsString();
				newFooter.activeFooter = backReader.ReadBackwardsBoolean();
			}
			else
			{
				newFooter.activeFooter = reader.ReadBoolean();
				newFooter.worldName = reader.ReadString();
				newFooter.worldId = reader.ReadInt32();
			}

			return newFooter;
		}

		private Boolean CompareFooter(String worldName, Int32 worldId)
		{
			Footer footer;

			Boolean returnVal = false;

			long position = this.stream.Position;

			this.stream.Seek(0, SeekOrigin.End);

			try
			{
				footer = ReadFooter(true);

				if (footer.activeFooter == true && footer.worldName == worldName && footer.worldId == worldId)
					returnVal = true;
			}
			catch (EndOfStreamException e)
			{
				// We don't need to do this but I do just so the compiler will not throw an warning
				// for never using it.
				e.GetType();

				// If we read past the end of the stream we know it did not match.
				returnVal = false;
			}

			// We set the position back to where it was when we were called.
			this.stream.Seek(position, SeekOrigin.Begin);
			return returnVal;
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

			try
			{
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnChest.Coordinates = new Point(x, y);

				strictBool = backReader.ReadBackwardsByte();

				if (strictBool == 1)
					returnChest.Active = true;

				if (returnChest.Active == true && x != 00 && y != 00 && x < maxXTiles && y < maxYTiles)
					validChest = true;
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}

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

			try
			{
				returnItem.Name = backReader.ReadBackwardsString();

				if (!String.IsNullOrEmpty(returnItem.Name))
				{
					returnItem.Count = backReader.ReadBackwardsByte();

					if (returnItem.Count != 0)
						validItem = true;
				}
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}

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

			try
			{
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnSign.signPosition = new Point(x, y);

				returnSign.signText = backReader.ReadBackwardsString(true, Constants.SignMaxSize);

				if (returnSign.signText != null)
				{
					returnSign.activeSign = backReader.ReadBackwardsBoolean();

					if (returnSign.activeSign == true && y != 0 && x != 0)
						validSign = true;
				}
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}

			if (validSign == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnSign.activeSign = false;
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
			Single sx, sy;
			NPC returnNpc = new NPC();
			long oldPosition = stream.Position;
			Boolean validNPC = false;

			try
			{
				y = backReader.ReadBackwardsInt32();
				x = backReader.ReadBackwardsInt32();

				returnNpc.npcHomeTile = new Point(x, y);

				returnNpc.isNpcHomeless = backReader.ReadBackwardsBoolean();

				sy = backReader.ReadBackwardsSingle();

				sx = backReader.ReadBackwardsSingle();

				x = (int)sx;
				y = (int)sy;

				if (x != sx || y != sy)
					x = y;

				returnNpc.npcPosition = new Point(x, y);

				returnNpc.npcName = backReader.ReadBackwardsString();

				if (!String.IsNullOrEmpty(returnNpc.npcName))
				{

					returnNpc.activeNpc = backReader.ReadBackwardsBoolean();

					if (returnNpc.activeNpc == true)
					{
						foreach (String name in Constants.NPCList)
						{
							if (returnNpc.npcName == name)
							{
								validNPC = true;
								break;
							}
						}
					}
				}
			}
			catch (EndOfStreamException e)
			{
				e.GetType();
			}

			if (validNPC == false)
			{
				stream.Seek(oldPosition, SeekOrigin.Begin);
				returnNpc.activeNpc = false;
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
