using System;
using System.IO;

namespace MoreTerra.Utilities
{
	class BackwardsBinaryReader : BinaryReader
	{
		private long oldPosition;
		Stream inStream;

		#region Constructors
		public BackwardsBinaryReader(Stream input) : base(input)
		{
			oldPosition = input.Position;
			inStream = input;
		}
		#endregion

		/// <summary>
		/// Attempts to read in a string of a specific size.
		/// Only handles up to 16k byte strings.
		/// Only checks for a # that correctly matches with the size of what would be the string afterwords.
		/// </summary>
		/// <param name="readSize">Used when you know the size of the string already.  Runs faster.
		/// <returns>The string or null if one was not found.</returns>
		public String ReadSetSizeBackwardsString(Int32 readSize)
		{
			Int32 peekSpot;
			String retStr = null;
			long localOldPosition = inStream.Position;
			long newPosition;

			// There's not enough in the stream to cover the size we requested or we asked for a 0 length.
			if (readSize == 0 || readSize > inStream.Length || readSize > 0x3FFF)
				return null;

			inStream.Seek(-(readSize + 1), SeekOrigin.Current);

			if (readSize > 127)
			{
				newPosition = oldPosition - (readSize + 2);
			}
			else
			{
				peekSpot = base.PeekChar();

				// We have a match, let's give this a shot.
				if (peekSpot == readSize)
				{
					retStr = base.ReadString();
					newPosition = localOldPosition - (readSize + 1);
				}
				else
				{
					inStream.Seek(localOldPosition, SeekOrigin.Begin);
					return null;
				}
			}

			inStream.Seek(newPosition, SeekOrigin.Begin);

			return retStr;
		}

		/// <summary>
		/// Reads a string in from the end.
		/// Only handles 127 bytes unless you specifically tell it to go further.  Not recommended
		/// to set maxSize to high values unless you absolutely believe there will be that big of string.
		/// Does no processing on the string to see if it is all in printable characters.
		/// Only checks for a # that correctly matches with the size of what would be the string afterwords.
		/// </summary>
		/// <param name="allowEmpty">Whether an empty string is an ok thing to return.</param>
		/// <param name="maxSize">The highest size string we can attempt to read.</param>
		/// <returns>The string or null if one was not found.</returns>
		public String ReadBackwardsString(Boolean allowEmpty = false, Int32 maxSize = 127)
		{
			Int32 peekSpot;
			Int32 readEnd = 128;
			String retStr = null;
			long localOldPosition = inStream.Position;
			Int32 i = 0;
			Byte lowByte;

			if (maxSize > 0x3FFF)
				return null;

			if (localOldPosition < maxSize)
				maxSize = (int)localOldPosition - 1;

			if (maxSize < readEnd)
				readEnd = maxSize + 1;

			inStream.Seek(-1, SeekOrigin.Current);

			if (allowEmpty == true)
			{
				peekSpot = base.PeekChar();

				if (peekSpot == 0)
					return "";
			}

			for (i = 1; i < readEnd; i++)
			{
				if (i == 0x40)
					i = 0x40;

				inStream.Seek(-1, SeekOrigin.Current);
				peekSpot = base.PeekChar();

				if (peekSpot == 0)
					return null;

				if (peekSpot == i)
				{
					retStr = base.ReadString();
					break;
				}

			}

			// We didn't find it in the first 127.  Let's go the rest of the way.
			if (maxSize > 128 && i == readEnd)
			{
				lowByte = ReadBackwardsByte();
				readEnd = maxSize;

				for (i = 128; i < readEnd; i++)
				{
					peekSpot = lowByte * 128;

					lowByte = ReadBackwardsByte();

					// If the low byte doesn't have it's high bit set we know it's not a two byte UTF7 number.
					if (lowByte < 128)
						continue;

					peekSpot += (lowByte - 128);

					if (peekSpot == i)
					{
						retStr = base.ReadString();
						break;
					}
				}
			}

			if (i == readEnd)
			{
				inStream.Seek(localOldPosition, SeekOrigin.Begin);
				return null;
			}

			inStream.Seek(localOldPosition - (i + ((i < 128) ? 1 : 2)), SeekOrigin.Begin);

			return retStr;
		}

		public Boolean ReadBackwardsBoolean()
		{
			if (inStream.Position < sizeof(Boolean))
				throw new EndOfStreamException();

			inStream.Seek(-1, SeekOrigin.Current);

			if (base.PeekChar() == 0)
				return false;
			else
				return true;
		}

		public Byte ReadBackwardsByte()
		{
			Int32 retByte;

			if (inStream.Position < sizeof(Boolean))
				throw new EndOfStreamException();

			inStream.Seek(-1, SeekOrigin.Current);
			retByte = base.ReadByte();
			inStream.Seek(-1, SeekOrigin.Current);

			return (Byte)retByte;
			//return (Byte)base.PeekChar();
		}

		public Byte PeekBackwardsByte()
		{

			if (inStream.Position < sizeof(Boolean))
				throw new EndOfStreamException();

			inStream.Seek(-1, SeekOrigin.Current);
			return base.ReadByte();
		}

		public Single ReadBackwardsSingle()
		{
			Single retVal;

			if (inStream.Position < sizeof(Single))
				throw new EndOfStreamException();

			inStream.Seek(-sizeof(Single), SeekOrigin.Current);
			retVal = base.ReadSingle();
			inStream.Seek(-sizeof(Single), SeekOrigin.Current);

			return retVal;
		}

		public Double ReadBackwardsDouble()
		{
			Double retVal;

			if (inStream.Position < sizeof(Double))
				throw new EndOfStreamException();

			inStream.Seek(-sizeof(Double), SeekOrigin.Current);
			retVal = base.ReadDouble();
			inStream.Seek(-sizeof(Double), SeekOrigin.Current);

			return retVal;
		}

		public Int32 ReadBackwardsInt32()
		{
			Int32 retVal;

			if (inStream.Position < sizeof(Int32))
				throw new EndOfStreamException();

			inStream.Seek(-sizeof(Int32), SeekOrigin.Current);
			retVal = base.ReadInt32();
			inStream.Seek(-sizeof(Int32), SeekOrigin.Current);

			return retVal;
		}
	}
}
