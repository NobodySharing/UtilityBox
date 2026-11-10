using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace VPE
{
	public static class Codec
	{
		/// <summary>Charset used for base-128 encoding. Aimed at maximum compatibility for different sites.</summary>
		private static readonly char[] Base128 =
		[
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'Á', 'Ä', 'Æ', 'B', 'C', 'Č', 'D', 'Ď', 'E', 'É', 'Ě', 'Ë', 'F', 'G', 'Ğ', 'H', 'I', 'Í', 'J', 'K', 'L', 'Ł', 'Ľ', 'M', 'N', 'Ň', 'Ñ', 'O', 'Ó', 'Ö', 'P', 'Q', 'R', 'Ř', 'S', 'Š', 'T', 'Ť', 'Þ', 'U', 'Ú', 'Ů', 'Ü', 'V', 'W', 'X', 'Y', 'Ý', 'Z', 'Ž',
			'a', 'á', 'ä', 'æ', 'b', 'c', 'č', 'd', 'ď', 'e', 'é', 'ě', 'ë', 'f', 'g', 'ğ', 'h', 'i', 'í', 'j', 'k', 'l', 'ł', 'ľ', 'm', 'n', 'ň', 'ñ', 'o', 'ó', 'ö', 'p', 'q', 'r', 'ř', 's', 'š', 't', 'ť', 'þ', 'u', 'ú', 'ů', 'ü', 'v', 'w', 'x', 'y', 'ý', 'z', 'ž',
			',', '.', '¿', '¡', '?', '!', ':', ';', '§', '¶', '°', '¢', '±', '«', '»', '€'
		];
		/// <summary>Specifies the encrypted type. Either as characters, or base-128, or in binary, either without or with dashes.</summary>
		public enum Encoding
		{
			Text = 0,
			Base64,
			Base128,
			Base128WithSpaces,
			Binary,
			BinaryWithDashes
		}
		/// <summary>Version of encoded message. At the time of creation, I am unsure what I will do with this, but it seems like a good idea to put it there, since it is highly likely things will change in the future.</summary>
		private static readonly byte Version = 0;
		/// <summary>Encodes a message to specified encoding, text output only.</summary>
		/// <param name="message">Message to encode, as list of ushorts.</param>
		/// <param name="encoding">Which encoding?</param>
		/// <returns>Text representation of encoded message.</returns>
		public static string Encode(List<ushort> message, Encoding encoding)
		{
			string result = "";
			switch (encoding)
			{
				case Encoding.Text:
					result = Codepage.ConvertToString(message);
					break;
				case Encoding.Base64:
					byte error = TightlyPackToBytes(message, out byte[] encoded, false);
					if (error == 0)
					{
						result = Convert.ToBase64String(encoded);
					}
					break;
				case Encoding.Base128:
					error = TightlyPackToBytes(message, out encoded, true);
					if (error == 0)
					{
						result = Codepage.ConvertToBase128Text(encoded, false);
					}
					break;
				case Encoding.Base128WithSpaces:
					error = TightlyPackToBytes(message, out encoded, true);
					if (error == 0)
					{
						result = Codepage.ConvertToBase128Text(encoded, true);
					}
					break;
				case Encoding.Binary:
					error = TightlyPackToBytes(message, out encoded, false);
					if (error == 0)
					{
						result = Convert.ToHexString(encoded);
					}
					break;
				case Encoding.BinaryWithDashes:
					error = TightlyPackToBytes(message, out encoded, false);
					if (error == 0)
					{
						result = BitConverter.ToString(encoded);
					}
					break;
			}
			return result;
		}
		/// <summary>Encodes a message to a byte array, for writing as a file.</summary>
		/// <param name="message">Message to encode, as list of ushorts.</param>
		/// <returns>Message as a byte array.</returns>
		public static byte[] Encode(List<ushort> message)
		{
			byte error = TightlyPackToBytes(message, out byte[] encoded, false);
			if (error == 0)
			{
				return encoded;
			}
			return [];
		}
		/// <summary>Decodes a message in specified encoding, text input only.</summary>
		/// <param name="message">Message to encode, as a text.</param>
		/// <param name="encoding">Which encoding?</param>
		/// <returns>List of ushorts representing decoded message.</returns>
		public static List<ushort> Decode(string message, Encoding encoding)
		{
			List<ushort> result = [];
			switch (encoding)
			{
				case Encoding.Text:
					result = Codepage.ConvertToNums(message);
					break;
				case Encoding.Base64:
					byte[] data = Convert.FromBase64String(message);
					byte error = UnpackTightlyPackedBytes(data, out List<ushort> temp, false);
					if (error == 0)
					{
						result = temp;
					}
					break;
				case Encoding.Base128:
					data = Codepage.ConvertFromBase128Text(message);
					error = UnpackTightlyPackedBytes(data, out temp, true);
					if (error == 0)
					{
						result = temp;
					}
					break;
				case Encoding.Base128WithSpaces:
					message = message.Replace(" ", "");
					data = Codepage.ConvertFromBase128Text(message);
					error = UnpackTightlyPackedBytes(data, out temp, true);
					if (error == 0)
					{
						result = temp;
					}
					break;
				case Encoding.Binary:
					data = Convert.FromHexString(message);
					error = UnpackTightlyPackedBytes(data, out temp, false);
					if (error == 0)
					{
						result = temp;
					}
					break;
				case Encoding.BinaryWithDashes:
					data = Convert.FromHexString(message.Replace("-", ""));
					error = UnpackTightlyPackedBytes(data, out temp, false);
					if (error == 0)
					{
						result = temp;
					}
					break;
			}
			return result;
		}
		/// <summary>Decodes a message in specified encoding, binary input only.</summary>
		/// <param name="binaryFile">Message as a byte array.</param>
		/// <returns>List of ushorts representing decoded message.</returns>
		public static List<ushort> Decode(byte[] binaryFile)
		{
			byte error = UnpackTightlyPackedBytes(binaryFile, out List<ushort> decoded, false);
			if (error == 0)
			{
				return decoded;
			}
			return [];
		}
		/// <summary>Tightly packs a set of numbers. No unused bits will remain.</summary>
		/// <param name="nums">Numbers to pack.</param>
		/// <param name="result">Tightly packed numbers, in bytes. Or base-128 numbers.</param>
		/// <param name="base128">Encode to base-128 numbers?</param>
		/// <returns>Error code. 0 = OK, 1 = nums is null, 2 = nums is empty, 3 = codepage has less than 64 chars (too little).</returns>
		private static byte TightlyPackToBytes(List<ushort> nums, out byte[] result, bool base128)
		{
			if (nums is null)
			{
				result = [];
				return 1;
			}
			if (nums.Count == 0)
			{
				result = [];
				return 2;
			}
			double dataBitLength = Math.Log(Codepage.Limit, 2);
			double flooredLength = Math.Floor(dataBitLength);
			if (flooredLength < 6)
			{ // This is a limit for minimum codepage size (64 chars at least), but this should not be triggered, since the check for custom codepage should be done somewhere else.
				result = [];
				return 3;
			}
			nums.Insert(0, Codepage.Limit);
			double decimalPart = dataBitLength - flooredLength;
			ulong[] chunks;
			byte flags = 0; // Flags providing context info: bits 0 and 1: how many numbers per combined chunk? Bits 2 and 3: how many numbers in the last combined chunk? Bit 4: base128? Bits 5 – 7: unused.
			flags |= base128 ? (byte)0b0001_0000 : (byte)0b0000_0000;
			if ((decimalPart > 0d && decimalPart <= 1d / 4d) || (decimalPart > 2d / 3d && decimalPart <= 3d / 4d))
			{ // Cases for combinging 4 numbers to a chunk.
				ulong limit = Codepage.Limit;
				ulong limit2 = limit * limit;
				ulong limit3 = limit2 * limit;
				flags |= 0b0000_0011;
				(int count, int rem) = Math.DivRem(nums.Count, 4);
				if (rem == 0)
				{
					chunks = new ulong[count];
					flags |= 0b0000_1100;
					for (int i = 0; i < chunks.Length; i++)
					{
						chunks[i] = limit3 * nums[i * 4] + limit2 * nums[i * 4 + 1] + limit * nums[i * 4 + 2] + nums[i * 4 + 3];
					}
				}
				else
				{
					chunks = new ulong[count + 1];
					flags |= (byte)((rem - 1) << 2);
					for (int i = 0; i < chunks.Length - 1; i++)
					{
						chunks[i] = limit3 * nums[i * 4] + limit2 * nums[i * 4 + 1] + limit * nums[i * 4 + 2] + nums[i * 4 + 3];
					}
					chunks[^1] = rem switch
					{
						3 => limit2 * nums[^3] + limit * nums[^2] + nums[^1],
						2 => limit * nums[^2] + nums[^1],
						1 => nums[^1],
						_ => 0
					};
				}
			}
			else if ((decimalPart > 1d / 4d && decimalPart <= 1d / 3d) || (decimalPart > 1d / 2d && decimalPart <= 2d / 3d))
			{ // Cases for combinging 3 numbers to a chunk.
				ulong limit = Codepage.Limit;
				ulong limit2 = limit * limit;
				flags |= 0b0000_0010;
				(int count, int rem) = Math.DivRem(nums.Count, 3);
				if (rem == 0)
				{
					chunks = new ulong[count];
					flags |= 0b0000_1000;
					for (int i = 0; i < chunks.Length; i++)
					{
						chunks[i] = limit2 * nums[i * 3] + limit * nums[i * 3 + 1] + nums[i * 3 + 2];
					}
				}
				else
				{
					chunks = new ulong[count + 1];
					flags |= (byte)((rem - 1) << 2);
					for (int i = 0; i < chunks.Length - 1; i++)
					{
						chunks[i] = limit2 * nums[i * 3] + limit * nums[i * 3 + 1] + nums[i * 3 + 2];
					}
					chunks[^1] = rem switch
					{
						2 => limit * nums[^2] + nums[^1],
						1 => nums[^1],
						_ => 0
					};
				}
			}
			else if (decimalPart > 1d / 3d && decimalPart <= 1d / 2d)
			{ // Cases for combinging 2 numbers to a chunk.
				ulong limit = Codepage.Limit;
				flags |= 0b0000_0001;
				(int count, int rem) = Math.DivRem(nums.Count + 2, 2);
				if (rem == 0)
				{
					chunks = new ulong[count];
					flags |= 0b0000_0100;
					for (int i = 0; i < chunks.Length; i++)
					{
						chunks[i] = limit * nums[i * 2] + nums[i * 2 + 1];
					}
				}
				else
				{
					chunks = new ulong[count + 1];
					// No flags here, since it would be 00.
					for (int i = 0; i < chunks.Length - 1; i++)
					{
						chunks[i] = limit * nums[i * 2] + nums[i * 2 + 1];
					}
					chunks[^1] = nums[^1];
				}
			}
			else
			{ // Cases for no combinging.
				nums.Insert(0, (ushort)((Version << 8) | flags));
				chunks = [.. nums.ConvertAll(a => (ulong)a)];
			}
			result = PackToBytes(chunks, dataBitLength, flags);
			return 0;
		}
		/// <summary>Packs prepared data to 8bit bytes, for universal use as byte array.</summary>
		/// <param name="chunks">Prepared data to pack.</param>
		/// <param name="dataBitLength">What is the bitlength of non-combined data elements (ushorts)?</param>
		/// <param name="flags">Flags providing context info: bits 0 and 1: how many numbers per combined chunk? Bits 2 and 3: how many numbers in the last combined chunk? Bit 4: base128? Bits 5 – 7: unused.</param>
		/// <returns>Array of 8bit bytes, for binary output.</returns>
		private static byte[] PackToBytes(ulong[] chunks, double dataBitLength, byte flags)
		{
			bool base128Flag = (flags & 0b0001_0000) == 0b0001_0000;
			byte numsPerChunk = (byte)(flags & 0b0000_0011 + 1);
			byte lastChunk = (byte)(flags & 0b0000_1100 + 1);
			byte bitsInByte = base128Flag ? (byte)7 : (byte)8;
			ulong fullByte = base128Flag ? 0x0000_0000_0000_007Ful : 0x0000_0000_0000_00FFul;
			byte chunkBitLength = (byte)Math.Ceiling(dataBitLength * numsPerChunk);
			uint magicBytes = 0x5650454Du; //„VPEM“ in hex. Prefix of all messages. Stands for „Very Primitivelly Encrypted Message“. Magic bytes.
			ulong start = (ulong)magicBytes << 32 | (ulong)Version << 24 | (ulong)Codepage.Limit << 8 | flags;
			List<byte> result = [];
			byte byt = 0;
			int shiftL = 0, shiftR = 0, diff = Math.Abs(bitsInByte - chunkBitLength);
			if (base128Flag)
			{
				for (shiftR = 32 - bitsInByte; shiftR >= 0; shiftR -= bitsInByte)
				{
					result.Add((byte)((start >> shiftR) & fullByte));
				}
				shiftR += bitsInByte;
				shiftL = bitsInByte - shiftR;
				byt = (byte)((start << shiftL) & fullByte);
			}
			else
			{
				result.AddRange(Helpers.GetBytes(start));
			}
			if (chunkBitLength < bitsInByte)
			{ // < full byte.
				if (!base128Flag)
				{
					shiftL = diff;
					shiftR = chunkBitLength - shiftL;
				}
				byt |= (byte)(fullByte & (chunks[0] << shiftL));
				for (int i = 1; i < chunks.Length; i++)
				{
					byt |= (byte)(fullByte & (chunks[i] >> shiftR));
					result.Add(byt);
					byt = 0;
					if (shiftR == 0)
					{
						shiftL = diff;
						shiftR = chunkBitLength - shiftL;
					}
					else
					{
						shiftL = bitsInByte - shiftR;
					}
					byt |= (byte)(fullByte & (chunks[i] << shiftL));
				}
			}
			else if (chunkBitLength % bitsInByte == 0)
			{ // Full byte(s).
				if (chunkBitLength == bitsInByte)
				{ // Full byte.
					result.AddRange(Array.ConvertAll(chunks, a => (byte)a));
				}
				else
				{ // More bytes.
					foreach (ulong number in chunks)
					{
						for (int shift = (numsPerChunk - 1) * bitsInByte; shift >= 0; shift -= bitsInByte)
						{
							result.Add((byte)((number >> shift) & fullByte));
						}
					}
				}
			}
			else
			{ // > Full byte.
				shiftR = diff;
				foreach (ulong number in chunks)
				{
					// ToDo: Dodělej to!
				}
			}
			return [.. result];
		}
		/// <summary>Unpacks tightly packed bytes into usable data (message).</summary>
		/// <param name="data">Data to unpack.</param>
		/// <param name="result">Unpacked data (message).</param>
		/// <param name="base128">Decode as base-128 numbers?</param>
		/// <returns>Error code.</returns>
		private static byte UnpackTightlyPackedBytes(byte[] data, out List<ushort> result, bool base128)
		{ // ToDo: Dodělej to!
			if (data is null)
			{
				result = [];
				return 1;
			}
			if (data.Length < 3)
			{ // Must be at least 3 bytes. 2 for length and 1 for data. Usually should be much longer than that.
				result = [];
				return 2;
			}
			ushort dataMax = Helpers.ToUInt16(data, 0);
			if (dataMax != Codepage.Limit)
			{ // For now, I don't know how to handle different sizes of codepage. It should be the same. If not, nothing will work.
				result = [];
				return 3;
			}
			double dataBitLength = Math.Log(dataMax, 2);
			double flooredLength = Math.Floor(dataBitLength);
			if (flooredLength < 6)
			{ // This is a limit for minimum codepage size (64 chars at least), but this should not be triggered, since the check for custom codepage should be done somewhere else.
				result = [];
				return 4;
			}
			Span<byte> pureData = new(data, 2, data.Length - 2);
			List<ulong> decoded = UnpackFromBytes(pureData, dataBitLength, flooredLength);
			return 0;
		}
		/// <summary></summary>
		/// <param name="message"></param>
		/// <param name="dataBitLength"></param>
		/// <param name="flooredLength"></param>
		/// <returns></returns>
		private static List<ulong> UnpackFromBytes(Span<byte> message, double dataBitLength, double flooredLength)
		{
			// ToDo: Dodělej to!
		}
	}
}