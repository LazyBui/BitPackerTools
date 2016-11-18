using System;
using System.Text;

namespace BitPackerTools {
	/// <summary>
	/// Represents a compact array that is read based on specific bit-level outputs.
	/// </summary>
	public sealed class PackedBitReader {
		private byte[] InternalStream { get; set; }
		private int BytePos { get; set; } = 0;
		private int BitPos { get; set; } = 1;
		private int RemainingBits {
			get { return ConvertBytesToBits(InternalStream.Length) - ConvertBytesToBits(BytePos) - BitPos + 1; }
		}

		/// <summary>
		/// Initializes a new instance of the BitPackerTools.PackedBitReader class with a bit buffer.
		/// </summary>
		public PackedBitReader(byte[] byteStream) {
			if (byteStream == null) throw new ArgumentNullException(nameof(byteStream));
			if (byteStream.Length == 0) throw new ArgumentException("Must have at least 1 byte", nameof(byteStream));

			InternalStream = new byte[byteStream.Length];
			// Copy the block in order to avoid modifications to the underlying array causing havoc
			Buffer.BlockCopy(byteStream, 0, InternalStream, 0, byteStream.Length);
		}

		/// <summary>
		/// Read a string value from current buffer. The default encoding is UTF8.
		/// </summary>
		/// <returns>A string containing the contents of the operation.</returns>
		public string ReadString() {
			string ret;
			if (!TryRead(out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a string value from current buffer.
		/// </summary>
		/// <param name="encoding">The encoding to interpret the string's bytes.</param>
		/// <returns>A string containing the contents of the operation.</returns>
		public string ReadString(Encoding encoding) {
			string ret;
			if (!TryRead(out ret, encoding)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a bool value from current buffer.
		/// </summary>
		/// <returns>A bool containing the contents of the operation.</returns>
		public bool ReadBool() {
			bool ret;
			if (!TryRead(out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a double value from current buffer.
		/// </summary>
		/// <returns>A double containing the contents of the operation.</returns>
		public double ReadDouble() {
			double ret;
			if (!TryRead(out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an sbyte. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>An sbyte containing the contents of the operation.</returns>
		public sbyte ReadSignedInt8(int bitCount) {
			sbyte ret;
			if (!TryReadSigned(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a short. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>A short containing the contents of the operation.</returns>
		public short ReadSignedInt16(int bitCount) {
			short ret;
			if (!TryReadSigned(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an int. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>An int containing the contents of the operation.</returns>
		public int ReadSignedInt32(int bitCount) {
			int ret;
			if (!TryReadSigned(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a long. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>A long containing the contents of the operation.</returns>
		public long ReadSignedInt64(int bitCount) {
			long ret;
			if (!TryReadSigned(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an sbyte. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>An sbyte containing the contents of the operation.</returns>
		public sbyte ReadInt8(int bitCount) {
			sbyte ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a short. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A short containing the contents of the operation.</returns>
		public short ReadInt16(int bitCount) {
			short ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an int. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>An int containing the contents of the operation.</returns>
		public int ReadInt32(int bitCount) {
			int ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a long. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A long containing the contents of the operation.</returns>
		public long ReadInt64(int bitCount) {
			long ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a byte. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A byte containing the contents of the operation.</returns>
		public byte ReadUInt8(int bitCount) {
			byte ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a ushort. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A ushort containing the contents of the operation.</returns>
		public ushort ReadUInt16(int bitCount) {
			ushort ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a uint. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A uint containing the contents of the operation.</returns>
		public uint ReadUInt32(int bitCount) {
			uint ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a ulong. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A ulong containing the contents of the operation.</returns>
		public ulong ReadUInt64(int bitCount) {
			ulong ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an array. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to read.</param>
		/// <returns>A byte array containing the contents of the operation.</returns>
		public byte[] ReadBytes(int bitCount) {
			byte[] ret;
			if (!TryRead(bitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an sbyte. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int bitCount, out sbyte data) {
			CheckSize(true, bitCount, sizeof(sbyte));

			byte[] raw;
			bool signed;
			if (TryReadSignedInternal(bitCount, out raw, out signed, sizeof(sbyte))) {
				data = (sbyte)raw[0];
				if (signed) data *= -1;
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a short. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int bitCount, out short data) {
			CheckSize(true, bitCount, sizeof(short));

			byte[] raw;
			bool signed;
			if (TryReadSignedInternal(bitCount, out raw, out signed, sizeof(short))) {
				data = BitConverter.ToInt16(raw, 0);
				if (signed) data *= -1;
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an int. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int bitCount, out int data) {
			CheckSize(true, bitCount, sizeof(int));

			byte[] raw;
			bool signed;
			if (TryReadSignedInternal(bitCount, out raw, out signed, sizeof(int))) {
				data = BitConverter.ToInt32(raw, 0);
				if (signed) data *= -1;
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a long. The sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int bitCount, out long data) {
			CheckSize(true, bitCount, sizeof(long));

			byte[] raw;
			bool signed;
			if (TryReadSignedInternal(bitCount, out raw, out signed, sizeof(long))) {
				data = BitConverter.ToInt64(raw, 0);
				if (signed) data *= -1;
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a bool value from the current buffer.
		/// </summary>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(out bool data) {
			byte[] raw;
			if (TryRead(1, out raw)) {
				data = raw[0] == 1;
				return true;
			}
			data = false;
			return false;
		}

		/// <summary>
		/// Attempt to read a <see cref="float" /> value from the current buffer.
		/// </summary>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(out float data) {
			byte[] raw;
			if (TryRead(ConvertBytesToBits(sizeof(float)), out raw)) {
				data = BitConverter.ToSingle(raw, 0);
				return true;
			}
			data = 0f;
			return false;
		}

		/// <summary>
		/// Attempt to read a <see cref="double" /> value from the current buffer.
		/// </summary>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(out double data) {
			byte[] raw;
			if (TryRead(ConvertBytesToBits(sizeof(double)), out raw)) {
				data = BitConverter.ToDouble(raw, 0);
				return true;
			}
			data = 0d;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an sbyte. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out sbyte data) {
			CheckSize(false, bitCount, sizeof(sbyte));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(sbyte))) {
				data = (sbyte)raw[0];
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a short. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out short data) {
			CheckSize(false, bitCount, sizeof(short));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(short))) {
				data = BitConverter.ToInt16(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an int. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out int data) {
			CheckSize(false, bitCount, sizeof(int));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(int))) {
				data = BitConverter.ToInt32(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a long. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out long data) {
			CheckSize(false, bitCount, sizeof(long));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(long))) {
				data = BitConverter.ToInt64(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a byte. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out byte data) {
			CheckSize(false, bitCount, sizeof(byte));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(byte))) {
				data = raw[0];
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a ushort. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out ushort data) {
			CheckSize(false, bitCount, sizeof(ushort));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(ushort))) {
				data = BitConverter.ToUInt16(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a uint. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out uint data) {
			CheckSize(false, bitCount, sizeof(uint));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(uint))) {
				data = BitConverter.ToUInt32(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a ulong. No sign bit is read.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out ulong data) {
			CheckSize(false, bitCount, sizeof(ulong));

			byte[] raw;
			if (TryReadInternal(bitCount, out raw, sizeof(ulong))) {
				data = BitConverter.ToUInt64(raw, 0);
				return true;
			}
			data = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a string out of the current buffer. The default encoding is UTF8.
		/// </summary>
		/// <param name="data">The string that will be initialized in the case where the read is successful.</param>
		/// <returns>true if reading the string was successful, false if a string could not be read.</returns>
		public bool TryRead(out string data) => TryRead(out data, Encoding.UTF8);

		/// <summary>
		/// Attempt to read a string out of the current buffer.
		/// </summary>
		/// <param name="data">The string that will be initialized in the case where the read is successful.</param>
		/// <param name="encoding">The encoding to interpret the string's bytes.</param>
		/// <returns>true if reading the string was successful, false if a string could not be read.</returns>
		public bool TryRead(out string data, Encoding encoding) {
			if (object.ReferenceEquals(encoding, null)) throw new ArgumentNullException(nameof(encoding));

			int length;
			if (!TryRead(ConvertBytesToBits(sizeof(int)), out length)) {
				data = null;
				return false;
			}
			if (length == 0) {
				data = string.Empty;
				return true;
			}

			byte[] raw;
			if (!TryRead(ConvertBytesToBits(length), out raw)) {
				data = null;
				return false;
			}
			data = encoding.GetString(raw);
			return true;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an array.
		/// </summary>
		/// <param name="bitCount">The number of bits to try reading.</param>
		/// <param name="data">The array that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int bitCount, out byte[] data) =>
			TryReadInternal(bitCount, out data, (int)Math.Ceiling((double)bitCount / Constants.BitsInByte));

		private void CheckSize(bool isSigned, int bitCount, int typeSize) {
			if (bitCount < 1) throw new BitCountException();
			if (isSigned) {
				if (bitCount >= ConvertBytesToBits(typeSize)) throw new BitCountException();
			}
			else {
				if (bitCount > ConvertBytesToBits(typeSize)) throw new BitCountException();
			}
		}

		private static int ConvertBytesToBits(int bytes) => bytes * Constants.BitsInByte;

		private bool TryReadSignedInternal(int bitCount, out byte[] data, out bool isSigned, int typeBytes) {
			if (bitCount > RemainingBits) {
				data = null;
				isSigned = false;
				return false;
			}

			if (!TryRead(out isSigned)) {
				data = null;
				return false;
			}

			if (!TryReadInternal(bitCount, out data, typeBytes)) {
				data = null;
				return false;
			}

			return true;
		}

		private bool TryReadInternal(int bitCount, out byte[] data, int typeBytes) {
			if (bitCount > RemainingBits) {
				data = null;
				return false;
			}

			data = new byte[typeBytes];

			int destBytePos = data.Length - 1;
			int destBitPos = 1;
			int consumedBits = 0;

			while (consumedBits < bitCount) {
				int bitsToConsume = Math.Min(bitCount - consumedBits, Constants.ByteSizeInBits);
				int remainingBits = Constants.ByteSizeInBits - (BitPos - 1);
				int attemptConsumeBits = Math.Min(bitsToConsume, remainingBits);
				byte rawValue = (byte)(InternalStream[BytePos] & PackedBitMasks.GetWideningMask(attemptConsumeBits, BitPos - 1));

				BitPos += attemptConsumeBits;
				if (BitPos > Constants.ByteSizeInBits) {
					BitPos = 1;
					BytePos++;
				}

				if (bitsToConsume > attemptConsumeBits) {
					data[destBytePos] |= (byte)(rawValue << (bitsToConsume - attemptConsumeBits));
					destBitPos += attemptConsumeBits;
					if (destBitPos > Constants.ByteSizeInBits) {
						destBitPos = 1;
						destBytePos--;
					}

					remainingBits = bitsToConsume - attemptConsumeBits;
					rawValue = (byte)(InternalStream[BytePos] & PackedBitMasks.GetWideningMask(remainingBits, BitPos - 1));
					data[destBytePos] |= (byte)(rawValue >> (Constants.ByteSizeInBits - remainingBits));

					destBitPos += remainingBits;
					if (destBitPos > Constants.ByteSizeInBits) {
						destBitPos = 1;
						destBytePos--;
					}

					BitPos += remainingBits;
					if (BitPos > Constants.ByteSizeInBits) {
						BitPos = 1;
						BytePos++;
					}
				}
				else {
					data[destBytePos] |= (byte)(rawValue >> (remainingBits - bitsToConsume));
					destBitPos += bitsToConsume;
					if (destBitPos > Constants.ByteSizeInBits) {
						destBitPos = 1;
						destBytePos--;
					}
				}

				consumedBits += bitsToConsume;
			}

			if (BitConverter.IsLittleEndian) Array.Reverse(data);
			return true;
		}
	}
}
