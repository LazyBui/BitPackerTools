using System;
using System.Collections.Generic;
using System.Text;

namespace BitPackerTools {
	/// <summary>
	/// Represents a compact array that is written based on specific bit-level inputs.
	/// </summary>
	public sealed class PackedBitWriter {
		private List<byte> InternalStream { get; set; } = new List<byte>();
		private int BitPos { get; set; } = 1;
		private bool ForceAddByte { get; set; }

		/// <summary>
		/// Initializes a new instance of the BitPackerTools.PackedBitWriter class that has an empty bit buffer.
		/// </summary>
		public PackedBitWriter() { }

		/// <summary>
		/// Writes an sbyte to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 7 bits is the maximum)</param>
		/// <param name="data">The value to write bits from.</param>
		public void WriteSigned(int bitCount, sbyte data) {
			bool signed = data < 0;
			Write(signed);
			if (signed) data *= -1;
			Write(bitCount, BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Writes a short to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 15 bits is the maximum)</param>
		/// <param name="data">The value to write bits from.</param>
		public void WriteSigned(int bitCount, short data) {
			bool signed = data < 0;
			Write(signed);
			if (signed) data *= -1;
			Write(bitCount, BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Writes an int to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 31 bits is the maximum)</param>
		/// <param name="data">The value to write bits from.</param>
		public void WriteSigned(int bitCount, int data) {
			bool signed = data < 0;
			Write(signed);
			if (signed) data *= -1;
			Write(bitCount, BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Writes a long to the current buffer (sign bit is included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write. The maximum value must account for the sign bit (i.e. 63 bits is the maximum)</param>
		/// <param name="data">The value to write bits from.</param>
		public void WriteSigned(int bitCount, long data) {
			bool signed = data < 0;
			Write(signed);
			if (signed) data *= -1;
			Write(bitCount, BitConverter.GetBytes(data));
		}

		/// <summary>
		/// Writes a bool to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="data">The value to write a bit from.</param>
		public void Write(bool data) => Write(1, data ? 1 : 0);

		/// <summary>
		/// Writes an sbyte to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, sbyte data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a short to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, short data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes an int to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, int data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a long to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, long data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a byte to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, byte data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a ushort to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, ushort data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a uint to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, uint data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a ulong to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the value to write.</param>
		/// <param name="data">The value to write bits from.</param>
		public void Write(int bitCount, ulong data) => Write(bitCount, BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a <see cref="float" /> to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// </summary>
		/// <param name="data">The <see cref="float" /> to write to the current buffer.</param>
		public void Write(float data) => Write(ConvertBytesToBits(sizeof(float)), BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a <see cref="double" /> to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// </summary>
		/// <param name="data">The <see cref="double" /> to write to the current buffer.</param>
		public void Write(double data) => Write(ConvertBytesToBits(sizeof(double)), BitConverter.GetBytes(data));

		/// <summary>
		/// Writes a string to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// The default encoding is UTF8.
		/// </summary>
		/// <param name="data">The string to write to the current buffer.</param>
		public void Write(string data) => Write(data, Encoding.UTF8);

		/// <summary>
		/// Writes a string to the current buffer (no sign bit included).
		/// This is provided as a convenience, it saves no space in a bitpacked context.
		/// </summary>
		/// <param name="data">The string to write to the current buffer.</param>
		/// <param name="encoding">The encoding for translating a string to bytes.</param>
		public void Write(string data, Encoding encoding) {
			if (object.ReferenceEquals(data, null)) throw new ArgumentNullException(nameof(data));
			if (object.ReferenceEquals(encoding, null)) throw new ArgumentNullException(nameof(encoding));
			byte[] raw = encoding.GetBytes(data);
			Write(ConvertBytesToBits(sizeof(int)), raw.Length);
			if (raw.Length > 0) {
				Write(ConvertBytesToBits(raw.Length), raw);
			}
		}

		/// <summary>
		/// Writes the specified number of bits from an array of bytes to the current buffer (no sign bit included).
		/// </summary>
		/// <param name="bitCount">The number of bits from the array to write.</param>
		/// <param name="data">The array to write bits from.</param>
		public void Write(int bitCount, byte[] data) {
			if (object.ReferenceEquals(data, null)) throw new ArgumentNullException(nameof(data));
			if (data.Length == 0) throw new ArgumentException("Must have elements", nameof(data));
			CheckSize(false, bitCount, data.Length);
			// Copy the data so we can reverse the array without the caller seeing any changes
			byte[] raw = new byte[data.Length];
			Buffer.BlockCopy(data, 0, raw, 0, data.Length);
			WriteInternal(bitCount, raw, 0, raw.Length);
		}

		/// <summary>
		/// Copies the internal buffer of bytes to a new array.
		/// </summary>
		/// <returns>An array of bytes built up from the internal buffer.</returns>
		public byte[] ToArray() => InternalStream.ToArray();

		private static int ConvertBytesToBits(int bytes) => bytes * Constants.BitsInByte;

		private int ParseBitCountAndExpandStreamAsNeeded(int bitCount) {
			if (InternalStream.Count == 0) InternalStream.Add(0x00);

			int oldPos = InternalStream.Count - 1;
			int bytesToAdd = 0;

			if ((bitCount + (BitPos - 1)) > Constants.ByteSizeInBits) {
				int adjustedBitCount = bitCount - (Constants.ByteSizeInBits - (BitPos - 1));
				bytesToAdd = adjustedBitCount / Constants.ByteSizeInBits;
				if (adjustedBitCount % Constants.ByteSizeInBits != 0) bytesToAdd++;
			}
			if (ForceAddByte) {
				bytesToAdd++;
				oldPos++;
			}

			for (int i = 0; i < bytesToAdd; i++) InternalStream.Add(0x00);

			ForceAddByte = false;
			return oldPos;
		}

		private void CheckSize(bool isSigned, int bitCount, int typeSize) {
			if (bitCount < 1) throw new BitCountException();
			if (isSigned) {
				if (bitCount >= ConvertBytesToBits(typeSize)) throw new BitCountException();
			}
			else {
				if (bitCount > ConvertBytesToBits(typeSize)) throw new BitCountException();
			}
		}

		// Do I need array offsets/lengths in the public API? Might get a little hard to keep track of positions.
		// I can add them later if needed but I'll have to revisit the logic here because it's almost certainly wrong.
		// Alternate note: This function assumes it's being passed a big-endian bit arrays
		private void WriteInternal(int bitCount, byte[] data, int? offset, int? length) {
			offset = offset ?? 0;
			length = (length ?? data.Length) - offset.GetValueOrDefault() - 1;

			if (BitConverter.IsLittleEndian) Array.Reverse(data);

			int bytePos = ParseBitCountAndExpandStreamAsNeeded(bitCount);
			int srcBytePos = offset.GetValueOrDefault() + length.GetValueOrDefault();
			int srcBitPos = 1;
			int consumedBits = 0;

			while (consumedBits < bitCount) {
				int bitsToConsume = Math.Min(bitCount - consumedBits, Constants.ByteSizeInBits);
				byte rawValue = (byte)(data[srcBytePos] & PackedBitMasks.GetNarrowingMask(bitsToConsume));
				int remainingBits = Constants.ByteSizeInBits - (BitPos - 1);

				// Extract only the bits we need for the current byte
				// Assuming we have more bits than our current byte boundary, we have to apply some bits to the next byte
				if (bitsToConsume > remainingBits) {
					InternalStream[bytePos++] |= (byte)((byte)(rawValue >> (bitsToConsume - remainingBits)) & PackedBitMasks.GetNarrowingMask(remainingBits));
					BitPos = 1;
					remainingBits = bitsToConsume - remainingBits;

					InternalStream[bytePos] |= (byte)(rawValue << (Constants.ByteSizeInBits - remainingBits));
					BitPos += remainingBits;
					ForceAddByte = false;
				}
				else {
					InternalStream[bytePos] |= (byte)(rawValue << (remainingBits - bitsToConsume));
					BitPos += bitsToConsume;
					if (BitPos > Constants.ByteSizeInBits) {
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
				if (srcBitPos > Constants.ByteSizeInBits) {
					srcBitPos = 1;
					srcBytePos--;
				}

				consumedBits += bitsToConsume;
			}
		}
	}
}
