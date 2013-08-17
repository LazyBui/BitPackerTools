using System;
using System.Collections.Generic;
using System.Text;

namespace BitPackerTools {
	/// <summary>
	/// Represents a compact array that is written based on specific bit-level inputs.
	/// </summary>
	public sealed class PackedBitWriter {
		private List<byte> InternalStream { get; set; }
		private int BitPos { get; set; }
		private bool ForceAddByte { get; set; }
		private const int cByteSizeInBits = sizeof(byte) * 8;

		/// <summary>
		/// Initializes a new instance of the BitPackerTools.PackedBitWriter class that has an empty bit buffer.
		/// </summary>
		public PackedBitWriter() {
			BitPos = 1;
			InternalStream = new List<byte>();
		}

		/// <summary>
		/// Writes an sbyte to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 7 bits is the maximum)</param>
		/// <param name="pData">The value to write bits from.</param>
		public void WriteSigned(int pBitCount, sbyte pData) {
			bool signed = pData < 0;
			Write(signed);
			if (signed) pData *= -1;
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a short to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 15 bits is the maximum)</param>
		/// <param name="pData">The value to write bits from.</param>
		public void WriteSigned(int pBitCount, short pData) {
			bool signed = pData < 0;
			Write(signed);
			if (signed) pData *= -1;
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes an int to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 31 bits is the maximum)</param>
		/// <param name="pData">The value to write bits from.</param>
		public void WriteSigned(int pBitCount, int pData) {
			bool signed = pData < 0;
			Write(signed);
			if (signed) pData *= -1;
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a long to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 63 bits is the maximum)</param>
		/// <param name="pData">The value to write bits from.</param>
		public void WriteSigned(int pBitCount, long pData) {
			bool signed = pData < 0;
			Write(signed);
			if (signed) pData *= -1;
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a bool to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pData">The value to write a bit from.</param>
		public void Write(bool pData) {
			Write(1, pData ? 1 : 0);
		}

		/// <summary>
		/// Writes an sbyte to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, sbyte pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a short to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, short pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes an int to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, int pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a long to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, long pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a byte to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, byte pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a ushort to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, ushort pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a uint to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, uint pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a ulong to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the value to write.</param>
		/// <param name="pData">The value to write bits from.</param>
		public void Write(int pBitCount, ulong pData) {
			Write(pBitCount, BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a double to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// </summary>
		/// <param name="pData">The double to write to the current buffer.</param>
		public void Write(double pData) {
			Write(ConvertBytesToBits(sizeof(double)), BitConverter.GetBytes(pData));
		}

		/// <summary>
		/// Writes a string to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// The default encoding is UTF8.
		/// </summary>
		/// <param name="pData">The string to write to the current buffer.</param>
		public void Write(string pData) {
			Write(pData, Encoding.UTF8);
		}

		/// <summary>
		/// Writes a string to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// </summary>
		/// <param name="pData">The string to write to the current buffer.</param>
		/// <param name="pEncoding">The encoding for translating a string to bytes.</param>
		public void Write(string pData, Encoding pEncoding) {
			byte[] data = pEncoding.GetBytes(pData);
			Write(sizeof(int) - 1, data.Length);
			Write(ConvertBytesToBits(data.Length), data);
		}

		/// <summary>
		/// Writes the specified number of bits from an array of bytes to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="pBitCount">The number of bits from the array to write.</param>
		/// <param name="pData">The array to write bits from.</param>
		public void Write(int pBitCount, byte[] pData) {
			CheckSize(false, pBitCount, pData.Length);
			// Copy the data so we can reverse the array without the caller seeing any changes
			byte[] data = new byte[pData.Length];
			Buffer.BlockCopy(pData, 0, data, 0, pData.Length);
			WriteInternal(pBitCount, data, 0, data.Length);
		}

		/// <summary>
		/// Copies the internal buffer of bytes to a new array.
		/// </summary>
		/// <returns>An array of bytes built up from the internal buffer.</returns>
		public byte[] ToArray() {
			return InternalStream.ToArray();
		}

		private static int ConvertBytesToBits(int pBytes) {
			return pBytes * 8;
		}

		private int ParseBitCountAndExpandStreamAsNeeded(int pBitCount) {
			if (InternalStream.Count == 0) InternalStream.Add(0x00);

			int oldPos = InternalStream.Count - 1;
			int bytesToAdd = 0;

			if ((pBitCount + (BitPos - 1)) > cByteSizeInBits) {
				int adjustedBitCount = pBitCount - (cByteSizeInBits - (BitPos - 1));
				bytesToAdd = adjustedBitCount / cByteSizeInBits;
				if (adjustedBitCount % cByteSizeInBits != 0) bytesToAdd++;
			}
			if (ForceAddByte) {
				bytesToAdd++;
				oldPos++;
			}

			for (int i = 0; i < bytesToAdd; i++) InternalStream.Add(0x00);

			ForceAddByte = false;
			return oldPos;
		}

		private void CheckSize(bool pSigned, int pBitCount, int pTypeSize) {
			if (pBitCount < 1) throw new BitCountException();
			if (pSigned) {
				if (pBitCount >= ConvertBytesToBits(pTypeSize)) throw new BitCountException();
			}
			else {
				if (pBitCount > ConvertBytesToBits(pTypeSize)) throw new BitCountException();
			}
		}

		// Do I need array offsets/lengths in the public API? Might get a little hard to keep track of positions.
		// I can add them later if needed but I'll have to revisit the logic here because it's almost certainly wrong.
		// Alternate note: This function assumes it's being passed a big-endian bit arrays
		private void WriteInternal(int pBitCount, byte[] pData, int? pOffset, int? pLength) {
			pOffset = pOffset ?? 0;
			pLength = (pLength ?? pData.Length) - pOffset.GetValueOrDefault() - 1;

			if (BitConverter.IsLittleEndian) Array.Reverse(pData);

			int bytePos = ParseBitCountAndExpandStreamAsNeeded(pBitCount);
			int srcBytePos = pOffset.GetValueOrDefault() + pLength.GetValueOrDefault();
			int srcBitPos = 1;
			int consumedBits = 0;

			while (consumedBits < pBitCount) {
				int bitsToConsume = Math.Min(pBitCount - consumedBits, cByteSizeInBits);
				byte rawValue = (byte)(pData[srcBytePos] & PackedBitMasks.GetNarrowingMask(bitsToConsume));
				int remainingBits = cByteSizeInBits - (BitPos - 1);

				// Extract only the bits we need for the current byte
				// Assuming we have more bits than our current byte boundary, we have to apply some bits to the next byte
				if (bitsToConsume > remainingBits) {
					InternalStream[bytePos++] |= (byte)((byte)(rawValue >> (bitsToConsume - remainingBits)) & PackedBitMasks.GetNarrowingMask(remainingBits));
					BitPos = 1;
					remainingBits = bitsToConsume - remainingBits;

					InternalStream[bytePos] |= (byte)(rawValue << (cByteSizeInBits - remainingBits));
					BitPos += remainingBits;
					ForceAddByte = false;
				}
				else {
					InternalStream[bytePos] |= (byte)(rawValue << (remainingBits - bitsToConsume));
					BitPos += bitsToConsume;
					if (BitPos > cByteSizeInBits) {
						BitPos = 1;
						bytePos++;
						// If the bits are directly on the border of a byte boundary (e.g. packed 32 bits)
						// Then we must indicate to the expansion function that it must add another byte
						// Because it uses the position in the current byte to determine how many are needed
						// But only if we end on this byte
						ForceAddByte = true;
					}
					else ForceAddByte = false;
				}

				srcBitPos += bitsToConsume;
				if (srcBitPos > cByteSizeInBits) {
					srcBitPos = 1;
					srcBytePos--;
				}

				consumedBits += bitsToConsume;
			}
		}
	}
}
