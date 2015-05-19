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
		public PackedBitReader(byte[] pByteStream) {
			if (pByteStream == null) throw new ArgumentNullException(nameof(pByteStream));
			if (pByteStream.Length == 0) throw new ArgumentException("Must have at least 1 byte", nameof(pByteStream));

			InternalStream = new byte[pByteStream.Length];
			// Copy the block in order to avoid modifications to the underlying array causing havoc
			Buffer.BlockCopy(pByteStream, 0, InternalStream, 0, pByteStream.Length);
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
		/// <param name="pEncoding">The encoding to interpret the string's bytes.</param>
		/// <returns>A string containing the contents of the operation.</returns>
		public string ReadString(Encoding pEncoding) {
			string ret;
			if (!TryRead(out ret, pEncoding)) throw new InsufficientBitsException();
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
		/// <param name="pBitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>An sbyte containing the contents of the operation.</returns>
		public sbyte ReadSignedInt8(int pBitCount) {
			sbyte ret;
			if (!TryReadSigned(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a short. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>A short containing the contents of the operation.</returns>
		public short ReadSignedInt16(int pBitCount) {
			short ret;
			if (!TryReadSigned(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an int. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>An int containing the contents of the operation.</returns>
		public int ReadSignedInt32(int pBitCount) {
			int ret;
			if (!TryReadSigned(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a long. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read, not including the sign bit.</param>
		/// <returns>A long containing the contents of the operation.</returns>
		public long ReadSignedInt64(int pBitCount) {
			long ret;
			if (!TryReadSigned(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an sbyte. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>An sbyte containing the contents of the operation.</returns>
		public sbyte ReadInt8(int pBitCount) {
			sbyte ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a short. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A short containing the contents of the operation.</returns>
		public short ReadInt16(int pBitCount) {
			short ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an int. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>An int containing the contents of the operation.</returns>
		public int ReadInt32(int pBitCount) {
			int ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a long. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A long containing the contents of the operation.</returns>
		public long ReadInt64(int pBitCount) {
			long ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a byte. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A byte containing the contents of the operation.</returns>
		public byte ReadUInt8(int pBitCount) {
			byte ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a ushort. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A ushort containing the contents of the operation.</returns>
		public ushort ReadUInt16(int pBitCount) {
			ushort ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a uint. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A uint containing the contents of the operation.</returns>
		public uint ReadUInt32(int pBitCount) {
			uint ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into a ulong. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A ulong containing the contents of the operation.</returns>
		public ulong ReadUInt64(int pBitCount) {
			ulong ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Read a specified number of bits from the current buffer into an array. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to read.</param>
		/// <returns>A byte array containing the contents of the operation.</returns>
		public byte[] ReadBytes(int pBitCount) {
			byte[] ret;
			if (!TryRead(pBitCount, out ret)) throw new InsufficientBitsException();
			return ret;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an sbyte. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int pBitCount, out sbyte pData) {
			CheckSize(true, pBitCount, sizeof(sbyte));

			byte[] data;
			bool signed;
			if (TryReadSignedInternal(pBitCount, out data, out signed, sizeof(sbyte))) {
				pData = (sbyte)data[0];
				if (signed) pData *= -1;
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a short. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int pBitCount, out short pData) {
			CheckSize(true, pBitCount, sizeof(short));

			byte[] data;
			bool signed;
			if (TryReadSignedInternal(pBitCount, out data, out signed, sizeof(short))) {
				pData = BitConverter.ToInt16(data, 0);
				if (signed) pData *= -1;
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an int. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int pBitCount, out int pData) {
			CheckSize(true, pBitCount, sizeof(int));

			byte[] data;
			bool signed;
			if (TryReadSignedInternal(pBitCount, out data, out signed, sizeof(int))) {
				pData = BitConverter.ToInt32(data, 0);
				if (signed) pData *= -1;
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a long. The sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading, not including the sign bit.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryReadSigned(int pBitCount, out long pData) {
			CheckSize(true, pBitCount, sizeof(long));

			byte[] data;
			bool signed;
			if (TryReadSignedInternal(pBitCount, out data, out signed, sizeof(long))) {
				pData = BitConverter.ToInt64(data, 0);
				if (signed) pData *= -1;
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a bool value from the current buffer.
		/// </summary>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(out bool pData) {
			byte[] data;
			if (TryRead(1, out data)) {
				pData = data[0] == 1;
				return true;
			}
			pData = false;
			return false;
		}

		/// <summary>
		/// Attempt to read a double value from the current buffer.
		/// </summary>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(out double pData) {
			byte[] data;
			if (TryRead(ConvertBytesToBits(sizeof(double)), out data)) {
				pData = BitConverter.ToDouble(data, 0);
				return true;
			}
			pData = 0d;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an sbyte. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out sbyte pData) {
			CheckSize(false, pBitCount, sizeof(sbyte));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(sbyte))) {
				pData = (sbyte)data[0];
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a short. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out short pData) {
			CheckSize(false, pBitCount, sizeof(short));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(short))) {
				pData = BitConverter.ToInt16(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an int. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out int pData) {
			CheckSize(false, pBitCount, sizeof(int));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(int))) {
				pData = BitConverter.ToInt32(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a long. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out long pData) {
			CheckSize(false, pBitCount, sizeof(long));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(long))) {
				pData = BitConverter.ToInt64(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a byte. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out byte pData) {
			CheckSize(false, pBitCount, sizeof(byte));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(byte))) {
				pData = data[0];
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a ushort. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out ushort pData) {
			CheckSize(false, pBitCount, sizeof(ushort));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(ushort))) {
				pData = BitConverter.ToUInt16(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a uint. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out uint pData) {
			CheckSize(false, pBitCount, sizeof(uint));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(uint))) {
				pData = BitConverter.ToUInt32(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into a ulong. No sign bit is read.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The value that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out ulong pData) {
			CheckSize(false, pBitCount, sizeof(ulong));

			byte[] data;
			if (TryReadInternal(pBitCount, out data, sizeof(ulong))) {
				pData = BitConverter.ToUInt64(data, 0);
				return true;
			}
			pData = 0;
			return false;
		}

		/// <summary>
		/// Attempt to read a string out of the current buffer. The default encoding is UTF8.
		/// </summary>
		/// <param name="pData">The string that will be initialized in the case where the read is successful.</param>
		/// <returns>true if reading the string was successful, false if a string could not be read.</returns>
		public bool TryRead(out string pData) {
			return TryRead(out pData, Encoding.UTF8);
		}

		/// <summary>
		/// Attempt to read a string out of the current buffer.
		/// </summary>
		/// <param name="pData">The string that will be initialized in the case where the read is successful.</param>
		/// <param name="pEncoding">The encoding to interpret the string's bytes.</param>
		/// <returns>true if reading the string was successful, false if a string could not be read.</returns>
		public bool TryRead(out string pData, Encoding pEncoding) {
			int length;
			if (!TryRead(sizeof(int) - 1, out length)) {
				pData = null;
				return false;
			}
			byte[] data;
			if (!TryRead(ConvertBytesToBits(length), out data)) {
				pData = null;
				return false;
			}
			pData = pEncoding.GetString(data);
			return true;
		}

		/// <summary>
		/// Attempt to read a specified number of bits from the current buffer into an array.
		/// </summary>
		/// <param name="pBitCount">The number of bits to try reading.</param>
		/// <param name="pData">The array that will be intialized in the case where there are enough bits.</param>
		/// <returns>true if reading all the bits was successful, false if there were not enough bits to read.</returns>
		public bool TryRead(int pBitCount, out byte[] pData) {
			return TryReadInternal(pBitCount, out pData, (int)Math.Ceiling((double)pBitCount / Constants.BitsInByte));
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

		private static int ConvertBytesToBits(int pBytes) {
			return pBytes * Constants.BitsInByte;
		}

		private bool TryReadSignedInternal(int pBitCount, out byte[] pData, out bool pSigned, int pTypeBytes) {
			if (pBitCount > RemainingBits) {
				pData = null;
				pSigned = false;
				return false;
			}

			if (!TryRead(out pSigned)) {
				pData = null;
				return false;
			}

			if (!TryReadInternal(pBitCount, out pData, pTypeBytes)) {
				pData = null;
				return false;
			}

			return true;
		}

		private bool TryReadInternal(int pBitCount, out byte[] pData, int pTypeBytes) {
			if (pBitCount > RemainingBits) {
				pData = null;
				return false;
			}

			pData = new byte[pTypeBytes];

			int destBytePos = pData.Length - 1;
			int destBitPos = 1;
			int consumedBits = 0;

			while (consumedBits < pBitCount) {
				int bitsToConsume = Math.Min(pBitCount - consumedBits, Constants.ByteSizeInBits);
				int remainingBits = Constants.ByteSizeInBits - (BitPos - 1);
				int attemptConsumeBits = Math.Min(bitsToConsume, remainingBits);
				byte rawValue = (byte)(InternalStream[BytePos] & PackedBitMasks.GetWideningMask(attemptConsumeBits, BitPos - 1));

				BitPos += attemptConsumeBits;
				if (BitPos > Constants.ByteSizeInBits) {
					BitPos = 1;
					BytePos++;
				}

				if (bitsToConsume > attemptConsumeBits) {
					pData[destBytePos] |= (byte)(rawValue << (bitsToConsume - attemptConsumeBits));
					destBitPos += attemptConsumeBits;
					if (destBitPos > Constants.ByteSizeInBits) {
						destBitPos = 1;
						destBytePos--;
					}

					remainingBits = bitsToConsume - attemptConsumeBits;
					rawValue = (byte)(InternalStream[BytePos] & PackedBitMasks.GetWideningMask(remainingBits, BitPos - 1));
					pData[destBytePos] |= (byte)(rawValue >> (Constants.ByteSizeInBits - remainingBits));

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
					pData[destBytePos] |= (byte)(rawValue >> (remainingBits - bitsToConsume));
					destBitPos += bitsToConsume;
					if (destBitPos > Constants.ByteSizeInBits) {
						destBitPos = 1;
						destBytePos--;
					}
				}

				consumedBits += bitsToConsume;
			}

			if (BitConverter.IsLittleEndian) Array.Reverse(pData);
			return true;
		}
	}
}
