using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace MoreTerra.Utilities
{
	class CompressedXml
	{
		// This is only for static functions.
		private CompressedXml()
		{
		}

		public static void LoadXml(Stream inStream, out XmlDocument xmlDoc, Boolean keepOpen = false)
		{
			Int32 compressedSize, uncompressedSize;

			xmlDoc = null;

			if (inStream == null)
				return;

			if ((inStream.ReadByte() != (Int32)'M') || (inStream.ReadByte() != (Int32)'T') ||
				(inStream.ReadByte() != (Int32)'Z'))
				return;

			uncompressedSize = inStream.ReadByte();
			uncompressedSize += (inStream.ReadByte() * 0x100);
			uncompressedSize += (inStream.ReadByte() * 0x10000);
			uncompressedSize += (inStream.ReadByte() * 0x1000000);

			compressedSize = inStream.ReadByte();
			compressedSize += (inStream.ReadByte() * 0x100);
			compressedSize += (inStream.ReadByte() * 0x10000);
			compressedSize += (inStream.ReadByte() * 0x1000000);

			if (inStream.Length != (compressedSize + 11))
				return;

			DeflateStream ds = new DeflateStream(inStream, CompressionMode.Decompress, keepOpen);

			xmlDoc = new XmlDocument();
			xmlDoc.Load(ds);
		}

		public static void SaveToMTZ(String inFile, String outFile)
		{
			FileStream ifs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
			FileStream ofs = new FileStream(outFile, FileMode.Create, FileAccess.Write);

			Byte[] buffer = new Byte[11];

			Int64 uncompressedSize = ifs.Length;

			buffer[0] = (Byte)'M';
			buffer[1] = (Byte)'T';
			buffer[2] = (Byte)'Z';

			buffer[3] = (Byte)(uncompressedSize & 0xFF);
			buffer[4] = (Byte)((uncompressedSize & 0xFF00) >> 8);
			buffer[5] = (Byte)((uncompressedSize & 0xFF0000) >> 16);
			buffer[6] = (Byte)((uncompressedSize & 0xFF000000) >> 24);

			buffer[7] = 0;
			buffer[8] = 0;
			buffer[9] = 0;
			buffer[10] = 0;

			ofs.Write(buffer, 0, 11);

			DeflateStream ds = new DeflateStream(ofs, CompressionMode.Compress, true);
			ifs.CopyTo(ds);
			ds.Flush();
			ds.Close();
			ifs.Close();

			ofs.Seek(7, SeekOrigin.Begin);

			Int64 compressedSize = (ofs.Length - 11);
			buffer[3] = (Byte)(compressedSize & 0xFF);
			buffer[4] = (Byte)((compressedSize & 0xFF00) >> 8);
			buffer[5] = (Byte)((compressedSize & 0xFF0000) >> 16);
			buffer[6] = (Byte)((compressedSize & 0xFF000000) >> 24);

			ofs.Write(buffer, 3, 4);
			ofs.Close();
		}
	}
}
