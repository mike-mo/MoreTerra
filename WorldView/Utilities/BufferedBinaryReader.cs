using System;
using System.IO;

namespace MoreTerra.Utilities
{
	class BufferedBinaryReader
	{
		private Int32 bufferSize;
		private Int32 maxBufferSize;
		private Byte[] buffer;
		private Stream stream;
		private Int64 bufferPosition;
		private Int64 startPosition;
		private Int64 endPosition;
		private Int64 streamEnd;
		private Int32 oldMininum;
		
		public BufferedBinaryReader(Stream str, Int32 bSize, Int32 oldMin)
		{
			if (str == null)
				return;

			stream = str;

			streamEnd = stream.Length;
			oldMininum = oldMin;

			if (bSize < 0)
				return;

			maxBufferSize = bSize;

			buffer = new Byte[maxBufferSize];

			bufferPosition = stream.Position;
			startPosition = stream.Position;
			endPosition = startPosition + bufferSize;
		}

		public Boolean ReadBoolean()
		{
			Int64 readPos;


			readPos = bufferPosition - startPosition;

			if (readPos >= bufferSize)
			{
				FillBuffer();

				if (bufferSize <= 0)
					throw new EndOfStreamException();

				readPos = bufferPosition - startPosition;
			}

			bufferPosition++;
			if (buffer[readPos] > 1)
				return true;

			return false;
		}

		public Byte ReadByte()
		{
			Int64 readPos;

			readPos = bufferPosition - startPosition;

			if (readPos >= bufferSize)
			{
				FillBuffer();

				if (bufferSize <= 0)
					throw new EndOfStreamException();

				readPos = bufferPosition - startPosition;
			}

			bufferPosition++;
			return buffer[readPos];
		}

		public short ReadInt16()
		{
			Int16 ret16 = 0;

			Int64 readPos;

			readPos = bufferPosition - startPosition;

			if (readPos >= bufferSize - 1)
			{
				FillBuffer();

				if (bufferSize <= 0)
					throw new EndOfStreamException();

				readPos = bufferPosition - startPosition;
			}

			bufferPosition += 2;
			ret16 = buffer[readPos++];
			ret16 += (Int16) (buffer[readPos++] * 256);

			return ret16;
		}


		private void FillBuffer()
		{
			Int64 readOffset;
			Int32 readCount;
			Int32 readBytes;

			readOffset = stream.Position - oldMininum;

			if (readOffset < 0)
				readOffset = 0;

			readCount = maxBufferSize;
			if (readOffset + readCount > streamEnd)
				readCount = (Int32) (streamEnd - readOffset);

			stream.Seek(readOffset, SeekOrigin.Begin);
			readBytes = stream.Read(buffer, 0, readCount);

			startPosition = readOffset;
			endPosition = readOffset + readBytes;
			bufferSize = readBytes;
		}

		public void Seek(Int64 offset)
		{
			bufferPosition = offset;
		}

		public Int64 Position
		{
			get
			{
				return bufferPosition;
			}
		}

	}
}
